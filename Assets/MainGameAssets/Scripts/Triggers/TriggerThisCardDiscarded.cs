using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerThisCardDiscarded : Trigger
{
    public bool immediately;
    public bool manuallyOnly;
    public bool endOfTurnOnly;
    public Ability otherAbilityToPerform;

    public override void CardDiscarded(Card card, bool automaticDiscard)
    {
        if (manuallyOnly && automaticDiscard)
            return;
        
        if (endOfTurnOnly && !automaticDiscard)
            return;
        
        if (card != GetCard()) return;

        if (immediately)
        {
            otherAbilityToPerform.PerformAbility(GetCard(), GetCard().player.GetHero().GetTile());
        }
        else
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                otherAbilityToPerform.PerformAbility(GetCard(), GetCard().player.GetHero().GetTile());
            });
        }
    }
}
