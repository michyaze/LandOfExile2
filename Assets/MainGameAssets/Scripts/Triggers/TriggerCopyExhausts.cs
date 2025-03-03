using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCopyExhausts : Trigger
{

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion == GetCard())
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                if (minion.GetZone() != MenuControl.Instance.battleMenu.board)
                    minion.ExhaustThisCard();
            });
        }
    }

    public override void CardDiscarded(Card card, bool automaticDiscard)
    {
        if (card == GetCard())
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                if (card.GetZone() != MenuControl.Instance.battleMenu.board)
                    card.ExhaustThisCard();
            });
        }
    }

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card == GetCard())
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                if (card.GetZone() != MenuControl.Instance.battleMenu.board)
                    card.ExhaustThisCard();
            });
        }
    }
}
