using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPredator : Trigger
{
    public bool hasTriggered;

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {
        if (initialAttack && attacker == GetCard() && !hasTriggered && attacker.GetPower(attacker,defender) > defender.GetPower())
        {
            Unit thisCard = attacker;
            Unit defendingCard = defender;
            hasTriggered = true;
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                if (defendingCard.GetTile() != null)
                {
                    thisCard.ForceAttack(defendingCard.GetTile());
                }
                hasTriggered = false;
            });
        }
    }

}
