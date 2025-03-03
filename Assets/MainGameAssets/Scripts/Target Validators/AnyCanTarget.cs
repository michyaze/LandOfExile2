using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTarget : MonoBehaviour
{

    public Card GetCard()
    {
        return GetComponentInParent<Card>();
    }

    public virtual bool CanTarget(Card sourceCard, Tile tile)
    {
        return true;
    }
}
