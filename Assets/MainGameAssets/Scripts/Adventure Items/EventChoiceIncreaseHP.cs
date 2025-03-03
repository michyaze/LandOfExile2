using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceIncreaseHP : EventChoice
{

    public int lifeIncreaseAmount;

    public override void PerformChoice()
    {
        MenuControl.Instance.heroMenu.hero.initialHP+=(lifeIncreaseAmount);
        MenuControl.Instance.heroMenu.hero.currentHP+=(lifeIncreaseAmount);

        MenuControl.Instance.dataControl.SaveData();

        CloseEvent();

        MenuControl.Instance.adventureMenu.ContinueAdventure();

    }
}
