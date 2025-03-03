using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Hero : Unit
{
    public List<Effect> startingEffects = new List<Effect>();
    public List<int> startingEffectCharges = new List<int>();
    
    
    public List<Effect> tempStartingEffects = new List<Effect>();
    public List<int> tempStartingEffectCharges = new List<int>();
    
    
    public IntentSystem intentSystem;


    public override string GetName()
    {
        if (cardTemplate == MenuControl.Instance.heroMenu.hero || this == MenuControl.Instance.heroMenu.hero)
        {
            return MenuControl.Instance.heroMenu.HeroName;
        }

        return base.GetName();
    }
    

    public void AddTempEffect(Effect effectTemplate,int charges)
    {
        tempStartingEffects.Add(effectTemplate);
        tempStartingEffectCharges.Add(charges);        
    }

    public override bool isRangedAttack()
    {
        if (weapon != null)
        {
            if (weapon.activatedAbility.GetTargetValidator() is TargetLinear)
            {
                if (((TargetLinear)(weapon.activatedAbility).GetTargetValidator()).range > 1)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public override int GetInitialPower(bool ignoreBattle = false, Player playerOverride = null)
    {
        int modifier = 0;
        if ((!MenuControl.Instance.battleMenu.inBattle || ignoreBattle) &&
            MenuControl.Instance.heroMenu.ascensionMode >= 7 &&
            MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter() != null &&
            MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter().isBoss &&
            this != MenuControl.Instance.heroMenu.hero)
        {
            modifier += 1;
        }

        return base.GetInitialPower(ignoreBattle) + (weapon != null ? weapon.GetPower() : 0) + modifier;
    }

    public override void SufferDamage(Card sourceCard, Ability ability, int damageAmount, bool destroy = false, bool triggerUnitDamage = true, bool firstDamage = true)
    {
        base.SufferDamage(sourceCard, ability, damageAmount, destroy, triggerUnitDamage,firstDamage);

        if (GetHP() <= 0)
        {
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.HeroDestroyed(sourceCard, ability, damageAmount, this);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }

            bool oneOtherHeroOnBoard = false;
            foreach (Card card in player.cardsOnBoard)
            {
                if (card is Hero && card != this)
                {
                    oneOtherHeroOnBoard = true;
                }
            }
    //不太确定这个逻辑，不过目前它只是用来复活我方英雄的？
           // if (oneOtherHeroOnBoard || player == MenuControl.Instance.battleMenu.playerAI)
           if(oneOtherHeroOnBoard || UniqueID == "Hero SleepingDemon")
            {
                player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.removedFromGame);
            }
        }
    }

    public override int GetBaseHP(bool ignoreBattle = false, Player playerOverride = null)
    {
        if (MenuControl.Instance.heroMenu.ascensionMode >= 1 &&
            (!MenuControl.Instance.battleMenu.inBattle || ignoreBattle))
        {
            if (this is Hero && this != MenuControl.Instance.heroMenu.hero &&
                this != MenuControl.Instance.battleMenu.player1.GetHero())
            {
                if (MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter() != null &&
                    !MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter().isBoss && this !=
                    MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter().playerHero)
                {
                    return initialHP + 4;
                }
            }
        }


        return base.GetBaseHP(ignoreBattle, playerOverride);
    }

    public override void InitializeUnit(bool keepEffects)
    {
        //Initialize card
        if (cardTemplate != null)
        {
            initialPower = ((Unit)cardTemplate).GetInitialPower(true) - (weapon != null ? weapon.GetPower() : 0);
            initialHP = ((Unit)cardTemplate).GetBaseHP(true);
            
            if (this.UniqueID != MenuControl.Instance.heroMenu.hero.UniqueID && MenuControl.Instance.eventMenu.isSpecialChallenge)
            {
                initialHP =(int)( initialHP * 1.5f);
            }
            initialMoves = ((Unit)cardTemplate).GetInitialMoves();
            initialActions = ((Unit)cardTemplate).GetInitialActions();
        }

        originalActivatedAbility = activatedAbility;
        originalActionAnimation = actionAnimation;
        originalMovementType = movementType;
        currentHP = initialHP;
        currentPower = initialPower;

        remainingMoves = 0;
        remainingActions = 0;
    }

    public IntentSystem GetIntentSystem()
    {
        if (MenuControl.Instance.battleMenu.inBattle)
        {
            if (intentSystem == null) //try to find an intent system on one of the heros
            {
                foreach (Card card in player.allCards)
                {
                    if (card is Hero && ((Hero)card).intentSystem != null)
                    {
                        intentSystem = ((Hero)card).intentSystem;
                    }
                }
            }

            return intentSystem;
        }

        return GetComponent<IntentSystem>();
    }

    
}