using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Card
{
    public NewWeapon weapon;
    public int initialPower;
    public int initialHP;
    public int initialMoves;
    public int initialActions;

    public int currentPower;
    public int currentHP;
    public int remainingMoves;
    public int currentDamageReduction;

    public int remainingActions;

    public TargetValidator movementType;

    protected TargetValidator originalMovementType;

    private int moveCountThisTurn = 0;

    public bool hasMovedThisTurn => moveCountThisTurn > 0;

    [HideInInspector] public Ability originalActivatedAbility;
    [HideInInspector] public ActionAnimation originalActionAnimation;


    public bool checkCanAttackTarget(Unit targetUnit,TargetValidator targetValidator)
    {
        var unitPreventions = GetComponentsInChildren<AttackRangeTargetPrevention>();
        if (unitPreventions == null || unitPreventions.Length == 0)
        {
            //todo 这个检查有点hack了，因为现在只有飞行一种targetPrevention，且飞行可以打飞行。所以如果unit有飞行就不检查被攻击者的飞行状态了
            var targetPreventions = targetUnit.GetComponentsInChildren<AttackRangeTargetPrevention>();
            if (targetPreventions.Length > 0 )
            {
                foreach (var targetPrevention in targetPreventions)
                {
                    if (targetPrevention.preventTargetValidation.Contains(targetValidator))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
    
    public virtual NewWeapon ChangeWeapon(NewWeapon weaponTemplate)
    {
        if (GetComponentInChildren<NewWeapon>())
        {
            //hacky, delay weapon destroy time
            Destroy(GetComponentInChildren<NewWeapon>().gameObject,MenuControl.Instance.battleMenu.GetPlaySpeed() * 5);
            if (weapon.gameObject != GetComponentInChildren<NewWeapon>().gameObject)
            {
                Debug.LogError("weapon is not equal to GetComponentInChildren<NewWeapon>()");
                Destroy(weapon.gameObject,MenuControl.Instance.battleMenu.GetPlaySpeed() * 5);
            }
        }
        
        var oldActionModifier = initialActions;
        //old info
        if (weapon != null)
        {
            oldActionModifier = weapon.GetComponentInChildren<ActionsModifier>()?weapon.GetComponentInChildren<ActionsModifier>().ModifyAmount(this,initialActions):initialActions;
            
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.WeaponChangedFrom(weapon);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }

            }
        }

        var newActionModifier = initialActions;

        //if change to nothing, don't trigger all those stuff
        if (weaponTemplate == null)
        {
            if (newActionModifier != oldActionModifier)
            {
                var diff = newActionModifier - oldActionModifier;
                remainingActions = Mathf.Clamp(remainingActions+diff,0, GetInitialActions());
            }
            activatedAbility = originalActivatedAbility;
            actionAnimation = originalActionAnimation;
            weapon = null;
            return null;
        }

        

        NewWeapon newWeapon = Instantiate(weaponTemplate, transform);
        newWeapon.player = player;
        newWeapon.cardTemplate = weaponTemplate;
        newWeapon.InitWeapon(this);
        var originalWeapon = weapon;
        
        
        weapon = newWeapon;
        
        
        //new info
        if(newWeapon!=null)
        {
            newActionModifier = newWeapon.GetComponentInChildren<ActionsModifier>()?newWeapon.GetComponentInChildren<ActionsModifier>().ModifyAmount(this,initialActions):initialActions;
        }
        
        
        activatedAbility = weapon.activatedAbility;
        actionAnimation = weapon.actionAnimation;

        if (newActionModifier != oldActionModifier)
        {
            var diff = newActionModifier - oldActionModifier;
            remainingActions = Mathf.Clamp(remainingActions+diff,0, GetInitialActions());
        }

        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.WeaponChanged(weaponTemplate, originalWeapon, this);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }

        return newWeapon;
    }
    
    public void resetMoveCountThisTurn()
    {
        moveCountThisTurn = 0;
    }

    public void AddMoveCountThisTurn()
    {
        moveCountThisTurn++;
    }

    public bool IsTaunt()
    {
        return GetEffectsOfType<TauntEffect>().Count != 0;
    }

    public void ChangeMovementTemporarily(TargetValidator newMovementType)
    {
        movementType = newMovementType;
    }

    public virtual List<Tile> GetTilesRight(int spaces = 1)
    {
        return new List<Tile>() { this.GetTile().GetTileRight(spaces) };
    }

    public virtual List<Tile> GetTilesUp(int spaces = 1)
    {
        return new List<Tile>() { this.GetTile().GetTileUp(spaces) };
    }

    public virtual List<Tile> GetTilesDown(int spaces = 1)
    {
        return new List<Tile>() { this.GetTile().GetTileDown(spaces) };
    }

    public virtual List<Tile> GetTilesLeft(int spaces = 1)
    {
        return new List<Tile>() { this.GetTile().GetTileLeft(spaces) };
    }

    public void ChangeMovementBack()
    {
        movementType = originalMovementType;
    }

    public TargetValidator summoningType;

    public List<Effect> currentEffects = new List<Effect>();

    public Doozy.Engine.Soundy.SoundyData attackingSound;
    public Doozy.Engine.Soundy.SoundyData damagedSound;
    public Doozy.Engine.Soundy.SoundyData deathSound;

    public void ChangePosition(Unit otherUnit)
    {
        if (!otherUnit)
        {
            return;
        }

        var tile = GetTile();
        var newTile = otherUnit.GetTile();
        if (!tile || !newTile)
        {
            return;
        }

        ForceMove(newTile);
        otherUnit.ForceMove(tile, newTile);
    }

    public virtual bool isRangedAttack()
    {
        if (activatedAbility is Attack)
        {
            if (((Attack)activatedAbility).GetTargetValidator() is TargetLinear)
            {
                if (((TargetLinear)((Attack)activatedAbility).GetTargetValidator()).range > 1)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public int GetInitialActions()
    {
        int newValue = initialActions;

        if (MenuControl.Instance.battleMenu.inBattle)
        {
            foreach (ActionsModifier modifier in MenuControl.Instance.battleMenu
                         .GetComponentsInChildren<ActionsModifier>())
            {
                if (modifier.enabled)
                {
                    newValue = modifier.ModifyAmount(this, newValue);
                }
            }
        }

        return Mathf.Max(0, newValue);
    }

    public int GetInitialMoves()
    {
        int newValue = initialMoves;
        if (MenuControl.Instance.battleMenu.inBattle)
        {
            foreach (MovesModifier modifier in MenuControl.Instance.battleMenu.GetComponentsInChildren<MovesModifier>())
            {
                if (modifier.enabled)
                {
                    newValue = Mathf.Max(0, modifier.ModifyAmount(this, newValue));
                }
            }
        }

        return newValue;
    }

    public virtual int GetInitialPower(bool ignoreBattle = false, Player playerOverride = null)
    {
        return initialPower;
    }

    public virtual int GetPower(Unit attacker = null, Unit defender = null)
    {
        //Check effects
        int newPower = currentPower;

        if (!MenuControl.Instance.battleMenu.inBattle)
        {
            newPower = GetInitialPower();
        }
        else //if (this is Hero)
        {
            newPower += weapon != null ? weapon.GetPower() : 0;
        }

        if (MenuControl.Instance.battleMenu.inBattle)
        {
            foreach (PowerModifier powerModifier in MenuControl.Instance.battleMenu
                         .GetComponentsInChildren<PowerModifier>())
            {
                if (powerModifier.enabled)
                {
                    newPower = powerModifier.ModifyPower(this, newPower, attacker, defender);
                }
            }
        }

        return Mathf.Max(0, newPower);
    }

    public virtual int GetBaseHP(bool ignoreBattle = false, Player playerOverride = null)
    {
        return initialHP;
    }

    public int GetHP()
    {
        int newHP = currentHP;
        if (MenuControl.Instance.battleMenu.inBattle)
        {
            foreach (HPModifier modifier in MenuControl.Instance.battleMenu.GetComponentsInChildren<HPModifier>())
            {
                if (modifier.enabled)
                {
                    newHP = modifier.ModifyAmount(this, newHP);
                }
            }
        }

        if (GetZone() == MenuControl.Instance.battleMenu.board || this == MenuControl.Instance.heroMenu.hero)
            return newHP;

        return GetBaseHP();
    }

    public int GetHPLost()
    {
        int newHP = currentHP;
        if (MenuControl.Instance.battleMenu.inBattle)
        {
            foreach (HPModifier modifier in MenuControl.Instance.battleMenu.GetComponentsInChildren<HPModifier>())
            {
                if (modifier.enabled)
                {
                    newHP = modifier.ModifyAmount(this, newHP);
                }
            }
        }

        if (GetZone() == MenuControl.Instance.battleMenu.board || this == MenuControl.Instance.heroMenu.hero)
            return Math.Max(0, initialHP - newHP);

        return 0;
    }

    public Tile lastTile = null;
    public Tile GetTile()
    {
        if (GetZone() == MenuControl.Instance.battleMenu.board)
        {
            var tile =  MenuControl.Instance.battleMenu.boardMenu.GetTileOfUnit(this);
            if (tile != null)
            {
                lastTile = tile;
                return tile;
            }
        }

        return null;
    }

    public override bool CanTarget(Tile tile)
    {
        foreach (AnyCanTarget canTarget in MenuControl.Instance.battleMenu.GetComponentsInChildren<AnyCanTarget>())
        {
            if (!canTarget.CanTarget(this, tile)) return false;
        }

        if (player == MenuControl.Instance.battleMenu.playerAI && GetComponent<AICanTarget>() != null)
        {
            if (!GetComponent<AICanTarget>().CanTarget(tile)) return false;
        }

        //Check activated ability
        if (GetZone() == MenuControl.Instance.battleMenu.board && CanAct())
        {
            if (tile != GetTile() && activatedAbility != null && activatedAbility.CanTargetTile(this, tile))
            {
                
                //check taunt
                
                //如果对方是敌人
                //只要周围有taunt的，并且不是被打的，敌人，就不行
                    if (!tile.GetUnit().IsTaunt() && tile.GetUnit().player!=player)
                    {
                        var adjacentTiles = GetTile().GetAdjacentTilesLinear();
                        foreach (var adjacentTile in adjacentTiles)
                        {
                            
                            if (adjacentTile.GetUnit()&&adjacentTile.GetUnit().player!=player && adjacentTile.GetUnit().IsTaunt())
                            {
                                return false;
                            }
                        }
                        
                    }
                //}

                return true;
            }

            if (tile.GetUnit() && tile.GetUnit() is LargeHero)
            {
                foreach (Tile tile2 in ((LargeHero)tile.GetUnit()).GetTiles())
                {
                    if (tile2 != GetTile() && activatedAbility != null && activatedAbility.CanTargetTile(this, tile2))
                    {
                        return true;
                    }
                }
            }
        }


        //Check if  summoning to a spot is valid
        if (GetZone() == MenuControl.Instance.battleMenu.hand)
        {
            if (summoningType != null && summoningType.CanUnitTargetTile(this, tile))
            {
                foreach (SummoningModifier summoningModifier in MenuControl.Instance.battleMenu
                             .GetComponentsInChildren<SummoningModifier>())
                {
                    if (summoningModifier.enabled)
                    {
                        if (!summoningModifier.CanSummon(this, tile))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        //Check if you are on the board and can move to tile
        if (movementType != null && movementType.CanUnitTargetTile(this, tile) && CanMove())
        {
            return true;
        }

        return false;
    }


    public override void TargetTile(Tile tile, bool payCost)
    {
        if (tile.isMoveable() || tile.GetUnit() == this)
        {
            //If Casting from hand pay cost and summon it
            if (player.cardsInHand.Contains(this))
            {
                if (payCost)
                    player.PayCostFor(this);

                player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.limbo);

                //Trigger play card event 
                foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
                {
                    try
                    {
                        trigger.CardPlayed(this, tile, null);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
            }

            Tile originalTile = null;
            if (GetZone() != MenuControl.Instance.battleMenu.board)
            {
                player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.board);

                InitializeUnit(true);

                MoveToTile(tile);

                //Summoning Sickness
                //ApplyEffect(this, null, MenuControl.Instance.battleMenu.summoningSickEffectTemplate, 0);
                Effect effect = Instantiate(MenuControl.Instance.battleMenu.summoningSickEffectTemplate, transform);
                currentEffects.Add(effect);
                effect.originalTemplate = MenuControl.Instance.battleMenu.summoningSickEffectTemplate;


                //Trigger onSummon event

                if (this is Minion)
                {
                    foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
                    {
                        try
                        {
                            trigger.MinionSummoned((Minion)this);
                            trigger.UnitMoved(this, null, tile);
                        }
                        catch (System.Exception e)
                        {
                            Debug.LogError(e);
                        }
                    }
                }
                else if (this is Hero)
                {
                    foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
                    {
                        try
                        {
                            trigger.HeroSummoned((Hero)this);
                            trigger.UnitMoved(this, null, tile);
                        }
                        catch (System.Exception e)
                        {
                            Debug.LogError(e);
                        }
                    }
                }
            }
            else
            {
                originalTile = GetTile();
                if (remainingMoves > 0)
                    remainingMoves -= 1;
            }

            if (originalTile != null)
                ForceMove(tile);
            else
                MoveToTile(tile);
        }
        else
        {
            ForceAttack(tile, true);

            if (remainingActions > 0)
                remainingActions -= 1;
        }
    }

    public void addRemainingActions(int value)
    {
        remainingActions = remainingActions + value; //Math.Min(remainingActions + value, GetInitialActions());
    }

    public void addRemainingMoves(int value)
    {
        remainingMoves = remainingMoves + value; //Math.Min(remainingMoves + value, GetInitialMoves());
    }

    public void useRemainingMoves(int value)
    {
        remainingMoves = remainingMoves - value;
    }

    public bool MoveRandomly()
    {
        if (remainingMoves > 0)
        {
            var adjacentTiles = GetTile().GetAdjacentTilesLinear();
             var randomTiles = new List<Tile>();
            // foreach (var tile in adjacentTiles)
            // {
            //     if (tile.isMoveable())
            //     {
            //         randomTiles.Add(tile);
            //     }
            // }

            foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
            {
                // if (CanTarget(tile))
                // {
                //     randomTiles.Add(tile);
                // }
                if (movementType != null && movementType.CanUnitTargetTile(this, tile) && CanMove())
                {
                    randomTiles.Add(tile);
                }
            }

            if (randomTiles.Count > 0)
            {
                ForceMove(randomTiles.RandomItem());
                remainingMoves -= 1;
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    public void ForceMove(Tile tile, Tile originalTile = null)
    {
        var oldTiles = GetTiles();
        if (originalTile == null)
        {
            originalTile = GetTile();
        }

        MoveToTile(tile);
        AddMoveCountThisTurn();

        if (MenuControl.Instance.largeHeroMoveFix && this is LargeHero)
        {
            var newTiles = GetTiles();
            //find tiles in newTiles and not in oldTiles
            var pureNewTiles = new List<Tile>();
            foreach (var newtile in newTiles)
            {
                if (!oldTiles.Contains(newtile))
                {
                    pureNewTiles.Add(newtile);
                }
            }

            //find tiles in oldTiles and not in newTiles
            var pureOldTiles = new List<Tile>();
            foreach (var oldtile in oldTiles)
            {
                if (!newTiles.Contains(oldtile))
                {
                    pureOldTiles.Add(oldtile);
                }
            }

            for (int i = 0; i < pureNewTiles.Count; i++)
            {
                foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
                {
                    try
                    {
                        trigger.UnitMoved(this, pureOldTiles[i], pureNewTiles[i]);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
            }
        }
        else
        {
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.UnitMoved(this, originalTile, tile);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }

    public virtual List<Tile> GetTiles()
    {
        return new List<Tile>() { GetTile() };
    }

    public void ForceAttack(Tile tile, bool initialAttack = false, bool consumeWeapon = true)
    {
        if (activatedAbility != null)
        {
            //Trigger attack event 
            if (activatedAbility is Attack)
            {
                foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
                {
                    try
                    {
                        trigger.UnitAttacks(this, tile.GetUnit(), tile, initialAttack);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
            }

            activatedAbility.PerformAbility(this, tile);

            if (activatedAbility is Attack)
            {
                foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
                {
                    try
                    {
                        trigger.AfterUnitAttacks(this, tile.GetUnit(), tile, initialAttack, consumeWeapon);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log("this " + this);
                        Debug.Log("tile " + tile);
                        Debug.Log("tile.GetUnit() " + tile.GetUnit());
                        Debug.LogError(e);
                    }
                }
            }
        }
    }

    public void MoveToTile(Tile tile)
    {
        Tile originalTile = GetTile();
        MenuControl.Instance.battleMenu.boardMenu.MoveUnitToTile(this, tile);
    }

    public virtual void Heal(Card sourceCard, Ability ability, int healAmount)
    {
        // modifiers
        int newAmount = healAmount;
        foreach (HealingModifier modifier in MenuControl.Instance.battleMenu.GetComponentsInChildren<HealingModifier>())
        {
            if (modifier.enabled)
            {
                newAmount = Mathf.Max(0, modifier.ModifyAmount(this, newAmount));
            }
        }

        if (currentHP >= GetBaseHP()) healAmount = 0;
        else
            healAmount = Mathf.Min(newAmount, GetBaseHP() - currentHP);


        currentHP += healAmount;

        //Trigger  event
        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.UnitHealed(this, ability, healAmount);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    public virtual void SufferDamage(Card sourceCard, Ability ability, int damageAmount, bool destroy = false,
        bool triggerUnitDamage = true, bool firstDamage = true)
    {
        var damageTimeModifier = 1;
        foreach (DamageTimeModifier modifier in MenuControl.Instance.battleMenu
                     .GetComponentsInChildren<DamageTimeModifier>())
        {
            if (modifier.enabled)
            {
                damageTimeModifier += modifier.ModifyAmount(sourceCard, ability, this, damageAmount);
            }
        }

        for (int i = 0; i < damageTimeModifier; i++)
        {
            int extraDamageAmountToCalculate = 0;//目前用在false life 上。false life的血不会被扣，但要在trigger中算作被扣
            if (!destroy)
            {
                if (GetEffectsOfType<Invincible>().Count > 0)
                {
                    damageAmount = 0;
                }


                //攻击提升在最前面
                if (damageAmount > 0)
                {
                    //DamageModifier
                    foreach (DamageModifier modifier in MenuControl.Instance.battleMenu
                                 .GetComponentsInChildren<DamageModifier>())
                    {
                        if (modifier.enabled)
                        {
                            damageAmount = modifier.ModifyAmount(sourceCard, ability, this, damageAmount);
                        }
                    }

                    damageAmount = Mathf.Max(0, damageAmount);
                }

                //reflective
                if (damageAmount > 0)
                {
                    if (firstDamage && GetEffectsOfType<ReflectEffect>().Count > 0)
                    {
                        Effect block = GetEffectsOfType<ReflectEffect>()[0];
                        if(block is ReflectEffect reflect)
                        {
                            if (reflect.consumeByUsage)
                            {
                                block.ConsumeCharges(ability, 1);
                            }
                        }
                        if (sourceCard is Unit unit)
                        {
                            unit.SufferDamage(sourceCard, ability, damageAmount, false, false,false);
                        }
                        else
                        {
                            sourceCard.player.GetHero().SufferDamage(sourceCard, ability, damageAmount, false, false,false);
                        }

                        return;
                    }
                }

                //Block
                if (damageAmount > 0)
                {
                    if (GetEffectsOfType<Block>().Count > 0)
                    {
                        Effect block = GetEffectsOfType<Block>()[0];
                        block.ConsumeCharges(ability, 1);
                        damageAmount = 0;
                    }

                    if (damageAmount > 0)
                    {
                        if (GetEffectsOfType<ShieldEffect>().Count > 0)
                        {
                            Effect shield = GetEffectsOfType<ShieldEffect>()[0];
                            var shieldValue = shield.remainingCharges;
                            var consumeChargeCount = Math.Min(shieldValue, damageAmount);
                            shield.ConsumeCharges(ability, consumeChargeCount);
                            damageAmount -= consumeChargeCount;
                        }
                    }
                }

                
                //false life
                if (damageAmount > 0)
                {
                    if (GetEffectsOfType<HPModifierSelf>().Count > 0)
                    {
                        for (int mi = GetEffectsOfType<HPModifierSelf>().Count-1; mi >= 0; mi--)
                        {
                            var modifier = GetEffectsOfType<HPModifierSelf>()[mi] as HPModifierSelf;
                            if (modifier.GetComponent<RemoveThisEffect>() &&
                                modifier.GetComponent<TriggerMultiUnitOnBoard>() && modifier.modifier == 1 &&
                                !modifier.GetComponent<PowerModifier>())
                            {
                                //其实就是先处理false life
                                var chargesToConsume = Math.Min(modifier.remainingCharges, damageAmount);
                                modifier.ConsumeCharges(ability, chargesToConsume);
                                damageAmount -= chargesToConsume;
                                extraDamageAmountToCalculate += chargesToConsume;
                            }
                        }
                    }
                }
            }
            else
            {
                damageAmount = GetHP();
            }

            if (MenuControl.Instance.settingsMenu.unitTakeDamage)
            {
                currentHP -= damageAmount;
            }


            //Trigger UnitTakesDamage event
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.UnitDamaged(sourceCard, this, ability, damageAmount+extraDamageAmountToCalculate, triggerUnitDamage);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }

            if (player != null && player == MenuControl.Instance.battleMenu.playerAI &&
                !MenuControl.Instance.battleMenu.tutorialMode)
            {
                MenuControl.Instance.heroMenu.damageDealtThisRun += damageAmount;
            }
        }
    }

    public virtual void RemoveEffectByTemplate(Card sourceCard, Ability ability, Effect templateEffect)
    {
        foreach (Effect effect in GetEffectsWithTemplate(templateEffect).ToArray())
        {
            RemoveEffect(sourceCard, ability, effect);
        }
    }

    public virtual void RemoveAllEffects(Card sourceCard, Ability ability)
    {
        for (int i = currentEffects.Count - 1; i >= 0; i--)
        {
            if (i >= currentEffects.Count)
            {
                continue;
            }

            var effect = currentEffects[i];
            if (effect.canBeRemoved)
            {
                RemoveEffect(sourceCard, ability, effect);
            }
        }
    }

    public virtual void RemoveEffect(Card sourceCard, Ability ability, Effect effect)
    {
        if (currentEffects.Contains(effect))
        {
            //Trigger Unit Removed Effect event
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.UnitRemovedEffect(this, ability, effect);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }

            if (!effect.IsEffectPersistent())
            {
                currentEffects.Remove(effect);
            }

            Destroy(effect.gameObject);
            effect.enabled = false;
            
            
            //Trigger Unit Removed Effect event
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.UnitFinishRemovedEffect(this, ability, effect);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }

    public virtual void ReduceEffect(Card sourceCard, Ability ability, Effect effectTemplate, int charges)
    {
        var effect = GetEffectsWithTemplate(effectTemplate)[0];
        effect.DoActionBeforeApply( sourceCard, GetTile(), -charges);
        effect.ConsumeCharges(ability, charges);
        // if (effectTemplate.chargesStack && GetEffectsWithTemplate(effectTemplate).Count > 0)
        // {
        //     effect.remainingCharges -= charges;
        //
        //     currentEffects.Remove(effect);
        //     if (effect.remainingCharges > 0)
        //     {
        //         currentEffects.Add(effect);
        //     }
        //     effect.DoActionBeforeApply( sourceCard, GetTile(), -charges);
        // }
        // else
        // {
        //     var effect = GetEffectsWithTemplate(effectTemplate)[0];
        //     effect.DoActionBeforeApply( sourceCard, GetTile(), -1);
        //     RemoveEffect(sourceCard, ability, effectTemplate);
        // }
        
    }

    public virtual Effect ApplyEffect(Card sourceCard, Ability ability, Effect effectTemplate, int charges)
    {
        int prevPower = GetPower();
        Effect effect = null;
        if (effectTemplate.chargesStack && GetEffectsWithTemplate(effectTemplate).Count > 0)
        {
            effect = GetEffectsWithTemplate(effectTemplate)[0];
        }
        else
        {
            effect = Instantiate(effectTemplate, transform);
        }

        
        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.UnitAppliedEffectBeforeCheck(this, ability, effect, charges);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }
        if (GetEffectsOfType<ImmuneEffect>().Count > 0)
        {
            foreach (ImmuneEffect immuneEffect in GetEffectsOfType<ImmuneEffect>())
            {
                if (immuneEffect.immuneEffect == effectTemplate)
                {
                    return null;
                }
            }
        }

        var tempEffect = Instantiate(effectTemplate, transform);

        if (tempEffect.hasConflict())
        {
            tempEffect?.DoActionWhenConflict(sourceCard, GetTile(), charges);
            if (tempEffect.stopApplyWhenConflict)
            {
                return null;
            }
        }
        
        if (!tempEffect.CanApply(sourceCard, GetTile(), charges))
        {
           // tempEffect?.DoActionBeforeApplyForcePerform(sourceCard, GetTile(), charges);
            
            return null;
        }

        tempEffect?.DoActionBeforeApply(sourceCard, GetTile(), charges);
        Destroy(tempEffect.gameObject);


       
        if (effectTemplate.chargesStack && GetEffectsWithTemplate(effectTemplate).Count > 0)
        {
            //effect = GetEffectsWithTemplate(effectTemplate)[0];
            effect.remainingCharges += charges;

            currentEffects.Remove(effect);
            currentEffects.Add(effect);
        }
        else
        {
            //effect = Instantiate(effectTemplate, transform);
            currentEffects.Add(effect);
            effect.originalTemplate = effectTemplate;
            effect.remainingCharges = charges;
        }

        effect.lastApplyTurnCount = MenuControl.Instance.battleMenu.currentTurn;


        //Trigger Unit Applied Effect event
        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.UnitAppliedEffect(this, ability, effect, charges);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }

        //Trigger Effect Charges Changed event
        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.EffectChargesChanged(ability, effect, 0);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }

        //Change power
        int newPower = GetPower();
        if (newPower != prevPower)
        {
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.UnitChangedPower(this, ability, prevPower);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }


        effect?.DoActionWhenApply(sourceCard, GetTile(), charges);

        return effect;
    }

    public virtual void InitializeUnit(bool keepEffects)
    {
        //Initialize card

        initialPower = ((Unit)cardTemplate).GetInitialPower(true, player);
        initialHP = ((Unit)cardTemplate).GetBaseHP(true, player);
        initialMoves = ((Unit)cardTemplate).initialMoves;
        initialActions = ((Unit)cardTemplate).initialActions;
        originalMovementType = movementType;

        currentHP = initialHP;
        currentPower = initialPower;

        originalActivatedAbility = activatedAbility;
        originalActionAnimation = actionAnimation;
        remainingMoves = GetInitialMoves();
        remainingActions = GetInitialActions();

        if (!keepEffects)
        {
            //this is a not very elegant way to fix effect that should still take effect after unit death
            //受到攻击触发的效果，在受击方死亡时也需要能触发（榴莲奇莫）
            LeanTween.delayedCall(MenuControl.Instance.battleMenu.GetPlaySpeed() * 4, () =>
            {
                foreach (Effect effect in currentEffects.ToArray())
                {
                    RemoveEffect(this, null, effect);
                }
            });
        }
    }

    public bool CanMove()
    {
        if (MenuControl.Instance.settingsMenu.infinitMoveAndActions &&
            (player == MenuControl.Instance.battleMenu.player1))
        {
            return true;
        }

        if (movementType == null) return false;

        if (GetEffectsOfType<Frozen>().Count > 0)
            return false;

        if (GetEffectsOfType<MagicFrozen>().Count > 0 && !(this is LargeHero))
        {
            return false;
        }

        if (GetEffectsOfType<Rooted>().Count > 0)
            return false;

        return remainingMoves > 0;
    }

    public bool CanAct()
    {
        if (MenuControl.Instance.settingsMenu.infinitMoveAndActions &&
            (player == MenuControl.Instance.battleMenu.player1))
        {
            return true;
        }

        if (activatedAbility == null) return false;

        if (GetEffectsOfType<Frozen>().Count > 0)
            return false;


        if (GetEffectsOfType<MagicFrozen>().Count > 0 && !(this is LargeHero))
        {
            return false;
        }

        if (GetEffectsOfType<Smoked>().Count > 0)
            return false;

        return remainingActions > 0;
    }

    public bool hasBlockRelatedEffect()
    {
        return GetEffectsOfType<Block>().Count != 0 || GetEffectsOfType<Invincible>().Count != 0;
    }

    public List<Effect> GetEffectsOfType<T>()
    {
        List<Effect> effects = new List<Effect>();
        foreach (Effect effect in currentEffects)
        {
            if (effect is T)
            {
                effects.Add(effect);
            }
        }

        return effects;
    }

    public List<Effect> GetEffectsWithTemplate(Effect template)
    {
        List<Effect> effects = new List<Effect>();
        if (template == null)
        {
            return effects;
        }

        foreach (Effect effect in currentEffects)
        {
            if (effect.originalTemplate.UniqueID == template.UniqueID)
            {
                effects.Add(effect);
            }
        }

        return effects;
    }

    public List<Effect> GetEffectsByID(string uniqueID)
    {
        List<Effect> effects = new List<Effect>();
        foreach (Effect effect in currentEffects)
        {
            if (effect.UniqueID == uniqueID)
            {
                effects.Add(effect);
            }
        }

        return effects;
    }

    public void ChangePower(Ability ability, int newValue)
    {
        int oldValue = currentPower;
        if (oldValue != newValue)
        {
            currentPower = Mathf.Max(0, newValue);

            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.UnitChangedPower(this, ability, oldValue);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }

    public void ReduceMaxHP(Ability ability, int reduceHP)
    {
        var newValue = GetBaseHP() - reduceHP;
        ChangeMaxHP(ability, newValue);
    }

    public virtual int GetInitialHP()
    {
        return initialHP;
    }
    public void ChangeMaxHP(Ability ability, int newValue)
    {
        int oldValue = GetBaseHP();
        if (newValue < currentHP)
        {
            ChangeCurrentHP(ability, newValue);
        }

        if (oldValue != newValue)
        {
            initialHP = Mathf.Max(0, newValue);

            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.UnitChangedMaxHP(this, ability, oldValue);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }

    public void ChangeMoves(Ability ability, int newValue)
    {
        int oldValue = remainingMoves;
        if (oldValue != newValue)
        {
            remainingMoves = Mathf.Max(0, newValue);

            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.UnitChangedMoves(this, ability, oldValue);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }

    public void ChangeActions(Ability ability, int newValue)
    {
        int oldValue = remainingActions;
        if (oldValue != newValue)
        {
            remainingActions = Mathf.Max(0, newValue);

            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.UnitChangedActions(this, ability, oldValue);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }

    public virtual void ChangeCurrentHP(Ability ability, int newValue)
    {
        int oldValue = currentHP;
        if (oldValue != newValue)
        {
            currentHP = Mathf.Max(0, newValue);

            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.UnitChangedCurrentHP(this, ability, oldValue);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }

    public virtual List<Tile> GetAdjacentTiles(int range = 1)
    {
        if (GetTile() != null)
        {
            return GetTile().GetAdjacentTilesLinear(range);
        }

        return new List<Tile>();
    }

    public virtual List<Tile> GetDiagonalTiles()
    {
        return GetTile().GetDiagonalTiles();
    }

    public bool IsAdjacentToUnit(Unit unit)
    {
        foreach (Tile tile in this.GetAdjacentTiles())
        {
            if (unit.GetTile() == tile)
            {
                return true;
            }

            if (unit is LargeHero && ((LargeHero)unit).GetTiles().Contains(tile))
            {
                return true;
            }
        }

        return false;
    }

    //当一个单位四面相邻均为敌方单位时的状态
    public bool IsPinned()
    {
        bool isPinned = true;
        foreach (Tile tile in GetAdjacentTiles())
        {
            if (tile != null && tile.GetUnit() == null)
            {
                isPinned = false;
            }

            if (tile != null && tile.GetUnit() != null && tile.GetUnit().player == player)
            {
                isPinned = false;
            }
        }

        return isPinned;
    }
}