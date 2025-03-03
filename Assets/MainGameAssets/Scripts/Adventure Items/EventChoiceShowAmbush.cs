using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceShowAmbush: EventChoice
{
    public AdventureItemEncounter encounter;

    public override void PerformChoice()
    {

        MenuControl.Instance.adventureMenu.adventureItems[MenuControl.Instance.adventureMenu.selectedIndex] = encounter;
        MenuControl.Instance.dataControl.SaveData();
        MenuControl.Instance.adventureMenu.RenderScreen();
        MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter().PerformItem(MenuControl.Instance.adventureMenu.selectedIndex);
    }
}
