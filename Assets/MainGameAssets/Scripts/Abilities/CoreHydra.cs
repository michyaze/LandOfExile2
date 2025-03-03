using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreHydra : Trigger
{
    public Minion headTemplate;
    public List<int> spawingTiles = new List<int>();
    public List<Minion> headsCountedForRespawn = new List<Minion>();
    public int lifeLostPerHead = 10;

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion.cardTemplate.UniqueID == headTemplate.UniqueID)
        {
            if (headsCountedForRespawn.Contains(minion)) return;
            headsCountedForRespawn.Add(minion);

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), null, () => {

                Hero hero = (Hero)GetCard();
                hero.ChangeCurrentHP(this, hero.currentHP - lifeLostPerHead);
            
                if (hero.GetHP() > 0)
                {
                    for (int ii = 0; ii < 2; ii += 1)
                    {

                        //Count heads on board (max 6)
                        List<Minion> heads = new List<Minion>();
                        foreach (Minion myMinion in GetCard().player.GetMinionsOnBoard())
                        {
                            if (myMinion.cardTemplate.UniqueID == headTemplate.UniqueID)
                            {
                                heads.Add(myMinion);
                            }
                        }

                        if (heads.Count < 6)
                        {
                            List<Tile> emptyTiles = new List<Tile>();
                            foreach (int integer in spawingTiles)
                            {
                                if (MenuControl.Instance.battleMenu.boardMenu.tiles[integer].isMoveable())
                                {
                                    emptyTiles.Add(MenuControl.Instance.battleMenu.boardMenu.tiles[integer]);
                                }
                            }

                            if (emptyTiles.Count > 0)
                            {
                                Minion newCard = (Minion)hero.player.CreateCardInGameFromTemplate(headTemplate);
                                newCard.TargetTile(emptyTiles[Random.Range(0, emptyTiles.Count)], false);
                            }

                        }
                    }
                }

            });
        }
    }
}
