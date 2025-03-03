using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TriggerKarimKingpriest : Trigger
{
    public Minion servitorToSummon;

    public override void MinionSummoned(Minion minion)
    {
        if (GetCard() == minion)
        {
            Tile thisTile = ((Unit) GetCard()).GetTile();

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                List<Minion> eligibleMinions = new List<Minion>();
                foreach (Tile tile in thisTile.GetAdjacentTilesLinear(1))
                {
                    if (tile.GetUnit() != null && tile.GetUnit() is Minion &&
                        !eligibleMinions.Contains(tile.GetUnit()) && tile.GetUnit().player == GetCard().player)
                    {
                        eligibleMinions.Add((Minion) tile.GetUnit());
                    }
                }

                if (eligibleMinions.Count > 0)
                {
                    Minion randomMinion = eligibleMinions[Random.Range(0, eligibleMinions.Count)];
                    Tile oldTile = randomMinion.GetTile();
                    randomMinion.Sacrifice(GetCard(), this);

                    MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                    {
                        List<Tile> emptyTiles = MenuControl.Instance.battleMenu.boardMenu.GetAllEmptyTiles();
                        if (emptyTiles.Count > 0)
                        {
                            if (emptyTiles.Count > 1)
                            {
                                emptyTiles.Remove(oldTile);
                            }

                            Card newCard = GetCard().player.CreateCardInGameFromTemplate(servitorToSummon);

                            Tile tile = emptyTiles.OrderBy(x =>
                                Vector2.Distance(x.transform.position, thisTile.transform.position)).First();

                            newCard.TargetTile(tile, false);
                        }
                    }, true);
                }
            });
        }
    }
}