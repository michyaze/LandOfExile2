using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CheckOnBoardCardType  { OpponentOnly,PlayerOnly, All};
public class FlameRegeneration : Ability
{

    public Effect burnEffectTemplate;
    public CheckOnBoardCardType onBoardCardType;
    
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int totalHeal = 0;
        var cardOnBoard = new List<Card>();
        switch (onBoardCardType)
        {
            case CheckOnBoardCardType.PlayerOnly:
                cardOnBoard.AddRange(GetCard().player.cardsOnBoard);
                break;
            case CheckOnBoardCardType.OpponentOnly:
                cardOnBoard.AddRange(GetCard().player.GetOpponent().cardsOnBoard);
                break;
            case CheckOnBoardCardType.All:
                cardOnBoard.AddRange(GetCard().player.cardsOnBoard);
                cardOnBoard.AddRange(GetCard().player.GetOpponent().cardsOnBoard);
                break;
        }
        foreach (Unit unit in cardOnBoard)
        {
            if (unit.GetEffectsWithTemplate(burnEffectTemplate).Count > 0)
            {
                totalHeal += unit.GetEffectsWithTemplate(burnEffectTemplate)[0].remainingCharges;
            }
        }

        GetCard().player.GetHero().Heal(GetCard(), this, totalHeal);
    }
}
