using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReachDeep : Trigger
{
    public bool firstTriggerInstance = true;

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
      
        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (GetCard().player == card.player)
        {
            if (firstTriggerInstance && card.player.cardsInHand.Count == 0) {
                firstTriggerInstance = false;
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (card.player.cardsInHand.Count == 0)
                    {
                        card.player.DrawACard();
                    }
                });
            }
        }

    }

    public override void CardDiscarded(Card card, bool automaticDiscard)
    {
        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (GetCard().player == card.player && !automaticDiscard)
        {
            if (firstTriggerInstance && card.player.cardsInHand.Count == 0)
            {
                firstTriggerInstance = false;
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (card.player.cardsInHand.Count == 0)
                    {
                        card.player.DrawACard();
                    }
                });
            }
        }
    }

    public override void TurnStarted(Player player)
    {
        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (GetCard().player == player)
        {
            firstTriggerInstance = true;
        }
    }
}

