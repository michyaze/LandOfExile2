using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ValueCompareType
{
    Equal,
    GreaterThan,
    LessThan
}

public enum PlayerType
{
    Self,
    Opponent,
    Both
}

public class TriggerCostUsage : Trigger
{
    public bool spellOnly = true;
    public int valueCheck = 0;
    public ValueCompareType type;
    public Ability anotherAbility;
    public bool triggerOnSelf = true;
    public PlayerType playerType;
    public bool triggerAfterFinished = false;

    public override void CardFinishedPlayed(Card card, Tile tile, List<Tile> tiles)
    {
         if (triggerAfterFinished)
        {
            InternalPlayed(card, tile, tiles);
        }
    }
    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (triggerAfterFinished)
        {
            return;
        }
        InternalPlayed(card, tile, tiles);
    }

    public  void InternalPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        {
            if (GetCard() is Unit unit)
            {
                if (GetCard().GetZone() != MenuControl.Instance.battleMenu.board)
                {
                    return;
                }
            }
        }
        if (playerType == PlayerType.Self && card.player != GetCard().player)
        {
            return;
        }

        if (playerType == PlayerType.Opponent && card.player == GetCard().player)
        {
            return;
        }

        if (triggerTime == 0)
        {
            return;
        }

        if (spellOnly && !card.isSpell())
        {
            return;
        }

        switch (type)
        {
            case ValueCompareType.Equal:
                if (card.GetCost() != valueCheck)
                {
                    return;
                }

                break;
            case ValueCompareType.GreaterThan:
                if (card.GetCost() <= valueCheck)
                {
                    return;
                }

                break;
            case ValueCompareType.LessThan:
                if (card.GetCost() >= valueCheck)
                {
                    return;
                }

                break;
        }

        if (triggerOnSelf)
        {
            if (GetCard() is Unit unit && unit.GetTile())
            {
                tile = unit.GetTile();
            }
        }

        triggerTime--;
        anotherAbility.PerformAbility(card, tile, card.GetCost());
    }
}