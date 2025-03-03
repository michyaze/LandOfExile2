using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<UIButton>().OnClick.OnTrigger.Event.AddListener(() =>
        {
            MenuControl.Instance.settingsMenu.OpenMenu();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
