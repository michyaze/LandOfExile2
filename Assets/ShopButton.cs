using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public GameObject buttonShow;
    public GameObject buttonHide;
    public Text textHide;
    public Text textShow;

    public void SetText(string text)
    {
        textShow.text = text;
        textHide.text = text;
    }
}
