// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class AreaGenerationArea4 : AreaGeneration
// {
//     public override void GenerateItems()
//     {
//         MenuControl.Instance.ApplySeed();
//
//         List<MapTileDirection> directions = new List<MapTileDirection>();
//         directions.Add(MapTileDirection.Up);
//         directions.Add(MapTileDirection.Down);
//         directions.Shuffle();
//
//         //Spawn Map Tiles
//         MenuControl.Instance.adventureMenu.mapTiles.Clear();
//
//         //Place first Tile
//         MapTile firstTile = Instantiate(MenuControl.Instance.adventureMenu.mapTilePrefab,
//             MenuControl.Instance.adventureMenu.mapPanel);
//         firstTile.revealed = true;
//         MenuControl.Instance.adventureMenu.mapTiles.Add(firstTile);
//
//         CreateBranchInDirection(directions[0]);
//     }
//
//     void CreateBranchInDirection(MapTileDirection primaryDirection)
//     {
//         //Deal with possibilities in each zone 0, 1, 2, then 3
//
//         //Zone 0
//         List<MapTile> zoneTiles = new List<MapTile>();
//         //Place first Tile
//         MapTile firstTile =
//             MenuControl.Instance.adventureMenu.MakeTileInDirection(MenuControl.Instance.adventureMenu.mapTiles[0],
//                 primaryDirection);
//         MenuControl.Instance.adventureMenu.mapTiles.Add(firstTile);
//         zoneTiles.Add(firstTile);
//
//         MenuControl.Instance.adventureMenu.adventureItems.Add(MenuControl.Instance.adventureMenu
//             .GetItemOfType<AdventureItemFloor4BlockedDoor>());
//         firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//         CreateNextZone(0, zoneTiles[Random.Range(0, zoneTiles.Count)], primaryDirection);
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
//         //Create extra rooms to either side
//         MapTile tileA = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile,
//             MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//         MenuControl.Instance.adventureMenu.mapTiles.Add(tileA);
//         zoneTiles.Add(tileA);
//
//         MapTile tileB = MenuControl.Instance.adventureMenu.MakeTileInDirection(firstTile,
//             MenuControl.Instance.adventureMenu.GetOppositeDirection(
//                 MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection)));
//         MenuControl.Instance.adventureMenu.mapTiles.Add(tileB);
//         zoneTiles.Add(tileB);
//
//         MapTile tileC = MenuControl.Instance.adventureMenu.MakeTileInDirection(tileA,
//             MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//         MenuControl.Instance.adventureMenu.mapTiles.Add(tileC);
//
//         MapTile tileD = MenuControl.Instance.adventureMenu.MakeTileInDirection(tileC,
//             MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//         MenuControl.Instance.adventureMenu.mapTiles.Add(tileD);
//
//         MapTile tileE = MenuControl.Instance.adventureMenu.MakeTileInDirection(tileD,
//             MenuControl.Instance.adventureMenu.GetNextDirection(primaryDirection));
//         MenuControl.Instance.adventureMenu.mapTiles.Add(tileE);
//
//         if (zoneIndex == 0)
//         {
//             MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(10));
//             firstTile.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(GetRandomEncounter(10));
//             tileA.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//
//             MenuControl.Instance.adventureMenu.adventureItems.Add(area.bosses[0]);
//             tileE.adventureItemIndex = MenuControl.Instance.adventureMenu.adventureItems.Count - 1;
//         }
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
//
//     public override void GenerateTestItems()
//     {
//         //Place first Tile
//         MapTile firstTile = Instantiate(MenuControl.Instance.adventureMenu.mapTilePrefab,
//             MenuControl.Instance.adventureMenu.mapPanel);
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
//                     MapTile tileA = MenuControl.Instance.adventureMenu.MakeTileInDirection(lastTile,
//                         MenuControl.Instance.adventureMenu.GetNextDirection(direction));
//                     MenuControl.Instance.adventureMenu.mapTiles.Add(tileA);
//
//
//                     MapTile tileB = MenuControl.Instance.adventureMenu.MakeTileInDirection(lastTile,
//                         MenuControl.Instance.adventureMenu.GetOppositeDirection(
//                             MenuControl.Instance.adventureMenu.GetNextDirection(direction)));
//                     MenuControl.Instance.adventureMenu.mapTiles.Add(tileB);
//                 }
//             }
//         }
//
//
//         MenuControl.Instance.adventureMenu.adventureItems.AddRange(allEncounters);
//         for (int ii = 0; ii < allEncounters.Count; ii += 1)
//         {
//             MapTile lastTile = MenuControl.Instance.adventureMenu.mapTiles[ii + 1];
//             lastTile.adventureItemIndex = ii;
//         }
//     }
// }