using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class YouAreMine : Ability
{

    public Effect possessedEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        for (int ii = 0; ii < 2; ii += 1)
        {
            List<Minion> enemyMinons = new List<Minion>();
            foreach (Card card in sourceCard.player.GetOpponent().cardsInDeck)
            {
                if (card is Minion)
                {
                    enemyMinons.Add((Minion)card);
                }
            }
            if (enemyMinons.Count > 0)
            {
                Unit unit = enemyMinons.OrderByDescending(x => x.GetPower()).First();

                unit.player.cardsInDeck.Remove(unit);
                unit.player = unit.player.GetOpponent();
                unit.player.cardsInHand.Add(unit);

                List<Tile> boardTiles = new List<Tile>();
                boardTiles.AddRange(MenuControl.Instance.battleMenu.boardMenu.GetAllEmptyTiles().OrderBy(x => Vector2.Distance(x.transform.position, sourceCard.player.GetOpponent().GetHero().GetTile().transform.position)));
                boardTiles = MenuControl.Instance.battleMenu.aiControl.MoveDangerousTilesToEnd(boardTiles);

                foreach (Tile tile in boardTiles)
                {
                    if (unit.CanTarget(tile))
                    {
                        unit.TargetTile(tile, false);
                        unit.ApplyEffect(sourceCard, this, possessedEffectTemplate, 0);
                        break;
                    }
                }


            }

        }

    }
}
