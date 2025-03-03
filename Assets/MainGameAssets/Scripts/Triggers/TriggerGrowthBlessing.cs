using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGrowthBlessing : Trigger
{
    

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        int changePower = GetEffect().remainingCharges;
        int changeHP = GetEffect().remainingCharges;

        if (card.UniqueID == "Green01")
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {

                if (changePower != 0)
                    ((Unit)GetCard()).ChangePower(this, ((Unit)GetCard()).currentPower + changePower);

                if (changeHP != 0)
                {
                    ((Unit)GetCard()).ChangeCurrentHP(this, ((Unit)GetCard()).currentHP + changeHP);
                    if (changeHP > 0)
                    {
                        ((Unit)GetCard()).ChangeMaxHP(this, ((Unit)GetCard()).GetInitialHP() + changeHP);
                    }
                }

            });
        }
    }
}
