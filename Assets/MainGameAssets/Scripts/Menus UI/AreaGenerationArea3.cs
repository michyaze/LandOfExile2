// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class AreaGenerationArea3 : AreaGeneration
// {
//     public List<AdventureItemEncounter> elites = new List<AdventureItemEncounter>();
//     public List<AdventureItemEncounter> hydraBattles = new List<AdventureItemEncounter>();
//     public Achievement hydraDefeatedAchievement;
//
//     public override void GenerateItems()
//     {
//
//         MenuControl.Instance.ApplySeed();
//
//
//
//         List<MapTileDirection> directions = new List<MapTileDirection>();
//         directions.Add(MapTileDirection.Left);
//         directions.Add(MapTileDirection.Right);
//         directions.Shuffle();
//
//         //Spawn Map Tiles
//         MenuControl.Instance.adventureMenu.mapTiles.Clear();
//
//         //Place first Tile
//         MapTile firstTile = Instantiate(MenuControl.Instance.adventureMenu.mapTilePrefab, MenuControl.Instance.adventureMenu.mapPanel);
//         firstTile.revealed = true;
//         MenuControl.Instance.adventureMenu.mapTiles.Add(firstTile);
//
//         CreateBranchInDirection(directions[0]);
//     }
//
//     void CreateBranchInDirection(MapTileDirection primaryDirection)
//     {
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
//         CreateNextZone(0, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection);
//
//     }
//
//     void CreateNextZone(int zoneIndex, MapTile hallwayTile, MapTileDirection primaryDirection)
//     {
//         List<MapTile> zoneTiles = new List<MapTile>();
//         //Create Hallway to Zone 1
//         MapTile firstTile = MenuControl.Instance.adventureMenu.MakeTileInDirection(hallwayTile, primaryDirection);
//         MenuControl.Instance.adventureMenu.mapTiles.Add(firstTile);
//         zoneTiles.Add(firstTile);
//
//         if (zoneIndex == 0)
//         {
//             //Create extra rooms to either side
//             MapTile tileA = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileA);
//             zoneTiles.Add(tileA);
//
//             MapTile tileB = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetOppositeDirection(MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection)));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileB);
//             zoneTiles.Add(tileB);
//
//             MapTile tileC = MenuControl.Instance.adventureMenu.MakeTileInDirection(tileA, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileC);
//
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(6));
//             firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(6));
//             tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(6));
//             tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(6));
//             tileC.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection);
//         }
//         else if (zoneIndex == 1)
//         {
//
//             //Create extra rooms to either side
//             MapTile tileA = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileA);
//             zoneTiles.Add(tileA);
//
//             MapTile tileB = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetOppositeDirection(MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection)));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileB);
//             zoneTiles.Add(tileB);
//
//             MapTile tileC = MenuControl.Instance.adventureMenu.MakeTileInDirection(tileA, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileC);
//
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemHealingShrine>());
//             firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemUpgradeCards>());
//             tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetNextRandomEvent());
//             tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemPurchaseCards>());
//             tileC.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//             List<Card> cards = ((AdventureItemPurchaseCards)MenuControl.Instance.adventureMenu.adventureItems[MenuControl.Instance.adventureMenu.adventureItems.Count - 1]).MakeCardList();
//             for (int xx = 0; xx < cards.Count; xx += 1)
//             {
//                 MenuControl.Instance.adventureMenu.itemCards.Add(cards[xx]);
//                 MenuControl.Instance.adventureMenu.itemCardsForItemIndex.Add(MenuControl.Instance.adventureMenu.adventureItems.Count - 1);
//             }
//
//             CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection);
//         }
//         else if (zoneIndex == 2)
//         {
//             if (!hydraDefeatedAchievement.IsCompleted())
//             {
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(hydraBattles[0]);
//             }
//             else
//             {
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(6));
//             }
//             firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             if (MenuControl.Instance.heroMenu.reaperMode)
//             {
//                 MapTile tileA = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//                 MenuControl.Instance.adventureMenu.mapTiles.Add(tileA);
//                 zoneTiles.Add(tileA);
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemPurchaseCardsReaper>());
//                 tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//             }
//             if (MenuControl.Instance.heroMenu.seasonsMode)
//             {
//                 MapTile tileA = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//                 MenuControl.Instance.adventureMenu.mapTiles.Add(tileA);
//                 zoneTiles.Add(tileA);
//
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemByID("EventSantaKrumpusOrNothing"));
//                 tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//             }
//
//             CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection);
//         }
//
//
//         else if (zoneIndex == 3)
//         {
//             //Create extra rooms to either side
//             MapTile tileA = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileA);
//             zoneTiles.Add(tileA);
//
//             MapTile tileB = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetOppositeDirection(MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection)));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileB);
//             zoneTiles.Add(tileB);
//
//             MapTile tileC = MenuControl.Instance.adventureMenu.MakeTileInDirection(tileA, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileC);
//
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(7));
//             firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(7));
//             tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(7));
//             tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEliteEncounter());
//             tileC.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection);
//         }
//         else if (zoneIndex == 4)
//         {
//             //Create extra rooms to either side
//             MapTile tileA = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileA);
//             zoneTiles.Add(tileA);
//
//             MapTile tileB = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetOppositeDirection(MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection)));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileB);
//             zoneTiles.Add(tileB);
//
//             MapTile tileC = MenuControl.Instance.adventureMenu.MakeTileInDirection(tileA, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileC);
//
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemRemoveCards>());
//             firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetNextRandomEvent());
//             tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetNextRandomEvent());
//             tileB.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemTreasure>());
//             tileC.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//             List<Card> cards = ((AdventureItemTreasure)MenuControl.Instance.adventureMenu.adventureItems[MenuControl.Instance.adventureMenu.adventureItems.Count - 1]).MakeCardList();
//             for (int xx = 0; xx < cards.Count; xx += 1)
//             {
//                 MenuControl.Instance.adventureMenu.itemCards.Add(cards[xx]);
//                 MenuControl.Instance.adventureMenu.itemCardsForItemIndex.Add(MenuControl.Instance.adventureMenu.adventureItems.Count - 1);
//             }
//
//             CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection);
//
//         }
//         else if (zoneIndex == 5)
//         {
//             if (!hydraDefeatedAchievement.IsCompleted())
//             {
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(hydraBattles[1]);
//             }
//             else
//             {
//                 MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(7));
//             }
//             firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             CreateNextZone(zoneIndex + 1, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection);
//         }
//         else if (zoneIndex == 6)
//         {
//
//             //MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemUnderConstruction>());
//             //firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             //Create extra rooms to either side
//             MapTile tileA = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileA);
//             zoneTiles.Add(tileA);
//
//             MapTile tileB = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile, MenuControl.Instance.adventureMenu.GetOppositeDirection(MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection)));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileB);
//             zoneTiles.Add(tileB);
//
//             MapTile tileC = MenuControl.Instance.adventureMenu.MakeTileInDirection(tileA, MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//             MenuControl.Instance.adventureMenu.mapTiles.Add(tileC);
//
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(area.bosses[0]);
//             tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemStairwayNextFloor>());
//             tileC.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//         }
//
//     }
//
//     public AdventureItemEncounter GetRandomEncounter(int level)
//     {
//         List<AdventureItemEncounter> items = new List<AdventureItemEncounter>();
//         foreach (AdventureItemEncounter item in area.enemies)
//         {
//             if (item.level == level && !MenuControl.Instance.adventureMenu.adventureItems.Contains(item))
//                 items.Add(item);
//         }
//
//         return items[Random.Range(0, items.Count)];
//     }
//
//     public AdventureItemEncounter GetRandomEliteEncounter()
//     {
//         List<AdventureItemEncounter> items = new List<AdventureItemEncounter>();
//         foreach (AdventureItemEncounter item in elites)
//         {
//             if (!MenuControl.Instance.adventureMenu.adventureItems.Contains(item))
//                 items.Add(item);
//         }
//
//         return items[Random.Range(0, items.Count)];
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
//         for (int ii = 0; ii < allEncounters.Count; ii += 1)
//         {
//
//             MapTile lastTile = MenuControl.Instance.adventureMenu.mapTiles[ii + 1];
//             lastTile.adventureItemIndex = ii;
//
//         }
//
//     }
// }
//
//
