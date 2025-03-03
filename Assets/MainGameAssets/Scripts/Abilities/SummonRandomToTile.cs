using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonRandomToTile : Ability
{
    public List<Card> cardTemplateToSummon;
    public bool opposingTeam;
    public bool addCopyEffect;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile.isMoveable())
        {
            Card templateCard = cardTemplateToSummon.RandomItem();
            if (templateCard == null)
            {
                templateCard = GetCard().cardTemplate;
            }

            if (opposingTeam)
            {
                Card newCard = sourceCard.player.GetOpponent().CreateCardInGameFromTemplate(templateCard);
                newCard.TargetTile(targetTile, false);
                if (addCopyEffect) newCard.gameObject.AddComponent<TriggerCopyExhausts>();
            }
            else
            {
                Card newCard = sourceCard.player.CreateCardInGameFromTemplate(templateCard);
                newCard.TargetTile(targetTile, false);
                if (addCopyEffect) newCard.gameObject.AddComponent<TriggerCopyExhausts>();
            }
        
        }

    }
}
