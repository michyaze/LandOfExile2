using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAccomplice : Trigger
{
    List<Unit> attackers = new List<Unit>();
    List<Unit> defenders = new List<Unit>();
    public Effect markedEffectTemplate;

    public override void TurnEnded(Player player)
    {
        attackers.Clear();
        defenders.Clear();
    }

    public override void TurnStarted(Player player)
    {
        attackers.Clear();
        defenders.Clear();
    }

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {
        if (attacker == GetCard().player.GetHero() || attacker == GetCard())
        {
            attackers.Add(attacker);
            defenders.Add(defender);

            //check if both hero and this card as hit defender
            bool heroHasHit = false;
            bool accompliceHasHit = false;

            for (int ii = 0; ii < attackers.Count; ii += 1)
            {
                if (defenders[ii] == defender && attackers[ii] == GetCard().player.GetHero())
                {
                    heroHasHit = true;
                }

                if (defenders[ii] == defender && attackers[ii] != GetCard().player.GetHero())
                {
                    accompliceHasHit = true;
                }
            }

            if (heroHasHit && accompliceHasHit)
            {
                attackers.Clear();
                defenders.Clear();

                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (defender.GetZone() == MenuControl.Instance.battleMenu.board)
                    {
                        defender.ApplyEffect(GetCard(), this, markedEffectTemplate, 1);
                    }
                });
            }
        }

    }
}
