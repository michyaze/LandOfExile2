using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventTargeting : Effect
{

    public virtual bool CanTarget(Card soureCard, Tile targetTile)
    {
        return true;
    }

}
