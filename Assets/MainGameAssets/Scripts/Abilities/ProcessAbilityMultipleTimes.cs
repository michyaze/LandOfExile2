using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessAbilityMultipleTimes : Ability
{
    public Ability abilityToPerform;
    public int times = 2;
    public bool useChargesInstead;
    public bool isRandom = false;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int count = times;
        if (useChargesInstead) count = GetEffect().remainingCharges;
        if (isRandom)
        {
            
            List<Tile> tiles = new List<Tile>();
            foreach (Tile tile1 in ((Unit)GetCard()).GetTile().GetAdjacentTilesLinear())
            {
                // I forgot what this is, but the logic is ambiguous, please comment when we figured out
                if (!tiles.Contains(tile1) && tile1.isMoveable())
                {
                    tiles.Add(tile1);
                }
            }
            tiles.Shuffle();
        
            for (int ii = 0; ii < Mathf.Min(count,tiles.Count); ii += 1)
            {
                abilityToPerform.PerformAbility(sourceCard, tiles[ii], amount);
            }
        }
        else
        {
            for (int ii = 0; ii < count; ii += 1)
            {
                abilityToPerform.PerformAbility(sourceCard, targetTile, amount);
            }
        }
    }

  
}
