using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TriggerSoulTrap : Trigger
{
    public Card gruntTemplate;

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion.player != GetCard().player)
        {
            Tile oldTile = minion.GetTile();
            
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                minion.RemoveFromGame();
                if (oldTile.GetUnit() == null)
                {
                    Card newCard = GetCard().player.CreateCardInGameFromTemplate(gruntTemplate);
                    newCard.TargetTile(oldTile, false);
                }

            });

        }
    }
}
