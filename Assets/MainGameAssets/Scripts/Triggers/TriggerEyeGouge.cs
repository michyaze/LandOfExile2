using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEyeGouge : Trigger
{
    public int powerToReduce;
    public int enemiesToMarkOnDiscard;
    public Effect markedEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile.GetUnit() != null)
        {
            targetTile.GetUnit().ChangePower(this, targetTile.GetUnit().currentPower - powerToReduce);


            if (targetTile.GetUnit().GetEffectsWithTemplate(markedEffectTemplate).Count > 0)
            {
                targetTile.GetUnit().GetEffectsWithTemplate(markedEffectTemplate)[0].ConsumeCharges(this, 1);

                for (int ii = 0; ii < 1 + GetCard().player.GetHero().GetEffectsByID("RogueMid02").Count; ii += 1)
                {
                    MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                    {
                        PerformAbility(sourceCard, targetTile, amount);
                    });
                }

            }
        }
    }

    public override void CardDiscarded(Card card, bool automaticDiscard)
    {
        if (!automaticDiscard && card == GetCard())
        {
            List<Unit> adjacentEnemies = new List<Unit>();
            foreach (Tile tile in GetCard().player.GetHero().GetAdjacentTiles())
            {
                if (tile.GetUnit() != null && tile.GetUnit().player != GetCard().player)
                {
                    adjacentEnemies.Add(tile.GetUnit());
                }
            }

            adjacentEnemies.Shuffle();
            for (int ii = 0; ii < enemiesToMarkOnDiscard; ii += 1)
            {
                adjacentEnemies[ii].ApplyEffect(GetCard(), this, markedEffectTemplate, 1);
            }
        }
    }

}
