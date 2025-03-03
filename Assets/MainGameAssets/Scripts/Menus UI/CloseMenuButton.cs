using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;

public class CloseMenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UIButton>().OnClick.OnTrigger.Action = (o) =>
        {
            GetComponentInParent<BasicMenu>().CloseMenu();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
