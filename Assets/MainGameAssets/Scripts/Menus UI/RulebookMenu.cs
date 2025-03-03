using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulebookMenu : BasicMenu
{
    public Text textBody;
    public Transform topicPanel;
    public Color onColor;
    public Color offColor;

    public override void ShowMenu()
    {
        base.ShowMenu();
        ShowRulesOnTopic("RulebookAdventuring");

        MenuControl.Instance.LogEvent("OpenRulebook");
    }

    public void ShowRulesOnTopic(string topicString)
    {
        textBody.text = MenuControl.Instance.GetLocalizedString(topicString+"Text");

        if (topicString == "RulebookKeywords")
        {
            string stringToShow = "";
            foreach (string keyword in MenuControl.Instance.infoMenu.keywordStrings)
            {
                string keyWordName = MenuControl.Instance.GetLocalizedString(keyword + "KeywordName", keyword);
                string keyWordDescription = MenuControl.Instance.GetLocalizedString(keyword + "KeywordDescription");

                stringToShow += "<color=white>" + keyWordName + "</color>";
                stringToShow += "\n" + keyWordDescription + "\n\n";
            }

            textBody.text = stringToShow;

        }

#if !UNITY_STANDALONE
        textBody.fontSize = 48;
#endif

        textBody.transform.parent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        LayoutRebuilder.ForceRebuildLayoutImmediate(textBody.transform.parent.GetComponent<RectTransform>());

        foreach (Transform child in topicPanel)
        {
            child.GetComponentInChildren<Text>().text = MenuControl.Instance.GetLocalizedString(child.name + "Title");
            child.GetComponentInChildren<Text>().color = (child.name == topicString ? onColor : offColor);

        }
    }
}
