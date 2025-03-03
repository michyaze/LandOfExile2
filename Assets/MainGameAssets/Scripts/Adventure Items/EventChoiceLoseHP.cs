using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceLoseHP : EventChoice
{

    public int lifeLostAmount;

    public override void PerformChoice()
    {
        MenuControl.Instance.heroMenu.hero.currentHP -= lifeLostAmount;

        MenuControl.Instance.dataControl.SaveData();

        CloseEvent();

        MenuControl.Instance.adventureMenu.ContinueAdventure();

    }
}
