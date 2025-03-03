using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CreditsMenu : BasicMenu
{
    public RectTransform content;

    public override void ShowMenu()
    {
        base.ShowMenu();
        content.anchoredPosition = Vector2.zero;
    }
}
