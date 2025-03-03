using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceHeal : EventChoice
{

    public int healAmount;

    public override void PerformChoice()
    {

        MenuControl.Instance.heroMenu.hero.Heal(null, null, healAmount);
        CloseEvent();

    }
}
