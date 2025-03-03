using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterSourceCardIsYourCard : TriggerFilter
{
    public Card cardTemplate;

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {

        return (sourceCard.player == GetCard().player && (cardTemplate == null || sourceCard.cardTemplate.UniqueID == cardTemplate.UniqueID));

    }
}
