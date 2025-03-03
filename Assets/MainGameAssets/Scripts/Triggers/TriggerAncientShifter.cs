using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAncientShifter : Trigger
{
    public bool destroyEndOfTurn;

    public override void MinionSummoned(Minion minion)
    {
        if (minion == GetCard())
        {
            destroyEndOfTurn = false;
        }
    }

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card.UniqueID == "Green01" && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                Unit thisUnit = (Unit)GetCard();

                thisUnit.ChangePower(this, GetCard().player.GetHero().GetPower());
                thisUnit.ChangeCurrentHP(this, GetCard().player.GetHero().GetHP());
                thisUnit.ChangeMaxHP(this, GetCard().player.GetHero().GetHP());
                thisUnit.ChangeActions(this, GetCard().player.GetHero().remainingActions);
                thisUnit.ChangeActions(this, GetCard().player.GetHero().remainingMoves);

                destroyEndOfTurn = true;

            });
        }
    }

    public override void TurnEnded(Player player)
    {
        if (player == GetCard().player && destroyEndOfTurn)
        {
            destroyEndOfTurn = false;
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                ((Unit)GetCard()).SufferDamage(GetCard(), this, 0, true);

            });
        }
    }

}
