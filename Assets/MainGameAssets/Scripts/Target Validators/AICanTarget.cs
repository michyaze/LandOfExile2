using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICanTarget : MonoBehaviour
{

    public Card GetCard()
    {
        return GetComponent<Card>();
    }

    public virtual bool CanTarget(Tile tile)
    {
        return true;
    }
}
