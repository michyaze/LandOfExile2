using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceTeleportTwoPaths : EventChoice
{
    public bool elitePath;

    public override void PerformChoice()
    {
        CloseEvent();

        MenuControl.Instance.adventureMenu.currentMapTileIndex = 0;
        MenuControl.Instance.adventureMenu.RenderScreen();
        MenuControl.Instance.eventMenu.CloseMenu();


        foreach (AdventureItem item in MenuControl.Instance.adventureMenu.adventureItems)
        {
            if (item is AdventureItemDoorwayClosedElite && elitePath)
            {
                MenuControl.Instance.adventureMenu.adventureItemCompletions[MenuControl.Instance.adventureMenu.adventureItems.IndexOf(item)] = true;

                MenuControl.Instance.ShowBlockingNotification(null, MenuControl.Instance.GetLocalizedString("BossDoorOpenedTitle"), MenuControl.Instance.GetLocalizedString("BossDoorOpenedPrompt"), () =>
                {
                    MenuControl.Instance.adventureMenu.RenderScreen();
                });
                break;
            }

            if (item is AdventureItemDoorwayClosedNormal && !elitePath)
            {
                MenuControl.Instance.adventureMenu.adventureItemCompletions[MenuControl.Instance.adventureMenu.adventureItems.IndexOf(item)] = true;

                MenuControl.Instance.ShowBlockingNotification(null, MenuControl.Instance.GetLocalizedString("BossDoorOpenedTitle"), MenuControl.Instance.GetLocalizedString("BossDoorOpenedPrompt"), () =>
                {
                    MenuControl.Instance.adventureMenu.RenderScreen();
                });
                break;
            }
        }

        MenuControl.Instance.dataControl.SaveData();
    }

}
