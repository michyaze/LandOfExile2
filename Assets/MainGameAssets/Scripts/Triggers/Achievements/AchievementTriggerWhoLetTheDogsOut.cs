using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerWhoLetTheDogsOut : TriggerAchievement
{
    public CardTag houndTag;

    public override void MinionSummoned(Minion minion)
    {
        CheckAchievement(minion.player);
    }

    private void CheckAchievement(Player player)
    {
        if (player == MenuControl.Instance.battleMenu.player1)
        {
            int count = 0;
            foreach (Minion boardMinion in player.GetMinionsOnBoard())
            {
                if (boardMinion.cardTags.Contains(houndTag))
                {
                    count += 1;
                }
            }

            if (count >= 5)
            {
                MarkAchievementCompleted();
            }
        }
    }

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        CheckAchievement(card.player);
    }

    public override void TurnEnded(Player player)
    {
        CheckAchievement(player);
    }
}
