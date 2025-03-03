using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateCardRandomlyFromTag : Ability
{
    public CardTag cardTag;
    public int copies;

    public bool treasureOnly;
    public bool forEnemy;
    public bool putIntoHand = true;
    public bool permanently;
    public bool notItself;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Card> cards = MenuControl.Instance.heroMenu.FilterCardsWithTag(MenuControl.Instance.heroMenu.allCards, cardTag);
        if (treasureOnly)
        {
            foreach (Card card in cards.ToArray())
            {
                if (!(card.cardTags.Contains(MenuControl.Instance.treasureTag)) /*&& !(card is Deprecated Weapon)*/)
                {
                    cards.Remove(card);
                }
            }
        }
        if (notItself)
        {
            Card templateCard1 = MenuControl.Instance.heroMenu.GetCardByID(GetCard().cardTemplate.UniqueID);

            if (templateCard1 != null && cards.Contains(templateCard1))
            {
                cards.Remove(templateCard1);
            }
        }
// //remove all cards that is not minion test
//         foreach (Card card in cards.ToArray())
//         {
//             if (!(card is Minion))
//             {
//                 cards.Remove(card);
//             }
//         }
        
        if (cards.Count > 0)
        {
            for (int ii = 0; ii < copies; ii += 1)
            {

                Card cardTemplate = cards[Random.Range(0, cards.Count)];
                // //test
                // foreach (var c in cards)
                // {
                //     if (c.UniqueID == "Treasure25")
                //     {
                //         cardTemplate = c;
                //     }
                // }

                // else
                // {
                //     newCard.RemoveFromGame();
                //
                // }
//这个forEnemy有歧义，这里其实指的是AI
                if (permanently && !forEnemy)
                {
                    
                    Card newCard = MenuControl.Instance.battleMenu.player1.CreateCardInGameFromTemplate(cardTemplate);
                    var deckCard = MenuControl.Instance.heroMenu.AddCardToDeck(newCard);
                    
                    if (putIntoHand)
                    {
                        deckCard.DrawThisCard();
                    }
                    else
                    {
                        //newCard.PutIntoZone(MenuControl.Instance.battleMenu.deck);
                    }
                    //MenuControl.Instance.dataControl.SaveData();
                }else

                // if (!(newCard is Deprecated Weapon))
                {

                    Card newCard = forEnemy ? GetCard().player.GetOpponent().CreateCardInGameFromTemplate(cardTemplate) : GetCard().player.CreateCardInGameFromTemplate(cardTemplate);
                    if (putIntoHand)
                    {
                        newCard.DrawThisCard();
                    }
                    else
                    {
                        newCard.PutIntoZone(MenuControl.Instance.battleMenu.deck);
                    }

                }


            }
        }
    }
}
