using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCardRandomClassSpell : Ability
{

    public int copies;
    public HeroClass classToCopy;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Card> cards = MenuControl.Instance.heroMenu.FilterCardsWithTag(MenuControl.Instance.heroMenu.ClassCards(), MenuControl.Instance.spellTag);

        if (cards.Count > 0)
        {
            for (int ii = 0; ii < copies; ii += 1)
            {

                Card cardTemplate = cards[Random.Range(0, cards.Count)];

                Card newCard = GetCard().player.CreateCardInGameFromTemplate(cardTemplate);
                newCard.gameObject.AddComponent<TriggerCopyExhausts>();
                newCard.DrawThisCard();

            }
        }
    }
}
