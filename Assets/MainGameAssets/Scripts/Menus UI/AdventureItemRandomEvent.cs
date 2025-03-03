using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureItemRandomEvent : AdventureItem {

    public List<int> validAreaInts = new List<int>();
    public EventDefinition eventDefinition;
    public List<Card> validWithTalents;
    public bool removeEventWhenOpen = false;
    public override void PerformItem(int index)
    {
        if (removeEventWhenOpen)
        {
            MenuControl.Instance.adventureMenu.FinishItem();
        }
        MenuControl.Instance.eventMenu.ShowEvent(eventDefinition);
    }

}
