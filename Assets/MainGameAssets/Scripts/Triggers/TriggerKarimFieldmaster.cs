using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKarimFieldmaster : Trigger
{
    public override void UnitChangedPower(Unit unit, Ability ability, int oldValue)
    {
        if (GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            int diff = unit.currentPower - oldValue;

            unit.currentPower = Mathf.Max(0, unit.currentPower + diff);

        }
    }
}
