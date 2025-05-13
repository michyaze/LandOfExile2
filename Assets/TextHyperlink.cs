///
/// 《超链接文本》支持多个链接 支持正则表达式
/// 当前版本修改于 uGUI-Hypertext GitHub:https://github.com/setchi/uGUI-Hypertext/tree/master
/// 新增超链接颜色修改控制。
/// 统一事件点击回调
/// 默认支持href匹配
/// 添加超链接下划线
/// 添加默认超链接颜色
/// 版本：0.10.1
/// 
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
namespace Hyperlink
{
    /// <summary>
    /// 顶点池子
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ObjectPool<T> where T : new()
    {
        private readonly Stack<T> _stack = new Stack<T>();
        private readonly Action<T> _getAction;
        private readonly Action<T> _releaseAction;
        
        /// <summary>
        /// 总数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 没有被使用的数量
        /// </summary>
        public int UnusedCount => _stack.Count;
        /// <summary>
        /// 已经使用的数量
        /// </summary>
        public int UsedCount => Count - UnusedCount;
 
        public ObjectPool(Action<T> onGetAction, Action<T> onRelease)
        {
            _getAction = onGetAction;
            _releaseAction = onRelease;
        }
        
        public T Get()
        {
            T element;
            if (_stack.Count == 0)
            {
                element = new T();
                Count++;
            }
            else
            {
                element = _stack.Pop();
            }
 
            _getAction?.Invoke(element);
 
            return element;
        }
 
        public void Release(T element)
        {
            if (_stack.Count > 0 && ReferenceEquals(_stack.Peek(), element))
            {
                UnityEngine.Debug.LogError("试图归还已经归还的对象。");
            }
 
            _releaseAction?.Invoke(element);
 
            _stack.Push(element);
        }
    }
 
    /// <summary>
    /// 超链接信息块
    /// </summary>
    internal class LinkInfo
    {
        public readonly int StartIndex;
        public readonly int Length;
        public readonly string Link = null;
        public readonly string Text;
        public readonly Color Color = new Color(75 / 255f, 122 / 255f, 247 / 255f, 1f);
        public  readonly bool OverwriteColor = false;
        public readonly ClickLinkEvent Callback;
        public List<Rect> Boxes;
 
        public LinkInfo(int startIndex, int length, Color? color, ClickLinkEvent callback)
        {
            StartIndex = startIndex;
            Length = length;
            Link = null;
            Text = null;
            OverwriteColor = color.HasValue;
            if (color.HasValue)
            {
                Color = color.Value;
            }
            Callback = callback;
            Boxes = new List<Rect>();
        }
 
        public LinkInfo(int startIndex, int length, string link, string text, Color? color, ClickLinkEvent callback)
        {
            StartIndex = startIndex;
            Length = length;
            Link = link;
            Text = text;
            OverwriteColor = color.HasValue;
            if (color.HasValue)
            {
                Color = color.Value;
            }
            Callback = callback;
            Boxes = new List<Rect>();
        }
 
        public LinkInfo(int startIndex, string link, string text, Color? color, ClickLinkEvent callback) : this(startIndex, text.Length, link, text, color,
            callback)
        {
        }
 
        public LinkInfo(int startIndex, string link, string text, ClickLinkEvent callback) : this(startIndex, link, text, Color.blue, 
            callback)
        {
        }
    }
    
    /// <summary>
    /// 超链接点击事件
    /// </summary>
    [Serializable]
    public class ClickLinkEvent : UnityEvent<string,string>
    {
    }
 
    /// <summary>
    /// 超链接正则表达式
    /// </summary>
    [Serializable]
    public class RegexPattern
    {
        public string pattern;
        public Color color;
        public bool overwriteColor = false;
        
        public RegexPattern(string regexPattern, Color color,bool overwriteColor = true)
        {
            this.pattern = regexPattern;
            this.overwriteColor = overwriteColor;
            this.color = color;
        }
    }
    
    public class TextHyperlink : Text, IPointerClickHandler
    {
        private const int CharVertex = 6;
        private const char Tab = '\t', LineFeed = '\n', Space = ' ', LesserThan = '<', GreaterThan = '>';
        /// <summary>
        /// 看不见顶点的字符
        /// </summary>
        private readonly char[] _invisibleChars =
        {
            Space,
            Tab,
            LineFeed
        };
        /// <summary>
        /// 超链接信息块
        /// </summary>
        private readonly List<LinkInfo> _links = new List<LinkInfo>();
        /// <summary>
        /// 字符顶点池
        /// </summary>
        private static readonly ObjectPool<List<UIVertex>> UIVerticesPool = new ObjectPool<List<UIVertex>>(null, l => l.Clear());
        /// <summary>
        /// 字符索引映射
        /// </summary>
        private int[] _charIndexMap;
 
        /// <summary>
        /// 超链接默认颜色
        /// </summary>
        private static readonly Color LinkColor = new Color(75 / 255f, 122 / 255f, 247 / 255f, 1f); 
        
        private Canvas _root;
        private Canvas RootCanvas => _root ? _root : (_root = GetComponentInParent<Canvas>());
 
        [SerializeField]
        private ClickLinkEvent _onClickLink = new ClickLinkEvent();
 
        /// <summary>
        /// 超链接匹配规则
        /// </summary>
        public List<RegexPattern> linkRegexPattern = new List<RegexPattern>()
        {
            new RegexPattern(@"<a href=([^>\n\s]+)>(.*?)(</a>)", LinkColor),
            // new RegexPattern(@"<a\s+[^>]*href\s*=\s*['""""](?<url>[^'""""]*)['""""][^>]*>(?<text>.*?)</a>", LinkColor),
        };
 
        /// <summary>
        /// 下划线
        /// </summary>
        public string underline = "￣";
 
        /// <summary>
        /// 超链接点击事件
        /// </summary>
        public ClickLinkEvent onClickLink
        {
            get => _onClickLink;
            set => _onClickLink = value;
        }
 
        #region PopulateMesh
 
        private readonly UIVertex[] _tempVerts = new UIVertex[4];
        
        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            if (font == null)
            {
                return;
            }
 
            m_DisableFontTextureRebuiltCallback = true;
 
            var extents = rectTransform.rect.size;
 
            var settings = GetGenerationSettings(extents);
            settings.generateOutOfBounds = true;
            cachedTextGenerator.PopulateWithErrors(text, settings, gameObject);
 
            var verts = cachedTextGenerator.verts;
            var unitsPerPixel = 1 / pixelsPerUnit;
            var vertCount = verts.Count;
 
            if (vertCount <= 0)
            {
                toFill.Clear();
                return;
            }
 
            var roundingOffset = new Vector2(verts[0].position.x, verts[0].position.y) * unitsPerPixel;
            roundingOffset = PixelAdjustPoint(roundingOffset) - roundingOffset;
            toFill.Clear();
 
            if (roundingOffset != Vector2.zero)
            {
                for (var i = 0; i < vertCount; ++i)
                {
                    var tempVertsIndex = i & 3;
                    _tempVerts[tempVertsIndex] = verts[i];
                    _tempVerts[tempVertsIndex].position *= unitsPerPixel;
                    _tempVerts[tempVertsIndex].position.x += roundingOffset.x;
                    _tempVerts[tempVertsIndex].position.y += roundingOffset.y;
 
                    if (tempVertsIndex == 3)
                    {
                        toFill.AddUIVertexQuad(_tempVerts);
                    }
                }
            }
            else
            {
                for (var i = 0; i < vertCount; ++i)
                {
                    var tempVertsIndex = i & 3;
                    _tempVerts[tempVertsIndex] = verts[i];
                    _tempVerts[tempVertsIndex].position *= unitsPerPixel;
 
                    if (tempVertsIndex == 3)
                    {
                        toFill.AddUIVertexQuad(_tempVerts);
                    }
                }
            }
 
            var vertices = UIVerticesPool.Get();
            toFill.GetUIVertexStream(vertices);
 
            GenerateCharIndexMap(vertices.Count < text.Length * CharVertex);
 
            _links.Clear();
            TryAddMatchLink();
            GenerateHrefBoxes(ref vertices);
 
            toFill.Clear();
            toFill.AddUIVertexTriangleStream(vertices);
            
            DrawUnderLine(toFill);
            
            UIVerticesPool.Release(vertices);
 
            m_DisableFontTextureRebuiltCallback = false;
        }
 
        /// <summary>
        /// 生成超链接包围框
        /// </summary>
        /// <param name="vertices"></param>
        private void GenerateHrefBoxes(ref List<UIVertex> vertices)
        {
            var verticesCount = vertices.Count;
 
            for (var i = 0; i < _links.Count; i++)
            {
                var linkInfo = _links[i];
 
                var startIndex = _charIndexMap[linkInfo.StartIndex];
                var endIndex = _charIndexMap[linkInfo.StartIndex + linkInfo.Length - 1];
 
                for (var textIndex = startIndex; textIndex <= endIndex; textIndex++)
                {
                    var vertexStartIndex = textIndex * CharVertex;
                    if (vertexStartIndex + CharVertex > verticesCount)
                    {
                        break;
                    }
 
                    var min = Vector2.one * float.MaxValue;
                    var max = Vector2.one * float.MinValue;
 
                    for (var vertexIndex = 0; vertexIndex < CharVertex; vertexIndex++)
                    {
                        var vertex = vertices[vertexStartIndex + vertexIndex];
                        if (linkInfo.OverwriteColor)
                        {
                            vertex.color = linkInfo.Color;
                        }
                        vertices[vertexStartIndex + vertexIndex] = vertex;
 
                        var pos = vertices[vertexStartIndex + vertexIndex].position;
 
                        if (pos.y < min.y)
                        {
                            min.y = pos.y;
                        }
 
                        if (pos.x < min.x)
                        {
                            min.x = pos.x;
                        }
 
                        if (pos.y > max.y)
                        {
                            max.y = pos.y;
                        }
 
                        if (pos.x > max.x)
                        {
                            max.x = pos.x;
                        }
                    }
 
                    linkInfo.Boxes.Add(new Rect {min = min, max = max});
                }
 
                linkInfo.Boxes = CalculateLineBoxes(linkInfo.Boxes);
            }
        }
 
        /// <summary>
        /// 计算行包围框
        /// </summary>
        /// <param name="boxes"></param>
        /// <returns></returns>
        private static List<Rect> CalculateLineBoxes(List<Rect> boxes)
        {
            var lineBoxes = new List<Rect>();
            var lineStartIndex = 0;
 
            for (var i = 1; i < boxes.Count; i++)
            {
                if (boxes[i].xMin >= boxes[i - 1].xMin)
                {
                    continue;
                }
 
                lineBoxes.Add(CalculateAABB(boxes.GetRange(lineStartIndex, i - lineStartIndex)));
                lineStartIndex = i;
            }
 
            if (lineStartIndex < boxes.Count)
            {
                lineBoxes.Add(CalculateAABB(boxes.GetRange(lineStartIndex, boxes.Count - lineStartIndex)));
            }
 
            return lineBoxes;
        }
 
        private static Rect CalculateAABB(IReadOnlyList<Rect> rects)
        {
            var min = Vector2.one * float.MaxValue;
            var max = Vector2.one * float.MinValue;
 
            for (var i = 0; i < rects.Count; i++)
            {
                if (rects[i].xMin < min.x)
                {
                    min.x = rects[i].xMin;
                }
 
                if (rects[i].yMin < min.y)
                {
                    min.y = rects[i].yMin;
                }
 
                if (rects[i].xMax > max.x)
                {
                    max.x = rects[i].xMax;
                }
 
                if (rects[i].yMax > max.y)
                {
                    max.y = rects[i].yMax;
                }
            }
 
            return new Rect {min = min, max = max};
        }
 
        /// <summary>
        /// 生成字节索引映射
        /// </summary>
        /// <param name="verticesReduced"></param>
        private void GenerateCharIndexMap(bool verticesReduced)
        {
            if (_charIndexMap == null || _charIndexMap.Length < text.Length)
            {
                Array.Resize(ref _charIndexMap, text.Length);
            }
 
            if (!verticesReduced)
            {
                for (var i = 0; i < _charIndexMap.Length; i++)
                {
                    _charIndexMap[i] = i;
                }
                return;
            }
 
            var offset = 0;
            var inTag = false;
 
            for (var i = 0; i < text.Length; i++)
            {
                var character = text[i];
 
                if (inTag)
                {
                    offset--;
 
                    if (character == GreaterThan)
                    {
                        inTag = false;
                    }
                }
                else if (supportRichText && character == LesserThan)
                {
                    offset--;
                    inTag = true;
                }
                else if (_invisibleChars.Contains(character))
                {
                    offset--;
                }
 
                _charIndexMap[i] = Mathf.Max(0, i + offset);
            }
        }
 
        #region Under Line
 
        private void DrawUnderLine(VertexHelper vh)
        {
            foreach (var link in _links)
            {
                foreach (var rect in link.Boxes)
                {
                    var height = rect.height;
                    // 左下
                    var pos1 = new Vector3(rect.min.x, rect.min.y, 0);
                    // 右下
                    var pos2 = new Vector3(rect.max.x, rect.max.y, 0) - new Vector3(0, height, 0);
 
                    MeshUnderLine(vh, pos1, pos2, link.Color);
                }
            }
        }
 
        private void MeshUnderLine(VertexHelper vh, Vector2 startPos, Vector2 endPos, Color lineColor)
        {            
        	var extents = rectTransform.rect.size;
            var setting = GetGenerationSettings(extents);
 
            var underlineText = new TextGenerator();
            underlineText.Populate(underline, setting);
 
            var lineVer = underlineText.verts; //"￣"的的顶点数组
 
            var pos = new Vector3[4];
            pos[0] = startPos + new Vector2(-8, 0);
            pos[3] = startPos + new Vector2(-8, -4f);
            pos[2] = endPos + new Vector2(8, -4f);
            pos[1] = endPos + new Vector2(8, 0);
 
            if (lineVer.Count != 4) return;
            
            var tempVerts = new UIVertex[4];
            for (var i = 0; i < 4; i++)
            {
                tempVerts[i] = lineVer[i];
                tempVerts[i].color = lineColor;
                tempVerts[i].position = pos[i];
                tempVerts[i].uv0 = lineVer[i].uv0;
                tempVerts[i].uv1 = lineVer[i].uv1;
                tempVerts[i].uv2 = lineVer[i].uv2;
                tempVerts[i].uv3 = lineVer[i].uv3;
            }
 
            vh.AddUIVertexQuad(tempVerts);
        }
 
        #endregion
        #endregion
 
        private Vector3 CalculateLocalPosition(Vector3 position, Camera pressEventCamera)
        {
            if (!RootCanvas)
            {
                return Vector3.zero;
            }
 
            if (RootCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                return transform.InverseTransformPoint(position);
            }
 
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                position,
                pressEventCamera,
                out var localPosition
            );
 
            return localPosition;
        }
        
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            var localPosition = CalculateLocalPosition(eventData.position, eventData.pressEventCamera);
 
            foreach (var linkInfo in _links)
            {
                if (!linkInfo.Boxes.Any(t => t.Contains(localPosition))) continue;
                var subText = text.Substring(linkInfo.StartIndex, linkInfo.Length);
                var link = linkInfo.Link ?? subText;
                var content = linkInfo.Text ?? subText;
                linkInfo.Callback?.Invoke(link,content);
            }
        }
 
        #region Add Text Link
        
        /// <summary>
        /// 尝试添加超链接
        /// </summary>
        private void TryAddMatchLink()
        {
            foreach (var entry in linkRegexPattern)
            {
                var matches = Regex.Matches(text, entry.pattern, RegexOptions.Singleline);
                foreach (Match match in matches)
                {
                    var regex = new Regex(entry.pattern, RegexOptions.Singleline);
                    var regexMatch = regex.Match(match.Value);
                    var overwriteColor = entry.overwriteColor == true ? entry.color : (Color?)null;
                    if (regexMatch.Success)
                    {
                        var group = match.Groups[1];
                        AddLink(match.Index, group.Value,match.Value, overwriteColor, _onClickLink);
                    }
                    else
                    {
                        AddLink(match.Index, match.Value.Length, overwriteColor, _onClickLink);
                    }
                }
            }
        }
 
        private void CheckLinkException(int startIndex, int length, ClickLinkEvent onClick)
        {
            if (onClick == null)
            {
                throw new ArgumentNullException(nameof(onClick));
            }
 
            if (startIndex < 0 || startIndex > text.Length - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }
 
            if (length < 1 || startIndex + length > text.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
        }
        
        private void AddLink(int startIndex, int length, Color? linkColor, ClickLinkEvent onClick)
        {
            CheckLinkException(startIndex, length, onClick);
 
            _links.Add(new LinkInfo(startIndex, length, linkColor, onClick));
        }
        
        private void AddLink(int startIndex, string link, string content, Color? linkColor, ClickLinkEvent onClick)
        {
            CheckLinkException(startIndex, content.Length, onClick);
 
            _links.Add(new LinkInfo(startIndex, link, content, linkColor, onClick));
        }
 
        protected void AddLink(int startIndex, string link, string content, ClickLinkEvent onClick)
        {
            CheckLinkException(startIndex, content.Length, onClick);
 
            _links.Add(new LinkInfo(startIndex, link, content, onClick));
        }
        
        protected void CleanLink()
        {
            _links.Clear();
            linkRegexPattern.Clear();
        }
 
        #endregion
        
        #region Hyperlink_Test
 
        //#if Hyperlink_Test
        protected override void OnEnable()
        {
            base.OnEnable();
            onClickLink.AddListener(OnClickLinkText);
        }
 
        protected override void OnDisable()
        {
            base.OnDisable();
            onClickLink.RemoveListener(OnClickLinkText);
        }
 
        /// <summary>
        /// 当前点击超链接回调
        /// </summary>
        private void OnClickLinkText(string link,string content)
        {
            Debug.Log($"超链接信息：{link}\n{content}");
            Application.OpenURL(link);
        }
        //#endif
        #endregion
    }
}