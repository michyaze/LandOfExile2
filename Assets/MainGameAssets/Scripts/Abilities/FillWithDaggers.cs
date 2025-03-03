using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillWithDaggers : Ability
{

    public int damageAmount;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit unit = targetTile.GetUnit();
        int healthAmount = unit.GetInitialHP();


        Effect markedEffect = null;
        foreach (Effect effect in unit.currentEffects)
        {
            if (effect.UniqueID == "RogueEffect01")
            {
                markedEffect = effect;
                break;
            }
        }


        unit.SufferDamage(GetCard(), this, damageAmount);

        if (unit.GetZone() != MenuControl.Instance.battleMenu.board)
        {
            if (markedEffect != null)
            {
                for (int ii = 0; ii < 1 + GetCard().player.GetHero().GetEffectsByID("RogueMid02").Count; ii += 1)
                {
                    MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    foreach (Tile tile in targetTile.GetAdjacentTilesLinear())
                    {
                        if (tile.GetUnit() != null)
                        {
                            tile.GetUnit().SufferDamage(GetCard(), this, healthAmount);
                        }
                    }
                });
                }

            }
        }

    }
}
