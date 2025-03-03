using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScaleUp : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        var ySizeDelta = GetComponent<RectTransform>().sizeDelta.y;
        GetComponent<RectTransform>().sizeDelta = new Vector2( GetComponent<RectTransform>().sizeDelta.x*2, ySizeDelta*2);
        GetComponent<Text>().fontSize = GetComponent<Text>().fontSize * 2;
        GetComponentInParent<VerticalLayoutGroup>().padding = new RectOffset(0,0,-(int)ySizeDelta/2,-(int)ySizeDelta/2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
