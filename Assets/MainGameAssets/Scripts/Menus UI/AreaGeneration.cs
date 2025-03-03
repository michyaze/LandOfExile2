using System;
using System.Collections;
using System.Collections.Generic;
using Sinbad;
using UnityEngine;
using Random = UnityEngine.Random;


public class AreaGeneration : MonoBehaviour
{
    public float yOffset = 1;

    //public Area area;
    public virtual void GenerateItems(int areaId)
    {
        generatedEnemyGroup = new HashSet<string>();
        //Spawn Map Tiles
        MenuControl.Instance.adventureMenu.mapTiles.Clear();

        //read csv files, get all events.
        
        //var positionGroupInfos = CsvUtil.LoadObjects<PositionGroupInfo>("positionGroup");
        //for each event, get its spawn position group, and randomly pick one, then spawn it.
        
        //show it or not is done in adventureMenu

        var positionGroupsByGroupId = new Dictionary<int, List<PositionGroupInfo>>();
        foreach (var pair in MenuControl.Instance.csvLoader.positionGroupsByGroupId)
        {
            positionGroupsByGroupId[pair.Key] = new List<PositionGroupInfo>(pair.Value);
        }

        var allRandomEvents = MenuControl.Instance.adventureMenu.GetRandomEvents();

        int test = 0;
        int infoID = 0;
        foreach (var mapEvent in MenuControl.Instance.csvLoader.mapEventInfos)
        {
            test++;
            if (mapEvent.eventId == 0)
            {
                //it's a bug, but how
                continue;
            }

            if (mapEvent.level != areaId)
            {
                continue;
            }
            var eventID = 0;
            if (mapEvent.eventType == "出生点")
            {
                MenuControl.Instance.adventureMenu.playerChess.transform.localPosition =
                    new Vector2(mapEvent.position.x, mapEvent.position.y);
                continue;
            }
            EventTile firstTile = Instantiate(MenuControl.Instance.adventureMenu.mapEventPrefab, MenuControl.Instance.adventureMenu.spawnPos);
            if (mapEvent.position.x == float.NegativeInfinity)
            {
                // it should have a group position id
                var positionGroupId = mapEvent.positionGroup;
                if (positionGroupId == 0)
                {
                    Debug.LogError(mapEvent.eventId+"的 position group 不存在");
                }

                if (!positionGroupsByGroupId.ContainsKey(positionGroupId))
                {
                    
                    Debug.LogError(mapEvent.eventId+"的 position group "+positionGroupId+" 没有在 positionGroup.csv中定义");
                }

                if (positionGroupsByGroupId[positionGroupId].Count == 0)
                {
                    Debug.LogError(mapEvent.eventId+"的 position group 数量为0");
                }
                var pickPosition = positionGroupsByGroupId[positionGroupId].PickItem();

                firstTile.posX = pickPosition.position.x * MenuControl.Instance.areaMenu.currentArea.xScale;
                firstTile.posY = (pickPosition.position.y+yOffset) * MenuControl.Instance.areaMenu.currentArea.yScale;
                
                
                
                firstTile.transform.localPosition =  new Vector2(firstTile.posX,firstTile.posY );
            }
            else
            {
                firstTile.posX = mapEvent.position.x * MenuControl.Instance.areaMenu.currentArea.xScale;
                firstTile.posY = (mapEvent.position.y+yOffset) * MenuControl.Instance.areaMenu.currentArea.yScale;
                firstTile.transform.localPosition =  new Vector2(firstTile.posX ,firstTile.posY );
                
            }

            var scale = MenuControl.Instance.adventureMenu.eventItemScale;
            firstTile.transform.localScale = new Vector3(scale, scale, scale);

            switch (mapEvent.eventType)
            {
                case "吟游诗人":
                    MenuControl.Instance.adventureMenu.adventureItemInfos.Add(new AdventureItemInfo());
                    //铁匠铺
                    MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetBlacksmith());
                    
                    //MenuControl.Instance.shopMenu.ShowShopUpgrade(MenuControl.Instance.adventureMenu.GetBlacksmith().GetName());
                    break;
                
                case "治疗师":
                    MenuControl.Instance.adventureMenu.adventureItemInfos.Add(new AdventureItemInfo());
                    MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetShrine());
                    break;
                case "旅行商人":
                    MenuControl.Instance.adventureMenu.adventureItemInfos.Add(new AdventureItemInfo());
                    //店主
                    MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetShop());
                    List<Card> cards = ((AdventureItemPurchaseCards)MenuControl.Instance.adventureMenu.adventureItems[MenuControl.Instance.adventureMenu.adventureItems.Count - 1]).MakeCardList();
                    
                    //generate discount
                    int randomCount = Random.Range(MenuControl.Instance.shopMenu. minDiscountCount, MenuControl.Instance.shopMenu.maxDiscountCount+1);
                    List<int> discountValues = new List<int>();
                    int i = 0;
                    for (; i < randomCount; i++)
                    {
                        discountValues.Add(1);
                    }

                    for (; i < cards.Count; i++)
                    {
                        discountValues.Add(0);
                    }
                    discountValues.Shuffle();
                    
                 for (int xx = 0; xx < cards.Count; xx += 1)
                 {
                     if (MenuControl.Instance.heroMenu.isCardUnlockedAndAvailable(cards[xx]))
                     {
                         MenuControl.Instance.adventureMenu.itemCards.Add(cards[xx]);
                         MenuControl.Instance.adventureMenu.addToItemCardsForItemIndex(MenuControl.Instance.adventureMenu.adventureItems.Count - 1,discountValues[xx]);
                     }
                 }

                  //  MenuControl.Instance.shopMenu.GenerateDiscounts();
                    //MenuControl.Instance.shopMenu.ShowShopPurchase(MenuControl.Instance.adventureMenu.GetItemCards(),MenuControl.Instance.adventureMenu.GetShop(). GetName());
                    break;
                case "驱灵人":
                    MenuControl.Instance.adventureMenu.adventureItemInfos.Add(new AdventureItemInfo());
                    //神殿
                    MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetMonastery());
                    
                    //MenuControl.Instance.shopMenu.ShowShopRemoval(MenuControl.Instance.adventureMenu.GetMonastery().GetName());
                    break;
                case "随机事件":
                    MenuControl.Instance.adventureMenu.adventureItemInfos.Add(new AdventureItemInfo());
                    MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetNextRandomEvent(allRandomEvents));
                    break;
                case "剧情事件":
                    MenuControl.Instance.adventureMenu.adventureItemInfos.Add(new AdventureItemInfo());
                    MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetStoryEvent(mapEvent));
                    break;
                case "离开":
                    MenuControl.Instance.adventureMenu.adventureItemInfos.Add(new AdventureItemInfo());
                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemStairwayNextFloor>());
                    break;
                case "离开冒险":
                    MenuControl.Instance.adventureMenu.adventureItemInfos.Add(new AdventureItemInfo());
                    MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemStairwayFinishGame>());
                    break;
                case "低级宝箱":
                    
                    MenuControl.Instance.adventureMenu.adventureItemInfos.Add(new AdventureItemInfo());
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemArtifactChest>());
                    MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetTreasure());
                    List<Card> cards2 = ((AdventureItemTreasure)MenuControl.Instance.adventureMenu.adventureItems[MenuControl.Instance.adventureMenu.adventureItems.Count - 1]).MakeCardList();
                 for (int xx = 0; xx < cards2.Count; xx += 1)
                 {
                     if (MenuControl.Instance.heroMenu.isCardUnlockedAndAvailable(cards2[xx]))
                     {
                         MenuControl.Instance.adventureMenu.itemCards.Add(cards2[xx]);
                         MenuControl.Instance.adventureMenu.addToItemCardsForItemIndex(MenuControl.Instance.adventureMenu
                             .adventureItems.Count - 1);
                     }
                     else
                     {
                         Debug.LogError("a not valid card is generated in 低级宝箱 "+cards2[xx].name);
                     }
                 }
                    break;
                case "高级宝箱":
                    MenuControl.Instance.adventureMenu.adventureItemInfos.Add(new AdventureItemInfo());
                    MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemArtifactChest>());
                    break;
                case "怪物":
                case "精英":
                case "BOSS":
                    var encounter =   GetRandomEncounter(mapEvent.enemyGroup);
                    encounter.PickRandomSettingValue();
                    MenuControl.Instance.adventureMenu.adventureItems.Add(encounter);
                    
                    MenuControl.Instance.adventureMenu.adventureItemInfos.Add( encounter.GenerateSpecialChallenge());
                    break;
                default:
                    Debug.LogError(mapEvent.eventType+" is not taking care as mapEvent.eventType");
                    break;
                
            }
            firstTile.init(mapEvent,infoID,MenuControl.Instance.adventureMenu.adventureItems.Count - 1);
            MenuControl.Instance.adventureMenu.addMapTile(firstTile);
            infoID++;
        }

        MenuControl.Instance.adventureMenu.SortChildrenByY();

    }

    private HashSet<string> generatedEnemyGroup = new HashSet<string>();
         public AdventureItemEncounter GetRandomEncounter(int enemyGroupID)
     {
         List<AdventureItemEncounter> items = new List<AdventureItemEncounter>();
         foreach (var item in MenuControl.Instance.adventureMenu.allAdventureItems)
         {
             if (!MenuControl.Instance.csvLoader.EnemyEncounterIdToGroupId.ContainsKey(item.UniqueID))
             {
                 continue;
                 //Debug.LogError(item.name+" not in EnemyEncounterIdToGroupId");
             }

             if (generatedEnemyGroup.Contains(item.UniqueID))
             {
                 continue;
             }
             if (item is AdventureItemEncounter encounter && MenuControl.Instance.csvLoader.EnemyEncounterIdToGroupId[item.UniqueID] == enemyGroupID)
             {
                 items.Add(encounter);
             }
         }

         if (items.Count == 0)
         {
             Debug.LogError("enemy group has to duplicate for "+enemyGroupID);
             foreach (var item in MenuControl.Instance.adventureMenu.allAdventureItems)
             {
                 if (!MenuControl.Instance.csvLoader.EnemyEncounterIdToGroupId.ContainsKey(item.UniqueID))
                 {
                     continue;
                     //Debug.LogError(item.name+" not in EnemyEncounterIdToGroupId");
                 }

                 if (item is AdventureItemEncounter encounter && MenuControl.Instance.csvLoader.EnemyEncounterIdToGroupId[item.UniqueID] == enemyGroupID)
                 {
                     items.Add(encounter);
                 }
             }
         }
         // foreach (AdventureItemEncounter item in area.enemies)
         // {
         //     if (item.level == level && item.extraIntFlag == typeInt && !MenuControl.Instance.adventureMenu.adventureItems.Contains(item))
         //         items.Add(item);
         // }
         if (items.Count == 0)
         {
             Debug.LogError("items.Count is 0");
         }

         var pickedItem = items[Random.Range(0, items.Count)];
         
         generatedEnemyGroup.Add(pickedItem.UniqueID);
         return pickedItem;
     }

    public virtual void GenerateTestItems()
    {

    }

    public virtual string GetItemsRemainingText()
    {
        int initialItemsCount = 0;
        int remainingCount = 0;
        for (int ii = 0; ii < MenuControl.Instance.adventureMenu.adventureItems.Count; ii += 1)
        {
            AdventureItem item = MenuControl.Instance.adventureMenu.adventureItems[ii];
            if (item is AdventureItemEncounter)
            {
                initialItemsCount += 1;

                if (!MenuControl.Instance.adventureMenu.adventureItemCompletions[ii])
                {
                    remainingCount += 1;
                }
            }
        }

        return remainingCount + " / " + initialItemsCount.ToString();
    }
}