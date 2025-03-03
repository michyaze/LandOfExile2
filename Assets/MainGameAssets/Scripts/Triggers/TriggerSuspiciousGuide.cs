using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSuspiciousGuide : Trigger
{
    public Card newSpawnTemplate;
    public Effect dashTemplate;
    
    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card.UniqueID == "Green01" && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                bool oldMinionHasAttacked = ((Minion)GetCard()).remainingActions < ((Minion)GetCard()).initialActions;

                Minion newMinion = (Minion)GetCard().player.CreateCardInGameFromTemplate(newSpawnTemplate);

                foreach (Effect effect in ((Unit)GetCard()).currentEffects.ToArray())
                {

                    effect.transform.parent = newMinion.transform;
                    newMinion.currentEffects.Add(effect);

                }

                int oldMoves = ((Unit)GetCard()).remainingMoves;
                int oldActions = ((Unit)GetCard()).remainingActions;

                Tile oldTile = ((Unit)GetCard()).GetTile();
                ((Unit)GetCard()).PutIntoZone(MenuControl.Instance.battleMenu.removedFromGame);

                newMinion.TargetTile(oldTile, false);
                newMinion.RemoveEffect(GetCard(), this, newMinion.GetEffectsOfType<SummoningSick>()[0]);
                newMinion.remainingActions = oldActions;
                newMinion.remainingMoves = oldMoves;

                //Everything same above plus this part below -- If you have already attacked this turn when you Shapechange, gain +1 Dash. 

                if (oldMinionHasAttacked)
                {
                    newMinion.ApplyEffect(GetCard(), this, dashTemplate, 1);
                    newMinion.remainingMoves += 1;
                }

            });
        }

    }
}
