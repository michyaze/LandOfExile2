using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDefinitionEncounter : EventDefinition 
{

    public override string GetName()
    {
        return ((AdventureItemEncounter)MenuControl.Instance.adventureMenu.GetCurrentAdventureItem()).GetName();
    }
    public override string GetDescription()
    {
        return ((AdventureItemEncounter)MenuControl.Instance.adventureMenu.GetCurrentAdventureItem()).GetDescription();
    }



}
