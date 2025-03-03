using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceGainXP : EventChoice
{
    public int amountToGain;

    public override void PerformChoice()
    {

        MenuControl.Instance.heroMenu.AddXP(amountToGain);
        CloseEvent();
        MenuControl.Instance.adventureMenu.ContinueAdventure();
    }
}
