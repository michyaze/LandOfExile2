using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerConciousness : Trigger
{
    public Minion gestaltTemplate;

    public override void TurnStarted(Player player)
    {
        if (player == GetCard().player)
        {
            int charges = GetEffect().remainingCharges;
            Unit unit = (Unit)GetCard();

            unit.ChangePower(this, unit.currentPower + charges);
            unit.ChangeCurrentHP(this, unit.currentHP + charges);
            unit.ChangeMaxHP(this, unit.GetInitialHP() + charges);

        }
    }

    public override void TurnEnded(Player player)
    {
        Minion gestalt = GetGestalt();
        if (gestalt != null && gestalt.player != GetCard().player)
        {
            int opponentsCharges = 0;
            if (GetCard().player.GetOpponent().GetHero().GetEffectsWithTemplate(GetEffect().originalTemplate).Count > 0)
            {
                opponentsCharges = GetCard().player.GetOpponent().GetHero().GetEffectsWithTemplate(GetEffect().originalTemplate)[0].remainingCharges;
            }
            if (GetEffect().remainingCharges > opponentsCharges)
            {

                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {

                    gestalt.player.cardsOnBoard.Remove(gestalt);
                    gestalt.player = gestalt.player.GetOpponent();
                    gestalt.player.cardsOnBoard.Add(gestalt);

                });

            }
        }

    }

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion == GetGestalt())
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                ((Hero)GetCard()).RemoveEffect(sourceCard, this, GetEffect());
            });
        }
    }

    public Minion GetGestalt()
    {
        foreach (Card card in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (card.cardTemplate.UniqueID == gestaltTemplate.UniqueID)
            {
                return (Minion)card;
            }
        }
        return null;
    }

}

