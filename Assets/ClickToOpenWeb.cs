using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class ClickToOpenWeb : MonoBehaviour
{
    public string webSite2;
    
    public string webSite = "https://store.steampowered.com/app/3077400";
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (LocalizationManager.CurrentLanguage == MenuControl.Languages.Chinese.ToString() || webSite2 == null ||webSite2 .Length == 0)
            {
                
                Application.OpenURL(webSite);
            }
            else
            {
                
                Application.OpenURL(webSite2);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
