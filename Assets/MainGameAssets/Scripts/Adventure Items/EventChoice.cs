using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EventChoice : CollectibleItem
{
    public List<Card> validWithTalents;
    public Ability performAnotherAbility;
    public bool removeEventWhenOpen = false;

    public virtual void BeforePerformChoice()
    {
        if (removeEventWhenOpen)
        {
            MenuControl.Instance.adventureMenu.FinishItem();
        }
    }
    public virtual void PerformChoice()
    {

    }

    public virtual bool IsVisible()
    {
        if (validWithTalents.Count != 0)
        {
            bool skillSatisfied = false;
            foreach (var skillNeed in validWithTalents)
            {
                foreach (var talentAcquired in  MenuControl.Instance.levelUpMenu.variableTalentsAcquired)
                {
                    if (skillNeed.UniqueID == talentAcquired.UniqueID)
                    {
                        skillSatisfied = true;
                    }
                }
            }

            if (!skillSatisfied)
            {
                return false;
            }
        }

        return true;
    }

    public void CloseEvent()
    {
        MenuControl.Instance.adventureMenu.RemoveItem();
        MenuControl.Instance.eventMenu.CloseMenu();

    }
}
