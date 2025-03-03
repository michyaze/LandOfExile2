// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class AreaGenerationArea1 : AreaGeneration
// {
//
//     public List<AdventureItemEncounter> mediumEncounters = new List<AdventureItemEncounter>();
//     public List<AdventureItemEncounter> mediumNextEncounters = new List<AdventureItemEncounter>();
//     public List<AdventureItemEncounter> mediumEliteEncounters = new List<AdventureItemEncounter>();
//
//     public Achievement hydraDefeatedAchievement;
//
//
//     enum BranchType
//     {
//         Short, MediumElite, Long, Boss
//     }
//
//     public override void GenerateItems()
//     {
//
//         MenuControl.Instance.ApplySeed();
//
//         //Create 1st branch, 2nd branch, 3rd branch then boss branch
//
//         List<MapTileDirection> directions = new List<MapTileDirection>();
//         directions.Add(MapTileDirection.Up);
//         directions.Add(MapTileDirection.Down);
//         directions.Add(MapTileDirection.Left);
//         directions.Add(MapTileDirection.Right);
//         directions.Shuffle();
//
//         List<BranchType> branchTypes = new List<BranchType>();
//         branchTypes.Add(BranchType.Short);
//         branchTypes.Add(BranchType.Long);
//         branchTypes.Add(BranchType.MediumElite);
//         branchTypes.Add(BranchType.Boss);
//
//         //Spawn Map Tiles
//         MenuControl.Instance.adventureMenu.mapTiles.Clear();
//
//         //Place first Tile
//         MapTile firstTile = Instantiate(MenuControl.Instance.adventureMenu.mapTilePrefab, MenuControl.Instance.adventureMenu.mapPanel);
//         firstTile.revealed = true;
//         MenuControl.Instance.adventureMenu.mapTiles.Add(firstTile);
//
//         List<int> encounterTypes = new List<int>();
//         encounterTypes.Add(0);
//         encounterTypes.Add(1);
//         encounterTypes.Shuffle();
//
//         for (int ii = 0; ii < 4; ii += 1)
//         {
//             MapTileDirection direction = directions[ii];
//             BranchType branchType = branchTypes[ii];
//             int encInt = -1; 
//             if (branchType != BranchType.Boss)
//             {
//                 encInt = encounterTypes[0];
//                 encounterTypes.RemoveAt(0);
//                 encounterTypes.Add(encInt);
//             }
//             CreateBranchInDirection(direction, branchType, encInt, ii);
//         }
//
//     }
//
//     void CreateBranchInDirection(MapTileDirection primaryDirection, BranchType branchType, int monsterTypeIndex, int spoke)
//     {
//
//         int baseLevel = 1;
//
//         //Deal with possibilities in each zone 0, 1, 2, then 3
//
//         //Zone 0
//         List<MapTile> zoneTiles = new List<MapTile>();
//         //Place first Tile
//         MapTile firstTile = MenuControl.Instance.adventureMenu.MakeTileInDirection(MenuControl.Instance.adventureMenu.mapTiles[0], primaryDirection);
//         MenuControl.Instance.adventureMenu.mapTiles.Add(firstTile);
//         zoneTiles.Add(firstTile);
//
//         if (branchType == BranchType.Boss)
//         {
//             MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetBossDoorway());
//             firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//             firstTile.revealed = true;
//         }
//         if (branchType == BranchType.MediumElite )
//         {
//             MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemDoorwayClosedElite>());
//             firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//             firstTile.revealed = true;
//         }
//         if ( branchType == BranchType.Long)
//         {
//             MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemDoorwayClosedNormal>());
//             firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//             firstTile.revealed = true;
//         }
//
//         CreateNextZone(1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//
//     }
//
//     void CreateNextZone(int zoneIndex, MapTile hallwayTile, MapTileDirection primaryDirection, BranchType branchType, int monsterTypeIndex, int baseLevel, int spoke)
//     {
//
//         List<MapTile> zoneTiles = new List<MapTile>();
//         //Create Hallway to Zone 1
//         MapTile firstTile = MenuControl.Instance.adventureMenu.MakeTileInDirection(hallwayTile, primaryDirection);
//         MenuControl.Instance.adventureMenu.mapTiles.Add(firstTile);
//         zoneTiles.Add(firstTile);
//
//         if (branchType == BranchType.Boss)
//         {
//             if (zoneIndex == 1)
//             {
//                
//                 CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//             }
//             else if (zoneIndex == 2)
//             {
//                 //Random Event
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetNextRandomEvent());
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//             }
//             else if (zoneIndex == 3)
//             {
//                 if (MenuControl.Instance.heroMenu.ascensionMode >= 1)
//                 {
//                     MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemFloor1StrangeKey>());
//                     firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//                 }
//
//                 CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//             }
//             else if (zoneIndex == 4)
//             {
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(area.GetRandomBoss());
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//                 CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//             }
//             else if (zoneIndex == 5)
//             {
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemStairwayNextFloor>());
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//             }
//
//         }
//         else if (branchType == BranchType.Short)
//         {
//             //Create two rooms to either side
//             MapTile tileA = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileA);
//             zoneTiles.Add(tileA);
//
//             MapTile tileB = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetOppositeDirection(MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection)));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileB);
//             zoneTiles.Add(tileB);
//
//
//             if (zoneIndex == 1)
//             {
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(baseLevel, monsterTypeIndex));
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetBlacksmith());
//                 tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(baseLevel, monsterTypeIndex));
//                 tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//             }
//             if (zoneIndex == 2)
//             {
//
//                 //Random Event
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetNextRandomEvent());
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 //if (!hydraDefeatedAchievement.IsCompleted())
//                 //{
//                 //    MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemAnkhOfTime>());
//                 //    tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//                 //}
//
//                 if (MenuControl.Instance.heroMenu.reaperMode)
//                 {
//                     MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemPurchaseCardsReaper>());
//                     tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//                 }
//
//                 if (MenuControl.Instance.heroMenu.seasonsMode)
//                 {
//                     MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemByID("EventSantaKrumpusOrNothing"));
//                     tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//                 }
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(baseLevel, monsterTypeIndex));
//                 tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//             }
//             if (zoneIndex == 3)
//             {
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(baseLevel + 1, monsterTypeIndex));
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetMonastery());
//                 tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 //Teleport
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemTeleportTwoPaths>());
//                 tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//             }
//         }
//         else if (branchType == BranchType.Long)
//         {
//             //Create two rooms to either side
//             MapTile tileA = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileA);
//             zoneTiles.Add(tileA);
//
//             MapTile tileB = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetOppositeDirection(MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection)));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileB);
//             zoneTiles.Add(tileB);
//
//            
//             if (zoneIndex == 1)
//             {
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(baseLevel + 1, monsterTypeIndex));
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(baseLevel + 1, monsterTypeIndex));
//                 tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(baseLevel + 1, monsterTypeIndex));
//                 tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//             }
//             if (zoneIndex == 2)
//             {
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomMediumNextEncounter());
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//
//             }
//             if (zoneIndex == 3)
//             {
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomMediumNextEncounter());
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 //Treasure
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetTreasure());
//                 tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//                 List<Card> cards = ((AdventureItemTreasure)MenuControl.Instance.adventureMenu.adventureItems[MenuControl.Instance.adventureMenu.adventureItems.Count - 1]).MakeCardList();
//                 for (int xx = 0; xx < cards.Count; xx += 1)
//                 {
//                     MenuControl.Instance.adventureMenu.itemCards.Add(cards[xx]);
//                     MenuControl.Instance.adventureMenu.itemCardsForItemIndex.Add(MenuControl.Instance.adventureMenu.adventureItems.Count - 1);
//                 }
//
//                 //Random Event
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetNextRandomEvent());
//                 tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//             }
//             if (zoneIndex == 4)
//             {
//                 //Random Event
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetNextRandomEvent());
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 //Shop
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetShop());
//                 tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//                 List<Card> cards = ((AdventureItemPurchaseCards)MenuControl.Instance.adventureMenu.adventureItems[MenuControl.Instance.adventureMenu.adventureItems.Count - 1]).MakeCardList();
//                 for (int xx = 0; xx < cards.Count; xx += 1)
//                 {
//                     MenuControl.Instance.adventureMenu.itemCards.Add(cards[xx]);
//                     MenuControl.Instance.adventureMenu.itemCardsForItemIndex.Add(MenuControl.Instance.adventureMenu.adventureItems.Count - 1);
//                 }
//
//                 //Teleport
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemTeleport>());
//                 tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//
//             }
//         }
//         else if (branchType == BranchType.MediumElite)
//         {
//             //Create two rooms to either side
//             MapTile tileA = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileA);
//             zoneTiles.Add(tileA);
//
//             MapTile tileB = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetOppositeDirection(MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection)));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileB);
//             zoneTiles.Add(tileB);
//
//
//             if (zoneIndex == 1)
//             {
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomMediumEncounter());
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(baseLevel + 1, monsterTypeIndex));
//                 tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomMediumEncounter());
//                 tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//             }
//             if (zoneIndex == 2)
//             {
//
//                 //Random Event
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetNextRandomEvent());
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 if (Random.Range(0, 2) == 0)
//                 {
//                     MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(baseLevel + 1, monsterTypeIndex));
//                     tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//                 }
//
//                 CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//             }
//             if (zoneIndex == 3)
//             {
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomMediumNextEncounter());
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 if (Random.Range(0, 2) == 0)
//                 {
//                     MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomMediumNextEncounter());
//                     tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//                 }
//
//                 //Random Event
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetNextRandomEvent());
//                 tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//
//             }
//             if (zoneIndex == 4)
//             {
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(mediumEliteEncounters[Random.Range(0, mediumEliteEncounters.Count)]);
//                 firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 //Shrine Healing
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetShrine());
//                 tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 //Artifact Chest
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemArtifactChest>());
//                 tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//                 CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection, branchType, monsterTypeIndex, baseLevel, spoke);
//
//             }
//             if (zoneIndex == 5)
//             {
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemTeleport>());
//                 tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//             }
//
//         }
//
//     }
//
//     public AdventureItemEncounter GetRandomEncounter(int level, int typeInt)
//     {
//         List<AdventureItemEncounter> items = new List<AdventureItemEncounter>();
//         foreach (AdventureItemEncounter item in area.enemies)
//         {
//             if (item.level == level && item.extraIntFlag == typeInt && !MenuControl.Instance.adventureMenu.adventureItems.Contains(item))
//                 items.Add(item);
//         }
//
//         return items[Random.Range(0, items.Count)];
//     }
//
//     public AdventureItemEncounter GetRandomMediumEncounter()
//     {
//         List<AdventureItemEncounter> items = new List<AdventureItemEncounter>();
//         foreach (AdventureItemEncounter item in mediumEncounters)
//         {
//             if (!MenuControl.Instance.adventureMenu.adventureItems.Contains(item))
//                 items.Add(item);
//         }
//
//         return items[Random.Range(0, items.Count)];
//
//     }
//
//     public AdventureItemEncounter GetRandomMediumNextEncounter()
//     {
//         List<AdventureItemEncounter> items = new List<AdventureItemEncounter>();
//         foreach (AdventureItemEncounter item in mediumNextEncounters)
//         {
//             if (!MenuControl.Instance.adventureMenu.adventureItems.Contains(item))
//                 items.Add(item);
//         }
//
//         return items[Random.Range(0, items.Count)];
//
//     }
//
//     public override void GenerateTestItems()
//     {
//         //Place first Tile
//         MapTile firstTile = Instantiate(MenuControl.Instance.adventureMenu.mapTilePrefab, MenuControl.Instance.adventureMenu.mapPanel);
//         firstTile.revealed = true;
//         MenuControl.Instance.adventureMenu.mapTiles.Add(firstTile);
//
//         //Create two rooms to either side
//         List<MapTileDirection> directions = new List<MapTileDirection>();
//         directions.Add(MapTileDirection.Up);
//         directions.Add(MapTileDirection.Down);
//         directions.Add(MapTileDirection.Left);
//         directions.Add(MapTileDirection.Right);
//
//         List<AdventureItemEncounter> allEncounters = new List<AdventureItemEncounter>();
//         allEncounters.AddRange(area.enemies);
//         allEncounters.AddRange(mediumEncounters);
//         allEncounters.AddRange(mediumNextEncounters);
//         allEncounters.AddRange(mediumEliteEncounters);
//         allEncounters.AddRange(area.bosses);
//
//         foreach (MapTileDirection direction in directions)
//         {
//             MapTile lastTile = firstTile;
//             for (int ii = 0; ii < 5; ii += 1)
//             {
//                 lastTile = MenuControl.Instance.adventureMenu.MakeTileInDirection(lastTile, direction);
//                 MenuControl.Instance.adventureMenu.mapTiles.Add(lastTile);
//
//                 if (ii >= 2)
//                 {
//                     //Create two rooms to either side
//                     MapTile tileA = MenuControl.Instance.adventureMenu.MakeTileInDirection(lastTile, MenuControl.Instance.adventureMenu.GetNextDirection(direction));
//                     MenuControl.Instance.adventureMenu.mapTiles.Add(tileA);
//
//
//                     MapTile tileB = MenuControl.Instance.adventureMenu.MakeTileInDirection(lastTile, MenuControl.Instance.adventureMenu.GetOppositeDirection(MenuControl.Instance.adventureMenu.GetNextDirection(direction)));
//                     MenuControl.Instance.adventureMenu.mapTiles.Add(tileB);
//
//                 }
//             }
//         }
//
//
//         MenuControl.Instance.adventureMenu.adventureItems.AddRange(allEncounters);
//         for (int ii = 0; ii < allEncounters.Count; ii += 1) {
//
//             MapTile lastTile = MenuControl.Instance.adventureMenu.mapTiles[ii+1];
//             lastTile.adventureItemIndex = ii;
//
//         }
//     
//     }
//
//     public override string GetItemsRemainingText()
//     {
//         int initialItemsCount = 0;
//         int remainingCount = 0;
//
//
//
//         for (int ii = 0; ii < MenuControl.Instance.adventureMenu.adventureItems.Count; ii += 1)
//         {
//             AdventureItem item = MenuControl.Instance.adventureMenu.adventureItems[ii];
//             if (item is AdventureItemEncounter)
//             {
//                 initialItemsCount += 1;
//
//                 if (!MenuControl.Instance.adventureMenu.adventureItemCompletions[ii])
//                 {
//                     remainingCount += 1;
//                 }
//             }
//
//             if (item is AdventureItemDoorwayClosedElite)
//             {
//                 if (!MenuControl.Instance.adventureMenu.adventureItemCompletions[ii])
//                 {
//                     remainingCount -= 6;
//                     initialItemsCount -= 6;
//                 }
//             }
//
//             if (item is AdventureItemDoorwayClosedNormal)
//             {
//                 if (!MenuControl.Instance.adventureMenu.adventureItemCompletions[ii])
//                 {
//                     remainingCount -= 5;
//                     initialItemsCount -= 5;
//                 }
//             }
//         }
//
//         
//
//         return remainingCount + " / " + initialItemsCount.ToString();
//     }
// }
//
//
