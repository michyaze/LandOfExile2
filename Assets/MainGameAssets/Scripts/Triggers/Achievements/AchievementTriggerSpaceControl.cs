using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerSpaceControl : TriggerAchievement
{

    public override void MinionSummoned(Minion minion)
    {
        if (minion.player == MenuControl.Instance.battleMenu.player1)
        {

            foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
            {
                if (tile.GetUnit() == null) return;
                if (tile.GetUnit() != MenuControl.Instance.battleMenu.playerAI.GetHero() && tile.GetUnit().player != MenuControl.Instance.battleMenu.player1) return;
            }
            MarkAchievementCompleted();
        }
    }
}
