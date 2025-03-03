using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterChargesOfThisEffect : TriggerFilter
{
    public int min = -1;
    public int max = -1;

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (GetComponent<Effect>() != null)
		{
            if (min >= 0 && GetComponent<Effect>().remainingCharges < min) return false;
            if (max >= 0 && GetComponent<Effect>().remainingCharges > max) return false;

            return true;

        }

        return false;

    }
}
