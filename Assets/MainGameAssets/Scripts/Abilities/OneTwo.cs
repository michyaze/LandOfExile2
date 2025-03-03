using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTwo : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (GetCard().player.cardsInDiscard.Count > 0)
        {
            Card nextcard = GetCard().player.cardsInDiscard[GetCard().player.cardsInDiscard.Count-1];
            if (nextcard is Castable && nextcard.cardTags.Contains(MenuControl.Instance.spellTag))
            {
                nextcard.PutIntoZone(MenuControl.Instance.battleMenu.hand);
                if (nextcard.CanTarget(targetTile))
                {
                    nextcard.PutIntoZone(MenuControl.Instance.battleMenu.discard);
                    MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                    {
                        nextcard.PutIntoZone(MenuControl.Instance.battleMenu.hand);
                        if (nextcard.CanTarget(targetTile))
                        {
                            nextcard.TargetTile(targetTile, false);
                        }
                        else
                        {
                            nextcard.PutIntoZone(MenuControl.Instance.battleMenu.discard);
                        }
                    });
                }
                else
                {
                    nextcard.PutIntoZone(MenuControl.Instance.battleMenu.discard);
                }
            }
        }
    }
}
