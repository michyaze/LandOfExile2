using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedDropdownItem : MonoBehaviour
{
    private Text label;
    // Start is called before the first frame update
    void Start()
    {
        label = GetComponentInChildren<Text>();
        label.text = MenuControl.Instance.GetLocalizedString(label.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
