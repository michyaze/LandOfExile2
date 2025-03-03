using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceSkipPortal : EventChoice
{

    public override void PerformChoice()
    {
        MenuControl.Instance.ShowNotification(null, MenuControl.Instance.GetLocalizedString("ChoiceLeavePortalCardName"), MenuControl.Instance.GetLocalizedString("ChoiceLeavePortalCardDescription"), true, false, true);
        CloseEvent();
    }

}
