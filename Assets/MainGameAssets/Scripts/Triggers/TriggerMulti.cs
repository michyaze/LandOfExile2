using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    GameStarted, TurnStarted, TurnEnded, MinionSummoned, MinionDestroyed, CardPlayed, CardDrawn, CardDiscarded, UnitAttacks, UnitHealed, UnitDamaged, PlayerShuffledDeck, MinionSacrificed, CardRazed, CardExhausted, HeroDestroyed, UnitMoved, UnitIsAttacked, HeroSummoned, GameEnded, EndOfEnemyTurn, BeforeInitialDraw, UnitAttacksInitialOnly, BeforeEndTurnImmediateOnly, MinionEvolved, PlayerCardDiscarded,
    WeaponChangedTo,WeaponChanged,UnitMoveInto,UnitMoveOutOf,EndOfAITurn,EndOfPlayerTurn,WeaponChangedFrom,UnitReturnToHand,DamageUnit,
}

public class TriggerMulti : Trigger
{
    public TriggerType triggerType;
    public bool eventMustTriggerOnThisCard;
    public int abilitiesToCallBelow = 1;
    public TriggerFilter triggerFilter;
    public bool immediatelyPerform;
    public override float PerformAnimationTime(Card sourceCard)
    {
        var time= base.PerformAnimationTime(sourceCard);
        bool startCounting = false;
        int countDown = abilitiesToCallBelow;
        foreach (Ability ability in GetComponents<Ability>())
        {
            if (ability == this)
            {
                startCounting = true;
            }
            else
            {
                if (startCounting)
                {
                    countDown -= 1;

                    time = Mathf.Max(time, ability.PerformAnimationTime(sourceCard));

                    if (countDown == 0) break;
                }
            }

        }

        return time;
    }

    public void CallAbilitiesBelow(Card sourceCard, Unit targetUnit, int amount = 0)
    {
        if (triggerFilter != null && !triggerFilter.Check(sourceCard, targetUnit.GetTile(), amount)) return;
        System.Action actionToPerform = () =>
        {

            bool startCounting = false;
            int countDown = abilitiesToCallBelow;
            if (this == null)
            {
                //Debug.LogError("this might be a problem but guess we can worry about it later");
                return;
            }
            foreach (Ability ability in GetComponents<Ability>())
            {
                if (ability == this)
                {
                    startCounting = true;
                }
                else
                {
                    if (startCounting)
                    {
                        countDown -= 1;
                        Tile targetTile = null;
                        if (targetUnit != null)
                        {
                            targetTile = targetUnit.GetTile();
                            if (targetTile == null)
                            {
                                //考虑到目标直接被杀死的情况
                                targetTile = targetUnit.lastTile;
                            }
                        }
                        ability.PerformAbility(sourceCard, targetTile, amount);

                        if (countDown == 0) break;
                    }
                }

            }

        };


        if (immediatelyPerform)
        {
            actionToPerform();
        }
        else
        {
            float animationTime = 0;
            bool startCounting = false;
            int countDown = abilitiesToCallBelow;
            foreach (Ability ability in GetComponents<Ability>())
            {
                if (ability == this)
                {
                    startCounting = true;
                }
                else
                {
                    if (startCounting)
                    {
                        countDown -= 1;

                        animationTime += ability.PerformAnimationTime(sourceCard);
                        if (countDown == 0) break;
                    }
                }
            }

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), actionToPerform, false,
                animationTime);
        }
    }
    public override void BeforeInitialDraw()
    {
        if (triggerType != TriggerType.BeforeInitialDraw) return;

        CallAbilitiesBelow(GetCard(), null);
    }
    public override void GameStarted()
    {
        if (triggerType != TriggerType.GameStarted) return;

        CallAbilitiesBelow(GetCard(), null);
      
    }

    public override void GameEnded(bool victory)
    {
        if (triggerType != TriggerType.GameEnded) return;

        CallAbilitiesBelow(GetCard(), null);
    }
    public override void WeaponChangedTo(NewWeapon weapon)
    {
        if (triggerType != TriggerType.WeaponChangedTo) return;
        
        Card card = GetComponentInParent<Card>();
        if (card == weapon)
            //this is wrong!!!
            //if(GetCard().UniqueID == weapon.UniqueID || card.UniqueID == weapon.UniqueID)
        {
            CallAbilitiesBelow(GetCard(), GetCard() as Unit);
        }
    }
    public override void WeaponChangedFrom(NewWeapon weapon)
    {
        if (triggerType != TriggerType.WeaponChangedFrom) return;
        
        Card card = GetComponentInParent<Card>();
        if (card == weapon)
            //this is wrong!!!
            //if(GetCard().UniqueID == weapon.UniqueID || card.UniqueID == weapon.UniqueID)
        {
            CallAbilitiesBelow(GetCard(), GetCard() as Unit);
        }
    }

    public override void WeaponChanged(NewWeapon weaponTemplate, NewWeapon oldWeapon, Unit hero)
    {
        if (triggerType != TriggerType.WeaponChanged) return;
        if (eventMustTriggerOnThisCard){
            if (GetCard().player == hero.player)
            { 
                CallAbilitiesBelow(GetCard(), null);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), null);
            }
        }
    }

    public override void TurnStarted(Player player)
    {
        if (triggerType != TriggerType.TurnStarted) return;
        if (eventMustTriggerOnThisCard){
            if (GetCard().player == player)
            { 
                CallAbilitiesBelow(GetCard(), null);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), null);
            }
        }
    }

    public override void TurnEnded(Player player)
    {
        if (triggerType != TriggerType.TurnEnded && triggerType != TriggerType.EndOfEnemyTurn && triggerType!=TriggerType.EndOfAITurn&& triggerType!=TriggerType.EndOfPlayerTurn) return;
        if (triggerType == TriggerType.EndOfAITurn)
        {
            if (MenuControl.Instance.battleMenu.playerAI == player)
            {
                if (abilitiesToCallBelow > 0)
                {
                    CallAbilitiesBelow(GetCard(), null);
                }
            } 
        }
        if (triggerType == TriggerType.EndOfPlayerTurn)
        {
            if (MenuControl.Instance.battleMenu.player1 == player)
            {
                if (abilitiesToCallBelow > 0)
                {
                    CallAbilitiesBelow(GetCard(), null);
                }
            } 
        }
        if (triggerType == TriggerType.TurnEnded)
        {
            if (eventMustTriggerOnThisCard)
            {
                if (GetCard().player == player)
                {
                    CallAbilitiesBelow(GetCard(), null);
                }
            }
            else
            {
                if (abilitiesToCallBelow > 0)
                {
                    CallAbilitiesBelow(GetCard(), null);
                }
            }
        }
        else if (triggerType == TriggerType.EndOfEnemyTurn)
        {
            if (eventMustTriggerOnThisCard)
            {
                if (GetCard().player != player)
                {
                    CallAbilitiesBelow(GetCard(), null);
                }
            }
            else
            {
                if (abilitiesToCallBelow > 0)
                {
                    CallAbilitiesBelow(GetCard(), null);
                }
            }
        }
    }

    public override void BeforeEndTurn(Player player)
    {
        if (triggerType != TriggerType.BeforeEndTurnImmediateOnly) return;

        if (eventMustTriggerOnThisCard)
        {
            if (GetCard().player != player)
            {
                CallAbilitiesBelow(GetCard(), null);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), null);
            }
        }
    }
    

    public override void MinionSummoned(Minion minion)
    {
        if (triggerType != TriggerType.MinionSummoned) return;
        if (eventMustTriggerOnThisCard)
        {
            if (minion == GetCard())
            {
                CallAbilitiesBelow(GetCard(), minion);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), minion);
            }
        }
    }

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (triggerType != TriggerType.MinionDestroyed) return;
        if (eventMustTriggerOnThisCard)
        {
            if (minion == GetCard())
            {
                CallAbilitiesBelow(GetCard(), minion);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), minion);
            }
        }
    }
    public override void MinionSacrificed(Card sourceCard, Ability ability, Minion minion)
    {
        if (triggerType != TriggerType.MinionSacrificed) return;

        if (eventMustTriggerOnThisCard)
        {
            if (minion == GetCard())
            {
                CallAbilitiesBelow(GetCard(), minion);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), minion);
            }
        }
    }

    public override void MinionEvolved(Minion minion)
    {
        if (triggerType != TriggerType.MinionEvolved) return;
        

        if (eventMustTriggerOnThisCard)
        {
            if (minion == GetCard())
            {
                CallAbilitiesBelow(GetCard(), minion);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), minion);
            }
        }
    }

    public override void CardDrawn(Card card)
    {
        if (triggerType != TriggerType.CardDrawn) return;
        if (eventMustTriggerOnThisCard)
        {
            if (card == GetCard())
            {
                CallAbilitiesBelow(card, null);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(card, null);
            }
        }
    }
    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (triggerTime == 0)
        {
            return;
        }
        
        if (triggerType != TriggerType.CardPlayed) return;
        if (eventMustTriggerOnThisCard)
        {
            if (card == GetCard())
            {
                CallAbilitiesBelow(card, null);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                triggerTime--;
                CallAbilitiesBelow(card, null);
            }
        }
    }

    public override void CardDiscarded(Card card, bool automaticDiscard)
    {
        if (triggerType != TriggerType.CardDiscarded) return;
        if (eventMustTriggerOnThisCard)
        {
            if (card == GetCard())
            {
                CallAbilitiesBelow(card, null);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(card, null);
            }
        }
    }

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {
        if (triggerType == TriggerType.UnitAttacks || (triggerType == TriggerType.UnitAttacksInitialOnly && initialAttack)){

            if (eventMustTriggerOnThisCard)
            {
                if (attacker == GetCard())
                {
                    CallAbilitiesBelow(attacker, defender);
                }

            }
            else
            {
                if (abilitiesToCallBelow > 0)
                {
                    CallAbilitiesBelow(attacker, defender);
                }
            }
        }
    }

    public override void UnitHealed(Unit unit, Ability ability, int healAmount)
    {
        if (triggerType != TriggerType.UnitHealed) return;
        if (eventMustTriggerOnThisCard)
        {
            if (unit == GetCard())
            {
                CallAbilitiesBelow(GetCard(), unit);
            }

        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), unit);
            }
        }
    }

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (triggerType != TriggerType.UnitDamaged) return;
        if (eventMustTriggerOnThisCard)
        {
            if (unit == GetCard())
            {
                CallAbilitiesBelow(GetCard(), unit, damageAmount);
            }

        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), unit, damageAmount);
            }
        }
    }

    public override void PlayerShuffledDeck(Player player)
    {
        if (triggerType != TriggerType.PlayerShuffledDeck) return;
        if (eventMustTriggerOnThisCard)
        {
            if (GetCard().player == player && player.cardsInDeck.Count > 0)
            {
                CallAbilitiesBelow(GetCard(), null);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0 && player.cardsInDeck.Count > 0)
            {
                CallAbilitiesBelow(GetCard(), null);
            }
        }
    }

    public override void CardRazed(Card card)
    {
        if (triggerType != TriggerType.CardRazed) return;
        if (eventMustTriggerOnThisCard)
        {
            if (card == GetCard())
            {
                CallAbilitiesBelow(card, null);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(card, null);
            }
        }
    }

    public override void CardExhausted(Card card, Zone previousZone)
    {
        if (triggerType != TriggerType.CardExhausted) return;
        if (eventMustTriggerOnThisCard)
        {
            if (card == GetCard())
            {
                CallAbilitiesBelow(card, null);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(card, null);
            }
        }
    }

    public override void HeroSummoned(Hero newHero)
    {
        if (triggerType != TriggerType.HeroSummoned) return;
        if (eventMustTriggerOnThisCard)
        {
            if (newHero == GetCard())
            {
                CallAbilitiesBelow(GetCard(), newHero);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), newHero);
            }
        }
    }

   


}
