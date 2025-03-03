using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSeasonsBlessing : Trigger
{
    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card is Minion && tile != null)
        {
            Unit minion = (Minion)card;
            if (card.cardTags.Contains(MenuControl.Instance.niceTag))
            {
                /*
                - Give 1 Mana back when played 
                - Draw a card
                - Give your hero +2 Power this turn when played
                - Heal all units next to deployment for 2HP
                - Unit has +2 HP
                - Unit has +1 Power
                - Unit has Haste

                +1 Mana
                Draw
                +2 Power Hero
                +2 HP ADJ
                +2 HP
                +1 Power
                Haste
                */
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (minion.GetZone() != MenuControl.Instance.battleMenu.board) return;
                    int randomInt = Random.Range(0, 7);
                    if (randomInt == 0)
                    {
                        card.player.ChangeMana(1);
                        MenuControl.Instance.battleMenu.boardMenu.ShowTextOverTile(MenuControl.Instance.GetLocalizedString("+1 Mana"+"CardName"), Color.green, minion.GetTile());
                    }
                    else if (randomInt == 1)
                    {
                        card.player.DrawACard();
                        MenuControl.Instance.battleMenu.boardMenu.ShowTextOverTile(MenuControl.Instance.GetLocalizedString("Draw"+"CardName"), Color.green, minion.GetTile());
                    }
                    else if (randomInt == 2)
                    {
                        card.player.GetHero().ApplyEffect(GetCard(), this, MenuControl.Instance.heroMenu.GetEffectByID("CommonEffect07"), 2);
                        MenuControl.Instance.battleMenu.boardMenu.ShowTextOverTile(MenuControl.Instance.GetLocalizedString("+2 Power Hero"+"CardName"), Color.green, minion.GetTile());
                    }
                    else if (randomInt == 3)
                    {
                        foreach (Tile adjacentTile in tile.GetAdjacentTilesLinear())
                        {
                            if (tile.GetUnit() != null)
                            {
                                tile.GetUnit().Heal(GetCard(), this, 2);
                            }
                        }
                        MenuControl.Instance.battleMenu.boardMenu.ShowTextOverTile(MenuControl.Instance.GetLocalizedString("+2 HP ADJ"+"CardName"), Color.green, minion.GetTile());
                    }
                    else if (randomInt == 4)
                    {
                        minion.ChangeCurrentHP(this, minion.currentHP + 2);
                        MenuControl.Instance.battleMenu.boardMenu.ShowTextOverTile(MenuControl.Instance.GetLocalizedString("+2 HP"+"CardName"), Color.green, minion.GetTile());
                    }
                    else if (randomInt == 5)
                    {
                        minion.ChangePower(this, minion.currentPower + 1);
                        MenuControl.Instance.battleMenu.boardMenu.ShowTextOverTile(MenuControl.Instance.GetLocalizedString("+1 Power"+"CardName"), Color.green, minion.GetTile());
                    }
                    else if (randomInt == 6)
                    {
                        if (minion.GetEffectsWithTemplate(MenuControl.Instance.battleMenu.summoningSickEffectTemplate).Count > 0)
                        {
                            minion.RemoveEffect(null, null, minion.GetEffectsWithTemplate(MenuControl.Instance.battleMenu.summoningSickEffectTemplate)[0]);
                            minion.remainingMoves = minion.GetInitialMoves();
                            minion.remainingActions = minion.GetInitialActions(); //TODO this is because Dash/Multistrike applied after summoning so its may be missing some (fix later)
                        }
                        MenuControl.Instance.battleMenu.boardMenu.ShowTextOverTile(MenuControl.Instance.GetLocalizedString("Haste"+"CardName"), Color.green, minion.GetTile());
                    }
                });

            }

            else if (card.cardTags.Contains(MenuControl.Instance.naughtyTag))
            {
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    /*
                    - Discard a random card in your hand
                    - Unit has - 1 Power ( capped at 0 ) 
                    - Unit deals 2 damage to all units around  deployment
                    - Unit takes 1 damage when summoned
                    - Unit has -1 Movement ( capped at 0 ) 
                    - Player loses 1 mana
                    Discard
                    -1 Power
                    -2 HP ADJ
                    -1 HP
                    -1 Move
                    -1 Mana
                    */
                    int randomInt = Random.Range(0, 5);
                    if (randomInt == 0)
                    {
                        if (minion.player.cardsInHand.Count > 0)
                        {
                            Card randomCardInHand = minion.player.cardsInHand[Random.Range(0, minion.player.cardsInHand.Count)];
                            randomCardInHand.DiscardThisCard();
                        }
                        MenuControl.Instance.battleMenu.boardMenu.ShowTextOverTile(MenuControl.Instance.GetLocalizedString("Discard"+"CardName"), Color.red, minion.GetTile());
                    }
                    else if (randomInt == 1)
                    {
                        minion.ChangePower(this, minion.currentPower - 1);
                        MenuControl.Instance.battleMenu.boardMenu.ShowTextOverTile(MenuControl.Instance.GetLocalizedString("-1 Power"+"CardName"), Color.red, minion.GetTile());
                    }
                    else if (randomInt == 2)
                    {
                        foreach (Tile adjacentTile in tile.GetAdjacentTilesLinear())
                        {
                            if (tile.GetUnit() != null)
                            {
                                tile.GetUnit().SufferDamage(minion, this, 2);
                            }
                        }
                        MenuControl.Instance.battleMenu.boardMenu.ShowTextOverTile(MenuControl.Instance.GetLocalizedString("-2 HP ADJ"+"CardName"), Color.red, minion.GetTile());
                    }
                    else if (randomInt == 3)
                    {
                        minion.SufferDamage(minion, this, 1);
                        MenuControl.Instance.battleMenu.boardMenu.ShowTextOverTile(MenuControl.Instance.GetLocalizedString("-1 HP"+"CardName"), Color.red, minion.GetTile());
                    }
                    else if (randomInt == 4)
                    {
                        minion.initialMoves = Mathf.Max(0, minion.initialMoves - 1);
                        MenuControl.Instance.battleMenu.boardMenu.ShowTextOverTile(MenuControl.Instance.GetLocalizedString("-1 Move"+"CardName"), Color.red, minion.GetTile());
                    }
                    else if (randomInt == 5)
                    {
                        minion.player.ChangeMana(-1);
                        MenuControl.Instance.battleMenu.boardMenu.ShowTextOverTile(MenuControl.Instance.GetLocalizedString("-1 Mana"+"CardName"), Color.red, minion.GetTile());
                    }
                });
            }
        }
    }
}
