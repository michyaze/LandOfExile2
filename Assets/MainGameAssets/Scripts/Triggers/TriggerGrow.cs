using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGrow : Trigger
{
    public int changePower = 1;
    public int changeHP = 1;

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card.UniqueID == "Green01" && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
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
