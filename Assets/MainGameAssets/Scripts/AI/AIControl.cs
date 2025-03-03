using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIControl : ScriptableObject
{
    public bool ignoreBurnMoveCheck;
    public bool ignoreBurnDeathCheck;
    
    
    public virtual bool UnitsAttackLogic(Player player)
    {
        if (!UnitsHitTauntEnemy(player)) return false;
        
        if (!UnitsMoveAndHitTauntEnemy(player)) return false;
        
        if (!UnitsHitEnemyHero(player)) return false;
        
        if (!UnitsMoveAndKillEnemyHero(player)) return false;

        if (!UnitsCanKillMinions(player)) return false;
        
        if (!UnitsHitEnemyHero(player,false)) return false;

        if (!UnitsMoveToHitEnemyHero(player)) return false;

        if (!UnitsCanMoveToKillMinions(player)) return false;

        if (!UnitsHitEnemiesInRange(player)) return false;

        if (!UnitsMoveToHitEnemy(player)) return false;
        return true;
    }

    public virtual bool TakeAITurn(Player player)
    {

        return true;
    }

    public virtual bool BuffingCastablesMinions(Player player)
    {

        //Castables - buffs
        foreach (Card card in player.cardsInHand)
        {
            if (card is Castable && card.CanAffordCost())
            {
                List<Minion> minions = player.GetMinionsOnBoard();
                minions.Shuffle();
                foreach (Unit unit in minions)
                {
                    if (card.CanTarget(unit.GetTile()))
                    {
                        MenuControl.Instance.battleMenu.AnimationTargetAction(card.player.GetVisibleCardForCard(card), unit.GetTile());
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public virtual bool BuffingCastablesHero(Player player)
    {

        //Castables - buffs
        foreach (Card card in player.cardsInHand)
        {
            if (card is Castable && card.CanAffordCost())
            {

                if (card.CanTarget(card.player.GetHero().GetTile()))
                {

                    MenuControl.Instance.battleMenu.AnimationTargetAction(card.player.GetVisibleCardForCard(card), card.player.GetHero().GetTile());
                    return false;
                }

            }
        }

        return true;
    }

    public virtual bool OffensiveCastables(Player player)
    {
        //Castables - offensive
        foreach (Card card in player.cardsInHand)
        {
            if (card is Castable && card.CanAffordCost())
            {
                foreach (Unit enemy in player.GetOpponent().cardsOnBoard)
                {
                    if (card.CanTarget(enemy.GetTile()))
                    {

                        MenuControl.Instance.battleMenu.AnimationTargetAction(card.player.GetVisibleCardForCard(card), enemy.GetTile());
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public virtual bool UnitsHitEnemyHero(Player player,bool ignoreBlock = true)
    {
        //See if any units can hit the enemy hero then do so
        if (ignoreBlock && player.GetOpponent().GetHero().hasBlockRelatedEffect())
        {
            return true;
        }
        foreach (Unit unit in player.cardsOnBoard)
        {
            if (unit.GetPower() > 0)
            {
                if (unit.CanTarget(player.GetOpponent().GetHero().GetTile()))
                {

                    MenuControl.Instance.battleMenu.AnimationTargetAction(unit.player.GetVisibleBoardCardForCard(unit), player.GetOpponent().GetHero().GetTile());
                    return false;
                }
            }
        }

        return true;
    }

    public virtual bool UnitsMoveAndKillEnemyHero(Player player,bool ignoreBlock = true)
    {
        //See if any units can move into an adjacent square to kill the enemy hero
        if (ignoreBlock && player.GetOpponent().GetHero().hasBlockRelatedEffect())
        {
            return true;
        }
        foreach (Unit unit in player.cardsOnBoard)
        {
            if (unit.GetPower() > 0 && BurnCanMoveCheck(unit))
            {
                if (unit.CanMove() && unit.GetPower() >= player.GetOpponent().GetHero().GetHP())
                {
                    Tile originalTile = unit.GetTile();
                    foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
                    {
                        if (tile.isMoveable() && unit.CanTarget(tile))
                        {
                            unit.MoveToTile(tile);
                            if (unit.CanTarget(player.GetOpponent().GetHero().GetTile()))
                            {
                                unit.MoveToTile(originalTile);
                                MenuControl.Instance.battleMenu.AnimationTargetAction(unit.player.GetVisibleBoardCardForCard(unit), tile);
                                return false;
                            }
                            else
                            {
                                unit.MoveToTile(originalTile);
                            }
                        }
                    }

                }
            }
        }

        return true;
    }

    public virtual bool UnitsCanKillMinions(Player player)
    {
        //See if any units can kill any enemy minions and do it WITHOUT BLOCK
        foreach (Unit enemyUnit in player.GetOpponent().cardsOnBoard)
        {
            if (enemyUnit is Minion)
            {
                foreach (Unit unit in player.cardsOnBoard)
                {
                    if (unit.GetPower() >= enemyUnit.GetHP() && unit.CanTarget(enemyUnit.GetTile()) && !enemyUnit.hasBlockRelatedEffect())
                    {
                        MenuControl.Instance.battleMenu.AnimationTargetAction(unit.player.GetVisibleBoardCardForCard(unit), enemyUnit.GetTile());
                        return false;
                    }

                }
            }
        }

        return true;
    }

    public virtual bool UnitsCanMoveToKillMinions(Player player)
    {
        //See if any units can move to kill any enemy minions WITHOUT BLOCK
        foreach (Unit enemyUnit in player.GetOpponent().cardsOnBoard)
        {
            foreach (Unit unit in player.cardsOnBoard)
            {
                if (unit.CanMove() && unit.GetPower() >= enemyUnit.GetHP() && BurnCanMoveCheck(unit))
                {
                    Tile originalTile = unit.GetTile();
                    foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
                    {
                        if (tile.isMoveable() && unit.CanTarget(tile))
                        {
                            unit.MoveToTile(tile);
                            if (unit.CanTarget(enemyUnit.GetTile()) && !enemyUnit.hasBlockRelatedEffect())
                            {
                                unit.MoveToTile(originalTile);
                                if (unit is Minion || CheckHeroWontDieAfterMove((Hero)unit, tile, enemyUnit))
                                {
                                    MenuControl.Instance.battleMenu.AnimationTargetAction(unit.player.GetVisibleBoardCardForCard(unit), tile);
                                    return false;
                                }
                            }

                            unit.MoveToTile(originalTile);

                        }
                    }

                }
            }
        }

        return true;
    }
    
    
    public virtual bool UnitsHitTauntEnemy(Player player)
    {
        //hit the enemies in range
        foreach (Unit enemyUnit in player.GetOpponent().cardsOnBoard)
        {
            if (enemyUnit.GetEffectsOfType<TauntEffect>().Count == 0)
            {
                continue;
            }
            foreach (Unit unit in player.cardsOnBoard)
            {
                if (unit.GetPower() > 0)
                {
                    if (unit.CanTarget(enemyUnit.GetTile()))
                    {
                        MenuControl.Instance.battleMenu.AnimationTargetAction(unit.player.GetVisibleBoardCardForCard(unit), enemyUnit.GetTile());
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public virtual bool UnitsHitEnemiesInRange(Player player)
    {
        //hit the enemies in range
        foreach (Unit enemyUnit in player.GetOpponent().cardsOnBoard)
        {
            foreach (Unit unit in player.cardsOnBoard)
            {
                if (unit.GetPower() > 0)
                {
                    if (unit.CanTarget(enemyUnit.GetTile()))
                    {
                        MenuControl.Instance.battleMenu.AnimationTargetAction(unit.player.GetVisibleBoardCardForCard(unit), enemyUnit.GetTile());
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public virtual bool UnitsMoveToHitEnemyHero(Player player)
    {
        //move to hit an enemy
        Hero enemyUnit = player.GetOpponent().GetHero();

        foreach (Unit unit in player.cardsOnBoard)
        {
            if (unit.GetPower() > 0 && BurnCanMoveCheck(unit))
            {
                if (unit.CanMove())
                {
                    Tile originalTile = unit.GetTile();
                    foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
                    {
                        if (tile == unit.GetTile())
                        {
                            continue;
                        }

                        var moveable = tile.isMoveable() || tile.GetUnit() == unit;
                        if (moveable && unit.CanTarget(tile))
                        {
                            unit.MoveToTile(tile);
                            if (unit.CanTarget(enemyUnit.GetTile()))
                            {
                                unit.MoveToTile(originalTile);

                                if (unit is Minion || CheckHeroWontDieAfterMove((Hero)unit, tile, null))
                                {
                                    MenuControl.Instance.battleMenu.AnimationTargetAction(unit.player.GetVisibleBoardCardForCard(unit), tile);
                                    return false;
                                }
                            }
                            unit.MoveToTile(originalTile);

                        }
                    }

                }
            }
        }

        return true;
    }
    public virtual bool UnitsMoveAndHitTauntEnemy(Player player)
    {
        //move to hit an enemy
        foreach (Unit enemyUnit in player.GetOpponent().cardsOnBoard)
        {
            
            if (enemyUnit.GetEffectsOfType<TauntEffect>().Count == 0)
            {
                continue;
            }
            foreach (Unit unit in player.cardsOnBoard)
            {
                if (unit.GetPower() > 0 && BurnCanMoveCheck(unit))
                {
                    if (unit.CanMove())
                    {
                        Tile originalTile = unit.GetTile();
                        foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
                        {
                            if (tile.isMoveable() && unit.CanTarget(tile))
                            {
                                unit.MoveToTile(tile);
                                if (unit.CanTarget(enemyUnit.GetTile()))
                                {
                                    unit.MoveToTile(originalTile);

                                    if (unit is Minion || CheckHeroWontDieAfterMove((Hero)unit, tile, null))
                                    {
                                        MenuControl.Instance.battleMenu.AnimationTargetAction(unit.player.GetVisibleBoardCardForCard(unit), tile);
                                        return false;
                                    }
                                }
                                else
                                {
                                    unit.MoveToTile(originalTile);
                                }

                            }
                        }

                    }
                }
            }
        }

        return true;
    }
    public virtual bool UnitsMoveToHitEnemy(Player player)
    {
        //move to hit an enemy
        foreach (Unit enemyUnit in player.GetOpponent().cardsOnBoard)
        {
            foreach (Unit unit in player.cardsOnBoard)
            {
                if (unit.GetPower() > 0 && BurnCanMoveCheck(unit))
                {
                    if (unit.CanMove())
                    {
                        Tile originalTile = unit.GetTile();
                        foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
                        {
                            if (tile.isMoveable() && unit.CanTarget(tile))
                            {
                                unit.MoveToTile(tile);
                                if (unit.CanTarget(enemyUnit.GetTile()))
                                {
                                    unit.MoveToTile(originalTile);

                                    if (unit is Minion || CheckHeroWontDieAfterMove((Hero)unit, tile, null))
                                    {
                                        MenuControl.Instance.battleMenu.AnimationTargetAction(unit.player.GetVisibleBoardCardForCard(unit), tile);
                                        return false;
                                    }
                                }
                                unit.MoveToTile(originalTile);

                            }
                        }

                    }
                }
            }
        }

        return true;
    }

    public virtual bool HeroMoveAndOffensiveCastables(Player player)
    {
        
        List<Card> validCards = new List<Card>();
        foreach (Card card in player.cardsInHand)
        {
            if (card is Castable && card.CanAffordCost())
            {
                validCards.Add(card);
            }
        }

        if (validCards.Count == 0)
        {
            return true;
        }

        //if (player.GetHero().CanMove())
        {
            List<Tile> validTiles = new List<Tile>();
            var unit = player.GetHero();
            var originalTile = unit.GetTile();
            if (unit.CanMove() && BurnShouldMoveCheck(unit))
            {
                foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
                {
                    if ((tile.isMoveable() || tile.GetUnit() == unit) && tile != unit.GetTile() &&
                        unit.CanTarget(tile))
                    {
                        validTiles.Add(tile);
                    }
                }

                //Choose the tile that is closest to enemy hero
                if (validTiles.Count > 0)
                {

                    foreach (Tile nextTile in validTiles.OrderBy(x => Vector2.Distance(x.transform.position,
                                 player.GetOpponent().GetHero().GetTile().transform.position)))
                    {
                        if (CheckMaintainPinOnEnemyHero(unit, nextTile))
                        {
                            if (CheckHeroWontDieAfterMove((Hero)unit, nextTile, null))
                            {
                                    unit.MoveToTile(nextTile);
                                    
                                    //finalValidTiles.Add(nextTile);
                                    foreach (Card card in validCards)
                                    {
                                            foreach (Unit enemy in player.GetOpponent().cardsOnBoard)
                                            {
                                                if (card.CanTarget(enemy.GetTile()))
                                                {

                                                    MenuControl.Instance.battleMenu.AnimationTargetAction(
                                                        unit.player.GetVisibleBoardCardForCard(unit), nextTile);
                                                    MenuControl.Instance.battleMenu.AnimationTargetAction(card.player.GetVisibleCardForCard(card), enemy.GetTile());
                                                    return false;
                                                }
                                            }
                                        
                                    }
                                    
                                    
                                    unit.MoveToTile(originalTile);

                                
                                
                                
                            }
                        }

                    }

                }
            }
        }
        //Castables - offensive
        

        return true;
    }

    List<Tile> SortTiles(List<Tile> tiles,Tile start, bool findNextUnit,Tile ExtraTile = null)
    {
        List<Tile> res = new List<Tile>();
        
        
        //对于每个tile，需要计算它到target的距离。需要走一个bfs,从start出发，找到到每个tile的最短距离，并且返回一个tile列表，按照距离排序
        
        Queue<(Tile tile,int move)> queue = new Queue<(Tile,int)>();
        HashSet<Tile> visited = new HashSet<Tile>();
        queue.Enqueue((start,0));
        visited.Add((start));
        int test = 0;
        while(queue.Count>0)
        {
            //bfs
            var queueIntem = queue.Dequeue();
            var tile = queueIntem.tile;
            int move = queueIntem.move;
            if (tiles.Contains(tile))
            {
                res.Add(tile);
                tiles.Remove(tile);
            }

            if (!findNextUnit && tile == ExtraTile)
            {
                //找路线，如果已经找到unit的当前位置了，那就别找了
            }

            // if (!tile.isMoveable() && start!=tile)
            // {
            //     continue;
            // }
            test++;
            if (test >= 1000)
            {
                break;
            }
            foreach (var nextTile in tile.GetAdjacentTilesLinear())
            {
                //如果是找下一个unit，则无视unit，只看障碍物
                if (!visited.Contains(nextTile) && findNextUnit?nextTile.isMoveableIgnoreFriends():nextTile.isMoveable())
                {
                    queue. Enqueue((nextTile,move+1));
                    visited.Add((nextTile));
                }
            }
        }

        if (res.Count == 0 && ExtraTile!=null && !findNextUnit) //找路线失败，试试无视unit
        {
            return SortTiles(tiles, start, true, ExtraTile);
        }
        return res;
    }

    public virtual bool UnitsMoveCloserToEnemyHero(Player player)
    {
        
        try
        {
            var cardsOnBoard = new List<Card>();
            foreach (var card in player.cardsOnBoard)
            {
                if (card is Unit unit && unit.GetTile())
                {
                    cardsOnBoard.Add(card);
                }
                else
                {
                    
                    Debug.LogError( "Unit is not on board?");
                }
            }

            var unitTiles = new List<Tile>();
            foreach (var unit in cardsOnBoard)
            {
                unitTiles.Add((unit as Unit).GetTile());
            }

            if (player.cardsOnBoard.Count > 7)
            {
                //不避障了
                //Move units closer to enemies
                foreach (Unit unit in cardsOnBoard.OrderBy(x =>
                             Vector2.Distance(((Unit)x).GetTile().transform.position,
                                 player.GetOpponent().GetHero().GetTile().transform.position)))
                {
                    if (unit.CanMove() && BurnShouldMoveCheck(unit))
                    {
                        List<Tile> validTiles = new List<Tile>();
                        foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
                        {
                            if ((tile.isMoveable() || tile.GetUnit() == unit) && tile != unit.GetTile() &&
                                unit.CanTarget(tile))
                            {
                                validTiles.Add(tile);
                            }
                        }

                        //Choose the tile that is closest to enemy hero
                        if (validTiles.Count > 0)
                        {

                            foreach (Tile nextTile in validTiles.OrderBy(x => Vector2.Distance(x.transform.position,
                                         player.GetOpponent().GetHero().GetTile().transform.position)))
                            {
                                if (CheckMaintainPinOnEnemyHero(unit, nextTile))
                                {
                                    if (unit is Minion || CheckHeroWontDieAfterMove((Hero)unit, nextTile, null))
                                    {
                                        MenuControl.Instance.battleMenu.AnimationTargetAction(
                                            unit.player.GetVisibleBoardCardForCard(unit), nextTile);
                                        return false;
                                    }
                                }

                            }

                        }
                    }
                }
            }
            else
            {
                unitTiles = SortTiles(unitTiles,player.GetOpponent().GetHero().GetTile(),true);
                //Move units closer to enemies
            while(unitTiles.Count>0)
            {
                var unit = unitTiles[0].GetUnit();
                unitTiles.RemoveAt(0);
                if (unit.CanMove() && BurnShouldMoveCheck(unit))
                {
                    List<Tile> validTiles = new List<Tile>();
                    foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
                    {
                        if ((tile.isMoveable() || tile.GetUnit() == unit) && tile != unit.GetTile() &&
                            unit.CanTarget(tile))
                        {
                            validTiles.Add(tile);
                        }
                    }

                    //Choose the tile that is closest to enemy hero
                    if (validTiles.Count > 0)
                    {

                        if (unit is LargeHero)
                        {
                            validTiles = validTiles.OrderBy(x => Vector2.Distance(x.transform.position,
                                player.GetOpponent().GetHero().GetTile().transform.position)).ToList();
                        }
                        else
                        {
                            validTiles = SortTiles(validTiles,player.GetOpponent().GetHero().GetTile(),false,unit.GetTile());
                        }
                        
                        foreach (Tile nextTile in validTiles/*.OrderBy(x => Vector2.Distance(x.transform.position,
                                     player.GetOpponent().GetHero().GetTile().transform.position))*/)
                        {
                            if (CheckMaintainPinOnEnemyHero(unit, nextTile))
                            {
                                if (unit is Minion || CheckHeroWontDieAfterMove((Hero)unit, nextTile, null))
                                {
                                    MenuControl.Instance.battleMenu.AnimationTargetAction(
                                        unit.player.GetVisibleBoardCardForCard(unit), nextTile);
                                    return false;
                                }
                            }

                        }

                    }
                }
                //排序所有敌人，根据到我方英雄的顺序。无视我方英雄以外的卡牌
                unitTiles = SortTiles(unitTiles,player.GetOpponent().GetHero().GetTile(),true);
            }
            }
            
        }catch (Exception e)
        {
            Debug.LogError(e.Message);
        }

        return true;
    }

    public virtual bool SummonMeleeMinionsCloseToEnemyHero(Player player)
    {
        //Summon minions 
        foreach (Card card in player.cardsInHand)
        {
            if (card is Minion && card.CanAffordCost())
            {
                Minion minion = (Minion)card;
                if (minion.cardTags.Contains(MenuControl.Instance.summonInFrontLikeMeleeTag) || (minion.activatedAbility != null && minion.activatedAbility is Attack
                && ((Attack)minion.activatedAbility).GetTargetValidator() is TargetLinear && ((TargetLinear)((Attack)minion.activatedAbility).GetTargetValidator()).range == 1))
                {
                    List<Tile> boardTiles = new List<Tile>();
                    boardTiles.AddRange(MenuControl.Instance.battleMenu.boardMenu.GetAllEmptyTiles().OrderBy(x => Vector2.Distance(x.transform.position, player.GetOpponent().GetHero().GetTile().transform.position)));
                    boardTiles = MoveDangerousTilesToEnd(boardTiles);

                    foreach (Tile tile in boardTiles)
                    {
                        if (card.CanTarget(tile))
                        {
                            MenuControl.Instance.battleMenu.AnimationTargetAction(card.player.GetVisibleCardForCard(card), tile);
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    public virtual bool SummonMinionsFarFromEnemyHero(Player player)
    {
        //Summon minions 
        foreach (Card card in player.cardsInHand)
        {
            if (card is Minion && card.CanAffordCost())
            {
                Minion minion = (Minion)card;

                List<Tile> boardTiles = new List<Tile>();
                boardTiles.AddRange(MenuControl.Instance.battleMenu.boardMenu.GetAllEmptyTiles().OrderByDescending(x => Vector2.Distance(x.transform.position, player.GetOpponent().GetHero().GetTile().transform.position)));
                boardTiles = MoveDangerousTilesToEnd(boardTiles);

                foreach (Tile tile in boardTiles)
                {
                    if (card.CanTarget(tile))
                    {
                        MenuControl.Instance.battleMenu.AnimationTargetAction(card.player.GetVisibleCardForCard(card), tile);
                        return false;
                    }
                }

            }
        }

        return true;
    }

    public virtual bool UnitsAidFriendlyUnits(Player player)
    {
        //Units aid friendly Units
        foreach (Unit myUnit in player.cardsOnBoard)
        {
            foreach (Unit unit in player.cardsOnBoard)
            {
                if (unit != myUnit)
                {
                    if (unit.CanTarget(myUnit.GetTile()))
                    {
                        MenuControl.Instance.battleMenu.AnimationTargetAction(unit.player.GetVisibleBoardCardForCard(unit), myUnit.GetTile());
                        return false;
                    }
                }
            }
        }

        return true;
    }


    public Tile ClosestTile(Tile currentTile, List<Tile> validTiles, List<Tile> alreadyCheckedTiles)
    {

        foreach (Tile tile in currentTile.GetAdjacentTilesLinear())
        {
            if (validTiles.Contains(tile))
            {
                return tile;
            }
            else if (!alreadyCheckedTiles.Contains(tile))
            {
                alreadyCheckedTiles.Add(tile);
                Tile newTile = ClosestTile(tile, validTiles, alreadyCheckedTiles);
                if (newTile != null) return newTile;
            }

        }
        return null;
    }

    public bool BurnShouldMoveCheck(Unit unit)
    {
        if (ignoreBurnMoveCheck) return true;

        Effect burnEffect = null;
        foreach (Effect effect in unit.currentEffects)
        {
            if (effect.UniqueID == "CommonEffect12")
            {
                burnEffect = effect;
                break;
            }
        }

        if (burnEffect != null)
        {
            return false;
        }
        return true;
    }

    public bool BurnCanMoveCheck(Unit unit)
    {
        if (ignoreBurnDeathCheck) return true;

        Effect burnEffect = null;
        foreach (Effect effect in unit.currentEffects)
        {
            if (effect.UniqueID == "CommonEffect12")
            {
                burnEffect = effect;
                break;
            }
        }

        if (burnEffect != null)
        {
            int stacks = burnEffect.remainingCharges;

            if (unit.GetHP() <= stacks)
            {
                return false;
            }
        }

        return true;
    }

    public bool CheckMaintainPinOnEnemyHero(Unit unitToCheck, Tile emptyTile)
    {
        if (emptyTile.GetUnit() != null) return true;

        //Dont move units away from enemy hero unless another friendly minion can move into the same square to maintain the pin
        if (unitToCheck.GetAdjacentTiles().Contains(unitToCheck.player.GetOpponent().GetHero().GetTile()))
        {
            //units that can move to replace spot
            foreach (Unit unit in unitToCheck.player.cardsOnBoard)
            {
                if (unitToCheck != unit && unit.CanMove() && BurnShouldMoveCheck(unit))
                {
                    Tile originalTile = unitToCheck.GetTile();
                    unitToCheck.MoveToTile(emptyTile);
                    if (unit.CanTarget(originalTile))
                    {
                        unitToCheck.MoveToTile(originalTile);
                        return true;
                    }
                    unitToCheck.MoveToTile(originalTile);
                }
            }
            return false;
        }

        return true;
    }

    public bool CheckHeroWontDieAfterMove(Hero hero, Tile emptyTile, Unit unitToKill)
    {
        if (emptyTile.GetUnit() != null) return true;
        if (hero.remainingMoves > 1) return true;

        Tile originalTile = hero.GetTile();
        hero.MoveToTile(emptyTile);
        int powerOnTile = 0;
        foreach (Card card in hero.player.GetOpponent().cardsOnBoard)
        {
            Unit unit = (Unit)card;

            if (unitToKill == null || unit != unitToKill)
            {
                if (unit.CanTarget(emptyTile))
                {
                    powerOnTile += unit.GetPower(unit, hero);
                }
            }
            if (unit.IsAdjacentToUnit(hero) && unit.GetComponentInChildren<TriggerGuardian>() != null && unit.GetComponentInChildren<TriggerGuardian>().canTrigger)
            {
                powerOnTile += unit.GetPower(unit, hero);
            }
        }
        hero.MoveToTile(originalTile);

        if (powerOnTile >= hero.GetHP() && !hero.hasBlockRelatedEffect() && hero.GetEffectsOfType<DamageModifierImmuneToAttacks>().Count == 0 && hero.GetEffectsOfType<DamageModifierImmuneUnlessSurounded>().Count == 0 && hero.GetEffectsOfType<DamageModifierImmuneExceptFromUnitsWithEffect>().Count == 0)
        {
            return false;
        }

        return true;
    }

    public List<Tile> MoveDangerousTilesToEnd(List<Tile> tiles)
    {
        foreach (Tile tile in tiles.ToArray())
        {
            foreach (Tile adjacentTile in tile.GetAdjacentTilesLinear(1))
            {
                if (adjacentTile.GetUnit() != null && adjacentTile.GetUnit().player == MenuControl.Instance.battleMenu.player1 && adjacentTile.GetUnit().GetComponentInChildren<TriggerGuardian>())
                {
                    tiles.Remove(tile);
                    tiles.Add(tile);
                }
            }
        }

        return tiles;
    }
}


