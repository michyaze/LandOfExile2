using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArcaneMastery : Trigger
{

    public Effect spellBleedTemplate;

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card.player == GetCard().player && tile.GetUnit() != null && tile.GetUnit().player != GetCard().player)
        {
            Unit unit = tile.GetUnit();

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                if (unit.GetZone() == MenuControl.Instance.battleMenu.board)
                {
                    unit.ApplyEffect(GetCard(), this, spellBleedTemplate, GetEffect().remainingCharges);
                }
            });
        }
    }

  
}
