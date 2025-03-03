using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryCardTypeCell : MonoBehaviour
{
    public Color textSelectColor;
    public Color textDeselectColor;
    
    public Text textLabel;
    public Image icon;
    public void select()
    {
        
        var color = Color.white;
        icon.color = color;
        textLabel.color = textSelectColor;
    }

    public void deselect()
    {
        var color = Color.white;
        color.a = 1f / 2;
        icon.color = color;
        textLabel.color = textDeselectColor;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
