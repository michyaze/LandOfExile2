using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureItemPurchaseCards : AdventureItemKnownEvent
{
    public List<Card> MakeCardList()
    {

        return MenuControl.Instance.heroMenu.heroClass.GetComponent<ClassSpecialization>().CalculatePurchaseDrops();

    } 
}
