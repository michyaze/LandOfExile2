using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatableAbility : Ability
{
    public int timesToRepeat;
    public virtual void RepeatAbility()
    {

        timesToRepeat -= 1;
        if (timesToRepeat > 0)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                RepeatAbility();
            });
        }
    }
}
