using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceReplaceWithAdventureItem : EventChoice
{

    public AdventureItem itemToReplaceWith;

    public override void PerformChoice()
    {
        MenuControl.Instance.adventureMenu.adventureItems[MenuControl.Instance.adventureMenu.selectedIndex] = itemToReplaceWith;
        MenuControl.Instance.dataControl.SaveData();
        MenuControl.Instance.adventureMenu.RenderScreen();
        MenuControl.Instance.eventMenu.CloseMenu();
    }

}
