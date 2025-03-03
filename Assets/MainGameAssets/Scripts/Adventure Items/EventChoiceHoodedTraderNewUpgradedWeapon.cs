using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceHoodedTraderNewUpgradedWeapon : EventChoice
{

    public int changeHP;

    public override void PerformChoice()
    {

        NewWeapon NewWeapon = (NewWeapon)MenuControl.Instance.heroMenu.hero.weapon.RandomUpgradeCard;

        if (NewWeapon == null)
        {
            int level = MenuControl.Instance.heroMenu.hero.weapon.level;

            List<NewWeapon> cardsToChooseFrom = new List<NewWeapon>();
            foreach (Card card in MenuControl.Instance.heroMenu.heroClass.classCards)
            {
                if (card is NewWeapon && card.level >= level)
                {
                    cardsToChooseFrom.Add((NewWeapon)card);
                }
            }
            foreach (Card card in MenuControl.Instance.heroMenu.GetAllLoot())
            {
                if (card is NewWeapon && card.level >= level)
                {
                    cardsToChooseFrom.Add((NewWeapon)card);
                }
            }

            if (cardsToChooseFrom.Count > 0)
                NewWeapon = cardsToChooseFrom[Random.Range(0, cardsToChooseFrom.Count)];
            else NewWeapon = MenuControl.Instance.heroMenu.hero.weapon;
        }

        if (changeHP > 0)
        {
            MenuControl.Instance.heroMenu.hero.Heal(null, null, changeHP);
        }
        else if (changeHP < 0)
        {
            MenuControl.Instance.heroMenu.hero.currentHP += changeHP;
        }


        List<Card> cardsToShow = new List<Card>();
        cardsToShow.Add(NewWeapon);

        MenuControl.Instance.heroMenu.AddCardToDeck(NewWeapon);
        MenuControl.Instance.dataControl.SaveData();
        CloseEvent();

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("OK"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => {
            MenuControl.Instance.adventureMenu.ContinueAdventure();
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("CardAddedToDeckPrompt"), 0, 0, true, -1, false);

    }
}
