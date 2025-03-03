using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBleedingAdventurer : Trigger
{
    public override void GameEnded(bool victory)
    {
        if (victory && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            Card randomTreasure = MenuControl.Instance.heroMenu.GetAllUnlockedTreasures()[Random.Range(0, MenuControl.Instance.heroMenu.GetAllUnlockedTreasures().Count)];

            MenuControl.Instance.heroMenu.AddCardToDeck(randomTreasure);
        }
    }
}
