using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EventChoiceShrine : EventChoice
{
    public EventDefinition confirmationDefinition;
    [FormerlySerializedAs("notEnoughGoldDefinition")] public EventDefinition notEnoughStoneDefinition;
    public int healAmount;
    public bool costsStone;
    public int flareStoneCost = 3;

    public override void PerformChoice()
    {
        if (costsStone)
        {
            if (MenuControl.Instance.heroMenu.flareStones >= flareStoneCost)
            {
                MenuControl.Instance.heroMenu.consumeFlareStone(flareStoneCost);
                DoHeal();
                return;
            }
            else
            {
                MenuControl.Instance.eventMenu.ShowEvent(notEnoughStoneDefinition);
                return;
            }
        }

        DoHeal();
    }

    void DoHeal()
    {
        if (healAmount > 0)
        {
            MenuControl.Instance.heroMenu.hero.Heal(null, null, healAmount);
            MenuControl.Instance.eventMenu.ShowEvent(confirmationDefinition);
        }
    }
}
