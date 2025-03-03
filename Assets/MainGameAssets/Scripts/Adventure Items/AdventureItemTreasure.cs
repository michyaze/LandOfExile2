using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureItemTreasure : AdventureItemKnownEvent
{

    public List<Card> MakeCardList()
    {
        return MenuControl.Instance.heroMenu.heroClass.GetComponent<ClassSpecialization>().CalculateChestDrops();

    }
}
