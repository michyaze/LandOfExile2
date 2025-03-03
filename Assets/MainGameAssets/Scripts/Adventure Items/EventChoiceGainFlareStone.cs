using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceGainFlareStone : EventChoice
{
    public int flareStoneCount = 3;

    public override void PerformChoice()
    {
        
        MenuControl.Instance.heroMenu.addFlareStone(flareStoneCount);
        
        MenuControl.Instance.dataControl.SaveData();
        CloseEvent();
    }

}
