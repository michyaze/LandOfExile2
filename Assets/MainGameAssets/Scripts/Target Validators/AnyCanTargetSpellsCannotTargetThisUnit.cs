using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTargetSpellsCannotTargetThisUnit : AnyCanTarget
{

    public override bool CanTarget(Card sourceCard, Tile tile)
    {
        if (sourceCard is Castable && sourceCard.cardTags.Contains(MenuControl.Instance.spellTag))
        {
            if (tile == ((Unit)GetCard()).GetTile() || (GetCard() is LargeHero && ((LargeHero)GetCard()).GetTiles().Contains(tile)))
            {
                return false;
            }

        }

        return true;
    }
}
