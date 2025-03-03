using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerRebirth : Trigger
{

    public Effect templateEffect;
    public int charges;

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {

        if (!(GetCard() is Unit)) return;


        Unit unit = (Unit)GetCard();

        Tile originalTile = unit.GetTile();

        if (originalTile != null)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                if (originalTile.isMoveable()) //Space still empty
                {

                    unit.player.PutCardIntoZone(unit, MenuControl.Instance.battleMenu.board);

                    unit.InitializeUnit(false);

                    unit.RemoveEffect(null, null, unit.GetEffectsWithTemplate(MenuControl.Instance.battleMenu.summoningSickEffectTemplate)[0]);

                    unit.MoveToTile(originalTile);

                    unit.ApplyEffect(unit, this, templateEffect, charges);
                }
            });
        }
    }

}
