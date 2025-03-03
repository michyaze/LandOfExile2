using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerFilter : MonoBehaviour
{
    public virtual bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        return true;
    }

    public Card GetCard()
    {
        return GetComponentInParent<Card>();
    }
}