using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using Pool;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationFontSizeAdjust : MonoBehaviour
{

    public string[] languages;

    public int[] sizeOffsets;
    private int originalOffset;

    private Dictionary<string, int> languageToOffset;

    private void Awake()
    {
        originalOffset = GetComponent<Text>().fontSize;
    }

    // Start is called before the first frame update
    void Start()
    {
        languageToOffset = new Dictionary<string, int>();
        for (int i = 0; i < languages.Length; i++)
        {
            languageToOffset[languages[i]] = sizeOffsets[i];
        }
        EventPool.OptIn("ChangeLanguage",updateSize);
        updateSize();
    }

    void updateSize()
    {
        var currentLanguage = LocalizationManager.CurrentLanguage;
        if (languageToOffset.ContainsKey(currentLanguage))
        {
            GetComponent<Text>().fontSize = originalOffset + languageToOffset[currentLanguage];
        }
        else
        {
            GetComponent<Text>().fontSize = originalOffset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
