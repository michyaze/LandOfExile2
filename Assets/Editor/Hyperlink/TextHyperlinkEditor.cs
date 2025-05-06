using UnityEditor;
using UnityEditor.UI;
 
namespace Hyperlink.Editor
{
    [CustomEditor(typeof(TextHyperlink), true)]
    [CanEditMultipleObjects]
    public class TextHyperlinkEditor : TextEditor
    {
        private SerializedProperty _linkRegexPattern;
        private SerializedProperty _underline;
        private SerializedProperty _onClickLink;
        protected override void OnEnable()
        {
            base.OnEnable();
            _underline = serializedObject.FindProperty("underline");
            _linkRegexPattern = serializedObject.FindProperty("linkRegexPattern");
            _onClickLink = serializedObject.FindProperty("_onClickLink");
        }
 
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();
            EditorGUILayout.LabelField("Hyperlink", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_underline);
            EditorGUILayout.PropertyField(_linkRegexPattern);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_onClickLink);
            serializedObject.ApplyModifiedProperties();
        }
    }
}