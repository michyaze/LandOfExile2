using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBurningWill : Trigger
{
    public Effect burnEffectTemplate;
    public int charges;
    public bool canTrigger;

    public override void PlayerShuffledDeck(Player player)
    {
        if (GetCard().player == player && MenuControl.Instance.battleMenu.currentRound > 0 && player.cardsInDeck.Count > 0)
        {
            if (canTrigger)
            {
                canTrigger = false;
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    foreach (Unit unit in player.GetOpponent().cardsOnBoard.ToArray())
                    {
                    //unit.SufferDamage(GetCard(), this, damageAmount);
                    unit.ApplyEffect(GetCard(), this, burnEffectTemplate, charges);
                    }

                });
            }
        }
    }

    public override void TurnStarted(Player player)
    {
        canTrigger = true;
    }
}
