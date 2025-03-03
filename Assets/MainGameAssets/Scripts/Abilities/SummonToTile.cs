using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonToTile : Ability
{
    public Card cardTemplateToSummon;
    public bool opposingTeam;
    public bool addCopyEffect;
    public bool copyHP;
    public bool useAmountHP;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile.isMoveable())
        {
            Card templateCard = cardTemplateToSummon;
            if (templateCard == null)
            {
                templateCard = GetCard().cardTemplate;
            }

            Card newCard = null;
            if (opposingTeam)
            {
                newCard = sourceCard.player.GetOpponent().CreateCardInGameFromTemplate(templateCard);
            }
            else
            {
                newCard = sourceCard.player.CreateCardInGameFromTemplate(templateCard);
            }

            newCard.TargetTile(targetTile, false);
            if (copyHP)
            {
                if(newCard is Unit newUnit && sourceCard is Unit sourceUnit)
                {
                    newUnit.ChangeCurrentHP(this,sourceUnit.currentHP);
                }
            }else if (useAmountHP)
            {
                
                if(newCard is Unit newUnit )
                {
                    newUnit.ChangeCurrentHP(this,amount);
                }
            }
            if (addCopyEffect)
            {
                newCard.gameObject.AddComponent<TriggerCopyExhausts>();
            }
        
        }

    }
}
