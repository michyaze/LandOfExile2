using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHelmSniper : Trigger
{

    public bool hasSniped;

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {

        Minion helmSniper = (Minion)GetCard();

        if (helmSniper.GetZone() != MenuControl.Instance.battleMenu.board || !initialAttack) return;

        if (attacker != helmSniper && attacker.player == helmSniper.player && defender.player != helmSniper.player)
        {
            bool inRange = false;
            int range = 0;
            if (helmSniper.activatedAbility is Attack && helmSniper.activatedAbility.GetTargetValidator() is TargetLinear)
                range = ((TargetLinear)helmSniper.activatedAbility.GetTargetValidator()).range;

            //Check if it's in range
            for (int ii = 1; ii <= range; ii += 1)
            {
                if (helmSniper.GetTile().GetTileUp(ii) == defender.GetTile() || (defender is LargeHero && ((LargeHero)defender).GetTiles().Contains(helmSniper.GetTile().GetTileUp(ii))))
                {
                    inRange = true;
                }
                if (helmSniper.GetTile().GetTileLeft(ii) == defender.GetTile() || (defender is LargeHero && ((LargeHero)defender).GetTiles().Contains(helmSniper.GetTile().GetTileLeft(ii))))
                {
                    inRange = true;
                }
                if (helmSniper.GetTile().GetTileRight(ii) == defender.GetTile() || (defender is LargeHero && ((LargeHero)defender).GetTiles().Contains(helmSniper.GetTile().GetTileRight(ii))))
                {
                    inRange = true;
                }
                if (helmSniper.GetTile().GetTileDown(ii) == defender.GetTile() || (defender is LargeHero && ((LargeHero)defender).GetTiles().Contains(helmSniper.GetTile().GetTileDown(ii))))
                {
                    inRange = true;
                }

            }

            if (inRange && !hasSniped)
            {
                hasSniped = true;
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (defender.GetZone() == MenuControl.Instance.battleMenu.board)
                        helmSniper.ForceAttack(defender.GetTile());
                });
            }
        }

    }

    public override void TurnEnded(Player player)
    {
        if (player == GetCard().player)
            hasSniped = false;
    }
}
