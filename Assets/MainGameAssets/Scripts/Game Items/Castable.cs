using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castable : Card
{
    public bool hasToWearWeapon;//remove later
    public override bool CanTarget(Tile tile)
    {
        
        if (hasToWearWeapon && this.player.GetHero().weapon == null)
        {
            return false;
        }
        foreach (AnyCanTarget canTarget in MenuControl.Instance.battleMenu.GetComponentsInChildren<AnyCanTarget>())
        {
            if (!canTarget.CanTarget(this, tile)) return false;
        }

        if (player == MenuControl.Instance.battleMenu.playerAI && GetComponent<AICanTarget>() != null)
        {
            if (!GetComponent<AICanTarget>().CanTarget(tile)) return false;
        }

        //Check activated ability if any effects blocking targeting this tile (done in ability.cantargettile)
        if (GetZone() == MenuControl.Instance.battleMenu.hand || GetZone() == MenuControl.Instance.battleMenu.artifact)
        {
            if (activatedAbility != null && activatedAbility.CanTargetTile(this, tile))
            {
                return true;
            }
        }

        if (tile.GetUnit() && tile.GetUnit() is LargeHero)
        {
            foreach (Tile tile2 in ((LargeHero)tile.GetUnit()).GetTiles())
            {
                if (activatedAbility != null && activatedAbility.CanTargetTile(this, tile2))
                {
                    return true;
                }
            }
        }


        return false;
    }

    public bool CanTarget(List<Tile> tiles)
    {

        if (GetZone() == MenuControl.Instance.battleMenu.hand || GetZone() == MenuControl.Instance.battleMenu.artifact)
        {
            if (activatedAbility != null && ((MultiTargetAbility)activatedAbility).CanTargetTiles(this, tiles))
            {
                return true;
            }
        }

        return false;
    }

    public override void TargetTile(Tile tile, bool payCost)
    {

        if (payCost)
            player.PayCostFor(this);

        //If Casting from hand put into limbo
        if (player.cardsInHand.Contains(this))
        {
            player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.limbo);
        }

        if (activatedAbility != null)
        {

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

            activatedAbility.PerformAbility(this, tile);

            if (GetZone() == MenuControl.Instance.battleMenu.limbo)
                if (this.GetComponent<TriggerExhaustEndOfTurn>()||this .GetComponent<Exhaust>())
                {
                    this.ExhaustThisCard();
                    //player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.removedFromGame);
                }
                else
                {
                    if (!player.cardsRemovedFromGame.Contains(this))
                    {
                        //this.DiscardThisCard();
                        player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.discard);
                    }
                }


            //Trigger play card event 
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.CardFinishedPlayed(this, tile, null);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }

            }
        }

    }

    public override float PerformAnimationTime()
    {
        return activatedAbility.PerformAnimationTime(this);
    }


    public void TargetTiles(List<Tile> tiles, bool payCost)
    {

        //If Casting from hand pay cost
        if (player.cardsInHand.Contains(this))
        {
            if (payCost)
                player.PayCostFor(this);
        }

        player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.limbo);

        if (activatedAbility != null)
        {

            //Trigger play card event 
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.CardPlayed(this, tiles[0], tiles);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }

            }

            ((MultiTargetAbility)activatedAbility).PerformAbility(this, tiles);

            if (GetZone() != MenuControl.Instance.battleMenu.removedFromGame)
                player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.discard);
        }

    }
}
