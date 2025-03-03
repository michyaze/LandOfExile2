using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraHead : Trigger
{

    public Minion neckSectionTemplate;
    public Tile startingTile;
    public List<Minion> neckSections = new List<Minion>();

    public override void UnitMoved(Unit unit, Tile originalTile, Tile destinationTile)
    {
        if (unit == GetCard())
        {
            Tile lastTile = originalTile;
            foreach (Minion neckSection in neckSections)
            {
                Tile oldTile = neckSection.GetTile();
                neckSection.TargetTile(lastTile,false);
                lastTile = oldTile;
            }

            Minion newNeck = (Minion)unit.player.CreateCardInGameFromTemplate(neckSectionTemplate);
            newNeck.TargetTile(startingTile, false);
            neckSections.Add(newNeck);
        }
    }

    public override void HeroSummoned(Hero newHero)
    {
        if (GetCard() == newHero)
        {
            startingTile = newHero.GetTile();
        }
    }

    public override void MinionSummoned(Minion minion)
    {
        if (GetCard() == minion)
        {
            startingTile = minion.GetTile();
        }
    }

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion == GetCard())
        {
            foreach (Minion neckSection in neckSections.ToArray())
            {
                if (neckSection.GetZone() == MenuControl.Instance.battleMenu.board)
                {
                    neckSection.SufferDamage(GetCard(), this, 0, true);
                }
            }

            startingTile = null;
            neckSections.Clear();
        }

    }
}
