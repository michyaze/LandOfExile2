using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceReplaceWithTreasure : EventChoice
{

    public int reduceHP = 4;
    
    public override void PerformChoice()
    {
        MenuControl.Instance.eventMenu.CloseMenu();

        int seed = MenuControl.Instance.ApplySeed();
        Random.InitState(seed + MenuControl.Instance.adventureMenu.currentMapTileIndex);

        int index = MenuControl.Instance.adventureMenu.mapTiles[MenuControl.Instance.adventureMenu.currentMapTileIndex].adventureItemIndex;

        MenuControl.Instance.adventureMenu.adventureItems[index] = MenuControl.Instance.adventureMenu.GetTreasure();

        // var treasures = MenuControl.Instance.heroMenu.GetAllUnlockedTreasures();
        // List<Card> cards = new List<Card>();
        // for (int i = 0; i < 3; i++)
        // {
        //     cards.Add(treasures.PickItem());
        // }
        List<Card> cards = MenuControl.Instance.adventureMenu.GetTreasure().MakeCardList();
        for (int xx = 0; xx < cards.Count; xx += 1)
        {
            MenuControl.Instance.adventureMenu.itemCards.Add(cards[xx]);
            MenuControl.Instance.adventureMenu.addToItemCardsForItemIndex(index);
        }

        MenuControl.Instance.heroMenu.hero.ReduceMaxHP(null,reduceHP);
        
        MenuControl.Instance.dataControl.SaveData();
        MenuControl.Instance.adventureMenu.RenderScreen();
        MenuControl.Instance.adventureMenu.adventureItems[index].PerformItem(index);
    }
}
