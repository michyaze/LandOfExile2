using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLesserChaos : Trigger
{

    public Effect guardianTemplate;
    public Effect retaliateTemplate;
    public Effect forkStrikeTemplate;
    public TargetValidator rangeFourValidator;

    public override void MinionSummoned(Minion minion)
    {
        if (minion == GetCard())
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                //Warcry: Randomly gain one of: Guardian, Retaliate, Fork Strike, Range 4, +4 Power. 
                Unit unit = ((Unit)GetCard());
                int randomInt = Random.Range(0, 5);
                if (randomInt == 0)
                {
                    unit.ApplyEffect(GetCard(), this, guardianTemplate, 1);
                }
                else if (randomInt == 1)
                {
                    unit.ApplyEffect(GetCard(), this, retaliateTemplate, 1);
                }
                else if (randomInt == 2)
                {
                    unit.ApplyEffect(GetCard(), this, forkStrikeTemplate, 1);
                }
                else if (randomInt == 3)
                {
                    unit.activatedAbility.targetValidator = rangeFourValidator;
                }
                else
                {
                    unit.ChangePower(this, unit.currentPower + 4);
                }

            });

        }
    }
}
