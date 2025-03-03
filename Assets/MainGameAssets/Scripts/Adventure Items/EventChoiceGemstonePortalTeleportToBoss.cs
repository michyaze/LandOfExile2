using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this choice and event is deprectated
public class EventChoiceGemstonePortalTeleportToBoss : EventChoice
{
    public override void PerformChoice()
    {
        CloseEvent();

        // for (int ii = 0; ii < MenuControl.Instance.adventureMenu.adventureItems.Count; ii += 1)
        // {
        //     if (MenuControl.Instance.adventureMenu.adventureItems[ii] is AdventureItemEncounter && ((AdventureItemEncounter)MenuControl.Instance.adventureMenu.adventureItems[ii]).isBoss)
        //     {
        //         foreach (MapTile mapTile in MenuControl.Instance.adventureMenu.mapTiles)
        //         {
        //             if (mapTile.adventureItemIndex == ii)
        //             {
        //
        //                 mapTile.revealed = true;
        //                 MenuControl.Instance.adventureMenu.currentMapTileIndex = MenuControl.Instance.adventureMenu.mapTiles.IndexOf(mapTile);
        //                 MenuControl.Instance.dataControl.SaveData();
        //                 MenuControl.Instance.adventureMenu.RenderScreen();
        //                 return;
        //             }
        //         }
        //
        //     }
        // }
    }
}
