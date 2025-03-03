using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTargetAbility : Ability
{
    public int minTargets = 2;
    public int maxTargets = 2;

    public virtual void PerformAbility(Card sourceCard, List<Tile> targetTiles, int amount = 0)
    {

    }
    public virtual bool CanTargetTiles(Card card, List<Tile> tiles)
    {

        return false; 
    }
}