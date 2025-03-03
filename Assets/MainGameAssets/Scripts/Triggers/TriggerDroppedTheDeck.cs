using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDroppedTheDeck : Trigger
{
    public override void CardDiscarded(Card card, bool automaticDiscard)
    {
        if (!automaticDiscard && !card.cardTags.Contains(MenuControl.Instance.potionTag))
        {
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                if (trigger != this)
                {
                    trigger.CardDiscarded(card, automaticDiscard);
                }
            }
        }
    }
}
