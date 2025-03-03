using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeIntentCards : Ability
{
    public List<Card> originalCards;
    public List<Card> newCards;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        var player = sourceCard.player;
        if (player == null)
        {
            player = MenuControl.Instance.battleMenu.playerAI;
        }
        player.ChangeCardsInHand(originalCards, newCards);
        
        //MenuControl.Instance.battleMenu.playerAI.RenderIntent2ndHand();
    }
}
