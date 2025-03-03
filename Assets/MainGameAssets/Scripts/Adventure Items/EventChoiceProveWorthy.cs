using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceProveWorthy : EventChoice
{
    public int powerGain;
    public int lifeGain;

    public int maxDeckSize = 8;

    public override void PerformChoice()
    {
        if (MenuControl.Instance.heroMenu.cardsOwned.Count < maxDeckSize + 1)
        {
            MenuControl.Instance.heroMenu.hero.initialPower += powerGain;
            MenuControl.Instance.heroMenu.hero.currentHP += lifeGain;
            MenuControl.Instance.heroMenu.hero.initialHP += lifeGain;

            MenuControl.Instance.dataControl.SaveData();

            CloseEvent();
        }
    }
}
