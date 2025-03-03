using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoppleganger : Trigger
{
    public override void MinionSummoned(Minion minion)
    {
        if (((Unit)GetCard()).GetZone() == MenuControl.Instance.battleMenu.board)
        {
            if (minion.player != GetCard().player)
            {
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    Tile tile = ((Unit)GetCard()).GetTile();
                    GetCard().ExhaustThisCard();

                    Minion newCard = (Minion)GetCard().player.CreateCardInGameFromTemplate(minion.cardTemplate);

                    newCard.TargetTile(tile, false);
                    newCard.RemoveEffect(null, null, newCard.GetEffectsWithTemplate(MenuControl.Instance.battleMenu.summoningSickEffectTemplate)[0]);

                });
            }
        }
    }
}
