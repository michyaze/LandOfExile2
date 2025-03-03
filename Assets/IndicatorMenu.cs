using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorMenu : BasicMenu
{
    public Text text;
    public void ShowIndicator(string t)
    {
        text.text = t;
        ShowMenu();
    }
}
