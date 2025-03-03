using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ChangeLogMenu : BasicMenu
{
    public RectTransform content;
    public Text textBody;

    public override void ShowMenu()
    {
        base.ShowMenu();
        content.anchoredPosition = Vector2.zero;

#if !UNITY_STANDALONE
        textBody.fontSize = 48;
#endif
    }
}
