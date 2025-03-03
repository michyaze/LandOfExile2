using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerMultiUnitOnBoard : Trigger
{
    public TriggerType triggerType;
    public bool eventMustTriggerOnThisCard = true;
    public bool eventMustTriggerOnThisPlayer = false;
    public int abilitiesToCallBelow = 1;
    public TriggerFilter triggerFilter;
    public int triggerEveryXTimes = 1;
    public int currentTriggers;
    public bool immediatelyPerform;

    public void CallAbilitiesBelow(Card sourceCard, Unit targetUnit, int amount = 0, Tile targetTile = null)
    {
        currentTriggers += 1;
        if (currentTriggers < triggerEveryXTimes) return;
        currentTriggers = 0;

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

                        ability.PerformAbility(sourceCard, targetTile == null ? targetUnit.GetTile() : targetTile,
                            amount);

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
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), actionToPerform);
        }
    }

    public override void WeaponChanged(NewWeapon weaponTemplate, NewWeapon oldWeapon, Unit hero)
    {
        if (triggerType != TriggerType.WeaponChanged) return;
        if (eventMustTriggerOnThisCard || eventMustTriggerOnThisPlayer)
        {
            if (GetCard().player == hero.player)
            {
                CallAbilitiesBelow(GetCard(), hero);
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

    public override void BeforeInitialDraw()
    {
        if (triggerType != TriggerType.BeforeInitialDraw) return;

        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        CallAbilitiesBelow(GetCard(), (Unit)GetCard());
    }

    public override void UnitReturnToHand(Unit unit,Player player)
    {
        if (triggerType != TriggerType.UnitReturnToHand) return;
        if (eventMustTriggerOnThisCard)
        {
            if (GetCard() == unit)
            {
                CallAbilitiesBelow(GetCard(), (Unit)GetCard());
            }
        }
        else
        {
            CallAbilitiesBelow(GetCard(), (Unit)GetCard());
            
        }
    }
    public override void GameStarted()
    {
        if (triggerType != TriggerType.GameStarted) return;

        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;


        CallAbilitiesBelow(GetCard(), (Unit)GetCard());
    }

    public override void GameEnded(bool victory)
    {
        if (triggerType != TriggerType.GameEnded) return;

        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        CallAbilitiesBelow(GetCard(), (Unit)GetCard());
    }

    public override void TurnStarted(Player player)
    {
        if (triggerType != TriggerType.TurnStarted) return;
        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (eventMustTriggerOnThisCard)
        {
            if (GetCard().player == player)
            {
                CallAbilitiesBelow(GetCard(), GetCard() as Unit);
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), GetCard() as Unit);
            }
        }
    }

    public override void TurnEnded(Player player)
    {
        if (triggerType != TriggerType.TurnEnded && triggerType != TriggerType.EndOfEnemyTurn) return;

        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (triggerType == TriggerType.TurnEnded)
        {
            if (eventMustTriggerOnThisCard)
            {
                if (GetCard().player == player)
                {
                    CallAbilitiesBelow(GetCard(), (Unit)GetCard());
                }
            }
            else
            {
                if (abilitiesToCallBelow > 0)
                {
                    CallAbilitiesBelow(GetCard(), (Unit)GetCard());
                }
            }
        }
        else if (triggerType == TriggerType.EndOfEnemyTurn)
        {
            if (eventMustTriggerOnThisCard)
            {
                if (GetCard().player != player)
                {
                    CallAbilitiesBelow(GetCard(), (Unit)GetCard());
                }
            }
            else
            {
                if (abilitiesToCallBelow > 0)
                {
                    CallAbilitiesBelow(GetCard(), (Unit)GetCard());
                }
            }
        }
    }

    public override void BeforeEndTurn(Player player)
    {
        if (triggerType != TriggerType.BeforeEndTurnImmediateOnly) return;

        if (eventMustTriggerOnThisCard)
        {
            if (GetCard().player == player)
            {
                CallAbilitiesBelow(GetCard(), (Unit)GetCard());
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), (Unit)GetCard());
            }
        }
    }

    public override void MinionSummoned(Minion minion)
    {
        
        if (minion == GetCard()) currentTriggers = 0;

        if (triggerType != TriggerType.MinionSummoned) return;

        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

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
        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (eventMustTriggerOnThisCard)
        {
            if (minion == GetCard())
            {
                CallAbilitiesBelow(GetCard(), minion, 0, minion.GetTile());
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
        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (eventMustTriggerOnThisCard)
        {
            if (minion == GetCard())
            {
                CallAbilitiesBelow(GetCard(), minion, 0, minion.GetTile());
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), minion, 0, minion.GetTile());
            }
        }
    }

    public override void MinionEvolved(Minion minion)
    {
        if (triggerType != TriggerType.MinionEvolved) return;
        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (eventMustTriggerOnThisCard)
        {
            if (minion == GetCard())
            {
                CallAbilitiesBelow(GetCard(), minion, 0, minion.GetTile());
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(GetCard(), minion, 0, minion.GetTile());
            }
        }
    }

    public override void CardDrawn(Card card)
    {
        if (triggerType != TriggerType.CardDrawn) return;
        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (eventMustTriggerOnThisCard)
        {
            if (card == GetCard())
            {
                CallAbilitiesBelow(card, (Unit)GetCard());
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(card, (Unit)GetCard());
            }
        }
    }

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (triggerType != TriggerType.CardPlayed) return;
        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (eventMustTriggerOnThisCard)
        {
            if (card == GetCard())
            {
                CallAbilitiesBelow(card, (Unit)GetCard());
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(card, (Unit)GetCard());
            }
        }
    }

    public override void CardDiscarded(Card card, bool automaticDiscard)
    {
        if (triggerType != TriggerType.CardDiscarded && triggerType != TriggerType.PlayerCardDiscarded) return;
        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (triggerType == TriggerType.CardDiscarded)
        {
            if (eventMustTriggerOnThisCard)
            {
                if (card == GetCard())
                {
                    CallAbilitiesBelow(card, (Unit)GetCard());
                }
            }
            else
            {
                if (abilitiesToCallBelow > 0)
                {
                    CallAbilitiesBelow(card, (Unit)GetCard());
                }
            }
        }
        else if (triggerType == TriggerType.PlayerCardDiscarded &&
                 GetCard().player == MenuControl.Instance.battleMenu.player1)
        {
            if (eventMustTriggerOnThisCard)
            {
                if (card == GetCard())
                {
                    CallAbilitiesBelow(card, (Unit)GetCard());
                }
            }
            else
            {
                if (abilitiesToCallBelow > 0)
                {
                    CallAbilitiesBelow(card, (Unit)GetCard());
                }
            }
        }
    }

    
    
    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {
        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board) && !(GetCard() is Artifact)) return;

        if ((triggerType == TriggerType.UnitAttacks ||
            triggerType == TriggerType.UnitAttacksInitialOnly) && initialAttack)
        {
            if (eventMustTriggerOnThisCard)
            {
                if (attacker == GetCard())
                {
                    CallAbilitiesBelow(attacker, defender, 0, defender.GetTile());
                }
            }
            else
            {
                if (abilitiesToCallBelow > 0)
                {
                    CallAbilitiesBelow(attacker, defender, 0, defender.GetTile());
                }
            }
        }

        if (triggerType == TriggerType.UnitIsAttacked && !(this.GetEffect() != null &&
                                                           this.GetEffect().UniqueID == "CommonEffect29" &&
                                                           defender.GetEffectsOfType<Block>().Count >
                                                           0)) // Don't consume evasion if there is a Block
        {
            if (eventMustTriggerOnThisCard)
            {
                if (defender == GetCard())
                {
                    if (targetOnSource)
                    {
                        
                        CallAbilitiesBelow(defender, defender);
                    }
                    else
                    {
                        CallAbilitiesBelow(defender, attacker);
                    }
                }
            }
            else
            {
                if (abilitiesToCallBelow > 0)
                {
                    if (targetOnSource)
                    {
                        CallAbilitiesBelow(defender, defender);
                    }
                    else
                    {
                        CallAbilitiesBelow(defender, attacker);
                    }
                }
            }
        }
    }

    public override void UnitHealed(Unit unit, Ability ability, int healAmount)
    {
        if (triggerType != TriggerType.UnitHealed) return;
        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

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

    void moveIntoTriggerAction(Unit unit, Tile originalTile, Tile destinationTile)
    {
        if (triggerType == TriggerType.UnitMoveInto)
        {
            
            if (destinationTile == null)
            {
                return;//陷阱移除触发的
            }
            var trap = GetComponent<WeatherTrap>();
            //if ((GetComponent<WeatherTrap>() == null)) return;
            if (trap != null)
            {
                if (destinationTile == trap.GetTile())
                {
                    CallAbilitiesBelow(unit, unit, 0, destinationTile);
                }
            }else if (GetCard() == null && GetComponentInParent<Tile>())
            {
                //需要考虑大型怪触发
                if (destinationTile == GetComponentInParent<Tile>())
                {
                    CallAbilitiesBelow(unit, unit, 0, destinationTile);
                }
            }
            else
            {
                if (eventMustTriggerOnThisCard)
                {
                    if (unit == GetCard())
                    {
                        CallAbilitiesBelow(unit, unit, 0, destinationTile);
                    }
                }
            }
            return;
        }
    }


    public override void UnitMoved(Unit unit, Tile originalTile, Tile destinationTile)
    {
        moveIntoTriggerAction(unit, originalTile, destinationTile);
        if (triggerType == TriggerType.UnitMoveOutOf)
        {
            if (originalTile == null || originalTile == destinationTile)
            {
                return;//这是召唤怪物时触发和放置陷阱时触发
            }
            var trap = GetComponent<WeatherTrap>();
            // for trap, perform on unit
            if (trap != null)
            {
                if (originalTile == trap.GetTile())
                {
                    CallAbilitiesBelow(unit, unit, 0, destinationTile);
                }
            }
            else
            {
                //for tile, do changes on originalTile
                if (eventMustTriggerOnThisCard)
                {
                    if (unit == GetCard())
                    {
                        CallAbilitiesBelow(unit, unit, 0, originalTile);
                    }
                }
            }
            return;
        }

        if (triggerType != TriggerType.UnitMoved) return;
        if (originalTile == null || originalTile == destinationTile || destinationTile == null)
        {
            return;//这是召唤怪物时触发和放置陷阱时触发
        }
        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit)
                                          || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;
        if (eventMustTriggerOnThisCard)
        {
            if (unit == GetCard())
            {
                CallAbilitiesBelow(unit, unit, 0, destinationTile);
            }
        }

        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(unit, unit, 0);
            }
        }
    }

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (!enabled)
        {
            return;
        }
        if (!(GetCard() is NewWeapon) && !(GetCard() is Artifact) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;
        if (damageAmount <= 0)
        {
            return;
        }

        if (!triggerUnitDamage)
        {
            return;
        }

        if (triggerType == TriggerType.DamageUnit)
        {
            if (sourceCard is Unit sourceUnit)
            {
                
            if (eventMustTriggerOnThisCard)
            {
                if (sourceCard == GetCard())
                {
                    CallAbilitiesBelow(GetCard(), sourceUnit , damageAmount);
                }
            }else if (eventMustTriggerOnThisPlayer)
            {
                if (sourceCard.player == GetCard().player)
                {
                    
                    CallAbilitiesBelow(GetCard(), sourceUnit , damageAmount);
                }
            }
            else
            {
                if (abilitiesToCallBelow > 0)
                {
                    CallAbilitiesBelow(GetCard(), sourceUnit, damageAmount);
                }
            }
            }
        }

        if (triggerType == TriggerType.UnitDamaged)
        {
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

        if (triggerType == TriggerType.HeroDestroyed)
        {
            if (unit is Hero && unit.GetHP() <= 0)
            {
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
        }
    }

    public override void PlayerShuffledDeck(Player player)
    {
        if (triggerType != TriggerType.PlayerShuffledDeck) return;
        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (eventMustTriggerOnThisCard)
        {
            if (GetCard().player == player && player.cardsInDeck.Count > 0)
            {
                CallAbilitiesBelow(GetCard(), (Unit)GetCard());
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0 && player.cardsInDeck.Count > 0)
            {
                CallAbilitiesBelow(GetCard(), (Unit)GetCard());
            }
        }
    }

    public override void CardRazed(Card card)
    {
        if (triggerType != TriggerType.CardRazed) return;
        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;
        if (eventMustTriggerOnThisCard)
        {
            if (card == GetCard())
            {
                CallAbilitiesBelow(card, (Unit)GetCard());
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(card, (Unit)GetCard());
            }
        }
    }

    public override void CardExhausted(Card card, Zone previousZone)
    {
        if (triggerType != TriggerType.CardExhausted) return;
        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;
        if (eventMustTriggerOnThisCard)
        {
            if (card == GetCard())
            {
                CallAbilitiesBelow(card, (Unit)GetCard());
            }
        }
        else
        {
            if (abilitiesToCallBelow > 0)
            {
                CallAbilitiesBelow(card, (Unit)GetCard());
            }
        }
    }

    public override void HeroSummoned(Hero newHero)
    {
        if (triggerType != TriggerType.HeroSummoned) return;
        if (!(GetCard() is NewWeapon) &&
            (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

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