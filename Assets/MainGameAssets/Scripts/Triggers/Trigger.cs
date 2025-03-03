using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trigger : Ability
{
    public int triggerTime = -1;
    public bool targetOnSource;

    [Tooltip("这个能力不是所有trigger都实现了，目前未实现任何trigger，不要使用它")]
    public Ability performAbilityAfterTrigger;

    public virtual void PerformAbilityAfterTrigger(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if(performAbilityAfterTrigger != null){
            performAbilityAfterTrigger.PerformAbility(sourceCard, targetTile, amount);
        }
    }
    public virtual void GameStarted()
    {

      
    }

    public virtual void GameEnded(bool victory)
    {


    }

    public virtual void WeaponChangedTo(NewWeapon weapon)
    {
    }
    public virtual void WeaponChangedFrom(NewWeapon weapon)
    {
    }
    public virtual void TurnStarted(Player player)
    {

    }

    public virtual void TurnEnded(Player player)
    {

    }

    public virtual void AfterTurnEnded(Player player)
    {

    }

    public virtual void BeforeEndTurn(Player player)
    {

    }

    public virtual void MinionSummoned(Minion minion)
    {

    }

    public virtual void HeroDestroyed(Card sourceCard, Ability ability, int damageAmount, Hero hero)
    {

    }

    public virtual void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
       
    }

    public virtual void MinionSacrificed(Card sourceCard, Ability ability, Minion minion)
    {

    }

    public virtual void MinionEvolved(Minion minion)
    {

    }

    public virtual void CardDrawn(Card card)
    {

    }
    public virtual void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {

    }
    public virtual void CardFinishedPlayed(Card card, Tile tile, List<Tile> tiles)
    {

    }

    public virtual void CardDiscarded(Card card, bool automaticDiscard)
    {

    }

    public virtual void CardAddedIntoDeck(Card card)
    {
    }


    public virtual void UnitReturnToHand(Unit card,Player player)
    {
        
    }

    public virtual void CardRemoved(Card card)
    {

    }
    public virtual void UnitMoved(Unit unit, Tile originalTile, Tile destinationTile)
    {

    }

    public virtual void HeroSummoned(Hero newHero)
    {

    }

    public virtual void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {

    }

    public virtual void AfterUnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack, bool consumeWeapon)
    {

    }


    public virtual void UnitHealed(Unit unit, Ability ability, int healAmount)
    {
       
    }

    public virtual void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {

    }

    public virtual void TrapGenerated(Tile tile, WeatherTrap trap)
    {
        
    }

    public virtual void UnitAppliedEffect(Unit unit, Ability ability, Effect effect, int charges)
    {

    }
    
    public virtual void UnitAppliedEffectBeforeCheck(Unit unit, Ability ability, Effect effect, int charges)
    {

    }
    
    public virtual void TileAppliedEffect(Tile tile, Ability ability, Effect effect, int charges)
    {

    }
    
    public virtual void TileApplyEffectBeforeCheck(Tile tile, Ability ability, Effect effect, int charges)
    {

    }

    public virtual void UnitRemovedEffect(Unit unit, Ability ability, Effect effect)
    {

    }
    public virtual void UnitFinishRemovedEffect(Unit unit, Ability ability, Effect effect)
    {

    }

    public virtual void EffectChargesChanged(Ability ability, Effect effect, int previousCharges)
    {

    }

    public virtual void PlayerShuffledDeck(Player player)
    {

    }

    public virtual void CardRazed(Card card)
    {

    }

    public virtual void CardExhausted(Card card, Zone previousZone)
    {

    }

    public virtual void ManaChanged(Player player, int amountChanged, int previousAmount)
    {

    }
    

    public virtual void CardChangedInitialManaCost(Card card, Ability ability, int changeValue)
    {
    }

    public virtual void UnitChangedPower(Unit unit, Ability ability, int oldValue)
    {

    }

    public virtual void UnitChangedMaxHP(Unit unit, Ability ability, int oldValue)
    {

    }

    public virtual void UnitChangedCurrentHP(Unit unit, Ability ability, int oldValue)
    {

    }

    public virtual void UnitChangedMoves(Unit unit, Ability ability, int oldValue)
    {

    }

    public virtual void UnitChangedActions(Unit unit, Ability ability, int oldValue)
    {

    }

    public virtual void BeforeInitialDraw()
    {

    }

    public virtual void WeaponChanged(NewWeapon weaponTemplate, NewWeapon oldWeapon, Unit hero)
    {

    }

   
}
