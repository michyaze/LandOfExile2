using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueClassSpecialization : ClassSpecialization
{
    public override List<Card> CalculateVictoryDrops(AdventureItemEncounter encounter)
    {
        int seed = MenuControl.Instance.ApplySeed();
        Random.InitState(seed + MenuControl.Instance.adventureMenu.currentMapTileIndex);

        List<Card> cardsToShow = new List<Card>();

        //Add one/2 class cards
        for (int ii = 0; ii < (MenuControl.Instance.heroMenu.easyMode ? 2 : 1) + MenuControl.Instance.CountOfCardsInList(MenuControl.Instance.levelUpMenu.extraSpellDropTalent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired); ii += 1)
        {
            List<Card> eligibleCards = RestrictForAreasVisited(MenuControl.Instance.heroMenu.heroClass.classCards);
            eligibleCards.AddRange(RestrictForAreasVisited(MenuControl.Instance.heroMenu.GetUnlockedSpellCards()));
            foreach (Card card in cardsToShow)
            {
                eligibleCards.Remove(card);
            }
            if (eligibleCards.Count > 0)
            {
                Card card = ChooseFromElligibleCards(eligibleCards);
                cardsToShow.Add(card);

            }
        }

        //Add two minion cards
        for (int ii = 0; ii < 2 + MenuControl.Instance.CountOfCardsInList(MenuControl.Instance.levelUpMenu.extraMinionDropTalent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired); ii += 1)
        {
            List<Card> eligibleCards = RestrictForAreasVisited(MenuControl.Instance.heroMenu.heroPath.pathCards);
            eligibleCards.AddRange(RestrictForAreasVisited(MenuControl.Instance.heroMenu.GetUnlockedMinionCards()));
            foreach (Card card in cardsToShow)
            {
                eligibleCards.Remove(card);
            }
            if (eligibleCards.Count > 0)
            {
                Card card = ChooseFromElligibleCards(eligibleCards);
                cardsToShow.Add(card);

            }
        }


        return cardsToShow;
    }

    public override List<Card> CalculatePurchaseDrops()
    {
        //Add 3 Loot or Class cards
        List<Card> cardsToShow = new List<Card>();

        //One unlocked card
        if (MenuControl.Instance.heroMenu.GetUnlockedCards().Count > 0)
        {
            cardsToShow.Add(MenuControl.Instance.heroMenu.GetUnlockedCards()[Random.Range(0, MenuControl.Instance.heroMenu.GetUnlockedCards().Count)]);
        }

        //3 class cards
        List<Card> classCards = RestrictForAreasVisited(MenuControl.Instance.heroMenu.heroClass.classCards);
        classCards.AddRange(RestrictForAreasVisited(MenuControl.Instance.heroMenu.GetUnlockedSpellCards()));

        List<Card> bothCards = new List<Card>();
        bothCards.AddRange(classCards);
        bothCards.AddRange(MenuControl.Instance.heroMenu.GetAllLoot());

        //Add 3 loot
        List<Card> lootCards = MenuControl.Instance.heroMenu.GetAllLoot();

        for (int ii = 0; ii < 3; ii += 1)
        {
            Card card = ChooseFromElligibleCards(bothCards, true);
            if (card != null)
            {
                cardsToShow.Add(card);
                bothCards.Remove(card);
                lootCards.Remove(card);
                classCards.Remove(card);
            }
        }
        for (int ii = 0; ii < 3; ii += 1)
        {
            Card card = ChooseFromElligibleCards(classCards, true);
            if (card != null)
            {
                cardsToShow.Add(card);
                classCards.Remove(card);
            }
        }
        for (int ii = 0; ii < 3; ii += 1)
        {
            Card card = ChooseFromElligibleCards(lootCards, true);
            if (card != null)
            {
                cardsToShow.Add(card);
                lootCards.Remove(card);
            }
        }

        if (Random.Range(1, 101) <= 35)
        {
            //Add one allegiance
            List<Card> pathCards = RestrictForAreasVisited(MenuControl.Instance.heroMenu.heroPath.pathCards);
            pathCards.AddRange(RestrictForAreasVisited(MenuControl.Instance.heroMenu.GetUnlockedMinionCards()));
            if (pathCards.Count > 0)
            {
                cardsToShow.Add(ChooseFromElligibleCards(pathCards, true));
            }
        }

        return cardsToShow;
    }

    public override List<Card> CalculateChestDrops()
    {

        //Add two Loot or Path cards
        List<Card> cardsToShow = new List<Card>();

        //1x Level 2 Path Card
        List<Card> elligibleCards = MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.heroPath.pathCards, 2);
        elligibleCards.AddRange(MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.GetUnlockedMinionCards(), 2));
        Debug.Log("CalculateChestDrops Level 2 Path Card: " + elligibleCards.Count);
        cardsToShow.Add(ChooseFromElligibleCards(elligibleCards, true));

        //1x Level 3 Class Card
        elligibleCards = MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.heroClass.classCards, 3);
        elligibleCards.AddRange(MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.GetUnlockedSpellCards(), 3));
        Debug.Log("CalculateChestDrops Level 3 Class Card: " + elligibleCards.Count);
        cardsToShow.Add(ChooseFromElligibleCards(elligibleCards, true));

        //1x Potion
        elligibleCards = MenuControl.Instance.heroMenu.FilterCardsWithTag(MenuControl.Instance.heroMenu.FilterCardsWithTag(MenuControl.Instance.heroMenu.allCards, MenuControl.Instance.lootTag), MenuControl.Instance.potionTag);
        cardsToShow.Add(ChooseFromElligibleCards(elligibleCards, true));

        //1x Treasure
        elligibleCards = MenuControl.Instance.heroMenu.GetAllUnlockedTreasures();
        cardsToShow.Add(ChooseFromElligibleCards(elligibleCards, true));

        //1x Weapon
        elligibleCards.Clear();
        foreach (Card card in MenuControl.Instance.heroMenu.GetAllLoot())
        {
            if (card is NewWeapon)
            {
                elligibleCards.Add(card);
            }
        }
        cardsToShow.Add(ChooseFromElligibleCards(elligibleCards, true));

        foreach (Card card in cardsToShow.ToArray())
        {
            if (card == null)
                cardsToShow.Remove(null);
        }

        return cardsToShow;
    }
}
