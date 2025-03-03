using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public Card GetCard()
    {
        Card card = GetComponentInParent<Card>();

        // if (card is DeprecatedWeapon) //Incase of weapon
        // {
        //     card = card.transform.parent.GetComponentInParent<Card>();
        // }
        if (card is NewWeapon) //Incase of weapon
        {
            card = card.transform.parent.GetComponentInParent<Card>();
            if (card == null)
            {
                card = GetComponentInParent<Card>();
            }
        }

        if (card == null)
        {
            card = GetComponent<Card>();
        }

        return card;
    }
    
    public virtual float PerformAnimationTime(Card sourceCard)
    {
        return MenuControl.Instance.battleMenu.GetPlaySpeed();
    }

    public Effect GetEffect()
    {
        Effect effect = GetComponentInParent<Effect>();

        return effect;
    }

    public TargetValidator targetValidator;

    public TargetValidator GetTargetValidator()
    {
        
        var newValidator = targetValidator;
        if (newValidator == null)
        {
            return newValidator;
        }
        
        
        if (MenuControl.Instance.battleMenu.inBattle)
        {
            foreach (AttackRangeModifier powerModifier in MenuControl.Instance.battleMenu
                         .GetComponentsInChildren<AttackRangeModifier>())
            {
                if (powerModifier.enabled)
                {
                    newValidator = powerModifier.ModifyRange(this, newValidator);
                }
            }
        }
        
        return newValidator;
    }

    public virtual void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

    }
    public virtual bool CanTargetTile(Card card, Tile tile)
    {
        foreach (PreventTargeting effect in MenuControl.Instance.battleMenu.GetComponentsInChildren<PreventTargeting>())
        {
            if (!effect.CanTarget(card, tile)) return false;
        }

        if (GetTargetValidator())
        {
            Unit unit = null;

            if (card.player != null) unit = card.player.GetHero();
            else if (MenuControl.Instance.battleMenu.usingIntentSystem) unit = MenuControl.Instance.battleMenu.playerAI.GetHero();
            if (card is Unit) unit = (Unit)card;
            return GetTargetValidator().CanUnitTargetTile(unit, tile);
        }

        return false; 
    }

    public virtual bool CanDiscard(Card card, bool autoEndTurn)
    {
        return true;
    }
}