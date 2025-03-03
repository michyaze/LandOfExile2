using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJungleSplitter : Trigger
{
    public Card newSpawnTemplate;
    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion == GetCard())
        {
            Minion newMinion = (Minion)minion.player.CreateCardInGameFromTemplate(newSpawnTemplate);

            foreach (Effect effect in minion.currentEffects.ToArray())
            {
                if (!(effect is SummoningSick))
                {
                    effect.transform.parent = newMinion.transform;
                    minion.currentEffects.Remove(effect);
                    newMinion.currentEffects.Add(effect);
                }
                else
                {
                    minion.RemoveEffect(GetCard(), this, effect);
                }
            }
            
            
            Tile tile = minion.GetTile();
            minion.PutIntoZone(MenuControl.Instance.battleMenu.discard);
            newMinion.TargetTile(tile, false);

        }
    }
}
