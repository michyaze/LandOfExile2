using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingNotification : MonoBehaviour
{

    public void CloseNotification()
    {
        MenuControl.Instance.BlockingPopupDismissed();

    }

}
