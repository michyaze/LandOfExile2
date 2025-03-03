using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInfestedSummonToTile : Trigger
{
    public Card templateCard;


    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {

        if (minion == GetCard())
        {

            Tile tile = minion.GetTile();

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {

                if (tile.isMoveable())
                {

                    Card newCard = minion.player.GetOpponent().CreateCardInGameFromTemplate(templateCard);
                    newCard.TargetTile(tile, false);
                    //((Minion)newCard).RemoveEffect(null, null, ((Minion)newCard).GetEffectsWithTemplate(MenuControl.Instance.battleMenu.summoningSickEffectTemplate)[0]);

                }
            });

        }

    }
}
