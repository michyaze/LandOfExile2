using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Card
{
    public Ability abilityToPerform;
    public int maxPicksAllowed;
    //public bool requiresChoice;

    public int GetMaximumPickAllowed()
    {
        if (MenuControl.Instance.heroMenu.ascensionMode >= 11)
        {
            if (UniqueID == "WarriorBasic18") return -1;
        }
        return maxPicksAllowed;
    }
}
