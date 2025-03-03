using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoicePerformAbility : EventChoice
{
    

    public override void PerformChoice()
    {
        if (performAnotherAbility)
        {
            performAnotherAbility.PerformAbility(null,null,0);
        }
        CloseEvent();
        MenuControl.Instance.dataControl.SaveData();
        
    }

}
