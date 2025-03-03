using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSporeeater : Trigger
{
    public List<Card> sporeEaterTemplates = new List<Card>();
    public Ability createCardAbility;

    public override void MinionSummoned(Minion minion)
    {
        if (minion == GetCard())
        {
            int count = 0;
            foreach (Unit unit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
            {
                bool isSporeEater = false;
                foreach (Card card in sporeEaterTemplates)
                {
                    if (card.UniqueID == unit.cardTemplate.UniqueID)
                    {
                        isSporeEater = true;
                    }
                }
                if ((isSporeEater || unit.cardTemplate.UniqueID == GetCard().cardTemplate.UniqueID) && unit != GetCard())
                {
                    count += 1;
                }
            }

            if (count > 0)
            {
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    for (int ii = 0; ii < count; ii += 1)
                    {
                        createCardAbility.PerformAbility(GetCard(), GetCard().player.GetHero().GetTile());
                    }
                });

            }
        }
    }
}
