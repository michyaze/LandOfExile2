using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCowardlyThug : Trigger
{
    public Effect blockEffectTemplate;
         

    public override void MinionSummoned(Minion minion)
    {
        ApplyBlockCharges();
    }

    public override void HeroDestroyed(Card sourceCard, Ability ability, int damageAmount, Hero hero)
    {
        ApplyBlockCharges();
    }

    public override void UnitMoved(Unit unit, Tile originalTile, Tile destinationTile)
    {
        ApplyBlockCharges();
    }

    void ApplyBlockCharges()
    {
        Unit unit = (Unit)GetCard();
        if (unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            foreach (Tile tile in unit.GetAdjacentTiles())
            {
                if (tile.GetUnit() != null && tile.GetUnit().IsPinned() && tile.GetUnit().player != unit.player)
                {

                    int amountOfCharges = 3;
                    if (unit.GetEffectsWithTemplate(blockEffectTemplate).Count > 0)
                    {
                        amountOfCharges -= unit.GetEffectsWithTemplate(blockEffectTemplate)[0].remainingCharges;
                    }
                    if (amountOfCharges > 0)
                        unit.ApplyEffect(GetCard(), this, blockEffectTemplate, 3);

                    return;
                }
            }
        }
    }
}
