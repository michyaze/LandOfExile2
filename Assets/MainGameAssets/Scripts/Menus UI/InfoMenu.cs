using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keywordData
{
    public string afterKeywords;
    public List<string> keywords;
}
public class InfoMenu : BasicMenu
{
    public Image iconImage;
    public Text textShown;
    public GameObject effectInfoPrefab;
    public GameObject keywordInfoPrefab;
    public List<string> keywordStrings = new List<string>();

    public Transform rightPanelAnchor;

    public List<GameObject> panels = new List<GameObject>();

    public CardDescriptionPanel cardDescriptionPanelPrefab;
    public CardDescriptionPanel masterCardDescription;

    public TileDescriptionPanelPrefab tileDescriptionPanelPrefab;
    public TileDescriptionPanelPrefab masterTileDescription;

    public Dictionary<string, keywordData> keywordsText = new Dictionary<string, keywordData>();

    public keywordData GetKeywordData(string textToShow)
    {
        if (keywordsText.ContainsKey(textToShow))
        {
            
        }
        else
        {
            AddKeywordsText(textToShow);

        }
        return keywordsText[textToShow];
    }
    public void AddKeywordsText(string textToShow)
    {
        var originText = textToShow;
        List<string> newKeywords = new List<string>();
        
        foreach (string keyword in MenuControl.Instance.infoMenu.keywordStrings)
        {
            string keyWordName = MenuControl.Instance.GetLocalizedString(keyword + "KeywordName", keyword);
            if (textToShow.Contains(keyWordName + "ed") )
            {
                textToShow = textToShow.Replace(keyWordName + "ed", "<color=#86979c>" + keyWordName + "ed"+ "</color>");
                newKeywords.Add(keyword);
                //textToShow = AddKeyword(keyWordName + "ed", keyword, keyWordName, keyWordDescription, textToShow);
            }
            else if (textToShow.Contains(keyWordName + "d") )
            {
                textToShow =  textToShow.Replace(keyWordName + "d", "<color=#86979c>" + keyWordName+ "d" + "</color>");
                newKeywords.Add(keyword);
                //textToShow = AddKeyword(keyWordName + "d", keyword, keyWordName, keyWordDescription, textToShow);
            }
            else if (textToShow.Contains(keyWordName) )
            {
               //  textToShow = Regex.Replace(textToShow, keyWordName, $"<color=#86979c>{keyWordName}</color>", RegexOptions.None);
                textToShow = textToShow.Replace(keyWordName, "<color=#86979c>" + keyWordName + "</color>");
                
                newKeywords.Add(keyword);
                //textToShow = AddKeyword(keyWordName, keyword, keyWordName, keyWordDescription, textToShow);
            }
            else if (textToShow.Contains(keyword)) //If translation failed
            {
                textToShow = textToShow.Replace(keyword, "<color=#86979c>" + keyword + "</color>");
                newKeywords.Add(keyword);
                //textToShow = AddKeyword(keyword, keyword, keyWordName, keyWordDescription, textToShow);
            }
        }

        keywordsText[originText] = new keywordData() { afterKeywords= textToShow, keywords=newKeywords };
    }

    public void ShowInfo(Tile tile)
    {
        ShowMenu();

        if (masterTileDescription != null)
        {
            Destroy(masterTileDescription.gameObject);
        }

        masterTileDescription = Instantiate(tileDescriptionPanelPrefab, transform);


        // Vector2 viewportPoint =
        //     Camera.main.WorldToViewportPoint(new Vector3(position.x + 2.4f * (position.x < 0 ? 1f : -1f), 0f,
        //         0f)); //convert game object position to VievportPoint
        masterTileDescription.transform.localScale = Vector3.one * 1.3f;
        // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
        // masterCardDescription.GetComponent<RectTransform>().anchorMin = viewportPoint;
        // masterCardDescription.GetComponent<RectTransform>().anchorMax = viewportPoint;
        // masterCardDescription.transform.position = new Vector3(position.x, 0, 0);
        // masterCardDescription.showCardView = canRightClick;
        masterTileDescription.RenderPanel(tile);
    }
    public void ShowInfo(VisibleCard vc,bool canRightClick = true)
    {
        ShowInfo(vc.card, vc.transform.position, vc.isEnemy,canRightClick);
    }

    public void ShowInfo(Card card, Transform parent,bool canRightClick = true)
    {
        ShowInfo(card, parent.position, false,canRightClick);
    }

    public override void ShowMenu()
    {
        //if (MenuControl.Instance.settingsMenu.playSpeedSlider.value == MenuControl.Instance.settingsMenu.playSpeedSlider.maxValue)
        //{

        GetComponent<Doozy.Engine.UI.UIView>().Show(true);
        //return;
        //}
        //base.ShowMenu();
    }

    public void ShowInfo(Card card, Vector3 position, bool isEnemy,bool canRightClick = true)
    {
        ShowMenu();

        if (masterCardDescription != null)
        {
            Destroy(masterCardDescription.gameObject);
        }

        masterCardDescription = Instantiate(cardDescriptionPanelPrefab, transform);

        masterCardDescription.isEnemy = isEnemy;

        Vector2 viewportPoint =
            Camera.main.WorldToViewportPoint(new Vector3(position.x + 2.4f * (position.x < 0 ? 1f : -1f), 0f,
                0f)); //convert game object position to VievportPoint
        masterCardDescription.transform.localScale = Vector3.one * 1.3f;
        // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
        // masterCardDescription.GetComponent<RectTransform>().anchorMin = viewportPoint;
        // masterCardDescription.GetComponent<RectTransform>().anchorMax = viewportPoint;
        masterCardDescription.transform.position = new Vector3(position.x, 0, 0);
        masterCardDescription.showCardView = canRightClick;
        masterCardDescription.RenderPanel(card, position.x < 0);
        
        
    }

    public override void HideMenu(bool instantly = false)
    {
        base.HideMenu(instantly);
        if (masterCardDescription != null)
        {
            Destroy(masterCardDescription.gameObject);
            masterCardDescription = null;
        }
    }
}