using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClassSpecialization : ClassSpecialization
{
    public override List<Card> CalculateVictoryDrops(AdventureItemEncounter encounter)
    {
        int seed = MenuControl.Instance.ApplySeed();
        Random.InitState(seed + MenuControl.Instance.adventureMenu.currentMapTileIndex);

        List<Card> cardsToShow = new List<Card>();
        //Add one class cards
        for (int ii = 0; ii < 1 + MenuControl.Instance.CountOfCardsInList(MenuControl.Instance.levelUpMenu.extraSpellDropTalent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired); ii += 1)
        {
            List<Card> eligibleCards = RestrictForAreasVisited(MenuControl.Instance.heroMenu.ClassCards());
            //eligibleCards.AddRange(RestrictForAreasVisited(MenuControl.Instance.heroMenu.GetUnlockedSpellCards()));
            foreach (Card card in cardsToShow)
            {
                eligibleCards.Remove(card);
            }

            
            if (eligibleCards.Count > 0)
            {
                Card card = ChooseFromElligibleCards(eligibleCards);
                cardsToShow.Add(card);

                Debug.Log($"遭遇战战利品 一张职业卡 {card.GetChineseName()}");
            }
        }

        //Add two/3 minion cards
        for (int ii = 0; ii < (MenuControl.Instance.heroMenu.easyMode ? 3 : 2) + MenuControl.Instance.CountOfCardsInList(MenuControl.Instance.levelUpMenu.extraMinionDropTalent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired); ii += 1)
        {
            List<Card> eligibleCards = RestrictForAreasVisited(MenuControl.Instance.heroMenu.PathCards());
            //eligibleCards.AddRange(RestrictForAreasVisited(MenuControl.Instance.heroMenu.GetUnlockedMinionCards()));
            foreach (Card card in cardsToShow)
            {
                eligibleCards.Remove(card);
            }
            if (eligibleCards.Count > 0)
            {
                Card card = ChooseFromElligibleCards(eligibleCards);
                cardsToShow.Add(card);

                Debug.Log($"遭遇战战利品 两张阵营卡 {card.GetChineseName()}");
            }
        }
        var newCardsToShow = new List<Card>();
        foreach (var card in cardsToShow)
        {
            if (MenuControl.Instance.heroMenu.isCardUnlockedAndAvailable(card))
            {
                newCardsToShow.Add(card);
            }
            else
            {
                Debug.LogError("card not valid in CalculateVictoryDrops"+card.name);
            }
        }

        return newCardsToShow;
    }

    public override List<Card> CalculatePurchaseDrops()
    {
        List<Card> cardsToShow = new List<Card>();

        //One unlocked card
        //var test = MenuControl.Instance.heroMenu.GetUnlockedCards();
        //var test2 = MenuControl.Instance.heroMenu.GetUnlockedCardsExceptTreasureAndArtifact();
        var unlockedCards = RestrictForAreasVisited(MenuControl.Instance.heroMenu.GetUnlockedCardsExceptTreasureAndArtifact());
        if (unlockedCards.Count > 0)
        {
            cardsToShow.Add(unlockedCards[Random.Range(0, unlockedCards.Count)]);
        }

        //2 class cards
        List<Card> classCards = RestrictForAreasVisited(MenuControl.Instance.heroMenu.ClassCards(),1);
        classCards.AddRange(RestrictForAreasVisited(MenuControl.Instance.heroMenu.GetUnlockedSpellCards(true),1));
        Debug.Log(($"warrior class cards {classCards.Count}"));

        //Add 1 loot combination
        List<Card> lootCards = RestrictForAreasVisited(MenuControl.Instance.heroMenu.GetAllLoot(),1);
        Debug.Log(($"warrior class lootCards {lootCards.Count}"));
        
        
        List<Card> potionCards = MenuControl.Instance.heroMenu.CurrentHeroPotionCards();
        Debug.Log(($" encounter chest 1x Potion {potionCards.Count}"));
        
        List<Card> bothCards = new List<Card>();
        bothCards.AddRange(classCards);
        bothCards.AddRange(lootCards);

//add one both card
        // for (int ii = 0; ii < 1; ii += 1)
        // {
        //     Card card = ChooseFromElligibleCards(bothCards, true,true);
        //     if (card != null)
        //     {
        //         cardsToShow.Add(card);
        //         bothCards.Remove(card);
        //         lootCards.Remove(card);
        //         classCards.Remove(card);
        //         potionCards.Remove(card);
        //     }
        // }
        for (int ii = 0; ii < 2; ii += 1)
        {
            Card card = ChooseFromElligibleCards(classCards, true,true);
            if (card != null)
            {
                cardsToShow.Add(card);
                classCards.Remove(card);

                potionCards.Remove(card);
            }
        }
        for (int ii = 0; ii < 1; ii += 1)
        {
            Card card = ChooseFromElligibleCards(lootCards, true,true);
            if (card != null)
            {
                cardsToShow.Add(card);
                lootCards.Remove(card);
                potionCards.Remove(card);
            }
        }

        //if (Random.Range(1, 101) <= 35)
        //add one path card
        // {
        //     //Add one allegiance
        //     List<Card> pathCards = RestrictForAreasVisited(MenuControl.Instance.heroMenu.PathCards(),1);
        //     pathCards.AddRange(RestrictForAreasVisited(MenuControl.Instance.heroMenu.GetUnlockedMinionCardsWithoutTreasure(),1));
        //     
        //     Debug.Log(($"warrior class pathCards {pathCards.Count}"));
        //     if (pathCards.Count > 0)
        //     {
        //         var card = ChooseFromElligibleCards(pathCards, true, true);
        //         cardsToShow.Add(card);
        //         potionCards.Remove(card);
        //     }
        // }
        
        //1x Potion
        cardsToShow.Add(ChooseFromElligibleCards(potionCards, true,true));
        
        var newCardsToShow = new List<Card>();
        foreach (var card in cardsToShow)
        {
            if (MenuControl.Instance.heroMenu.isCardUnlockedAndAvailable(card))
            {
                newCardsToShow.Add(card);
            }
            else
            {
                Debug.LogError("card not valid in CalculatePurchaseDrops"+card.name);
            }
        }

        newCardsToShow = Card.SortCards(newCardsToShow);
        return newCardsToShow;
    }

    public override List<Card> CalculateChestDrops()
    {

        //Add two Loot or Path cards
        List<Card> cardsToShow = new List<Card>();

        // //1x Level 2 Path Card
        // List<Card> elligibleCards = MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.heroPath.pathCards, 2);
        // elligibleCards.AddRange(MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.GetUnlockedMinionCards(), 2));
        // cardsToShow.Add(ChooseFromElligibleCards(elligibleCards, true));

        // //1x Level 3 Class Card
        // elligibleCards = MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.heroClass.classCards, 3);
        // elligibleCards.AddRange(MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.GetUnlockedSpellCards(), 3));
        // cardsToShow.Add(ChooseFromElligibleCards(elligibleCards, true));

        //1x Potion
        List<Card> elligibleCards = MenuControl.Instance.heroMenu.CurrentHeroPotionCards();
        Debug.Log(($" encounter chest 1x Potion {elligibleCards.Count}"));
        cardsToShow.Add(ChooseFromElligibleCards(elligibleCards, true));

        //1x Treasure
        elligibleCards = MenuControl.Instance.heroMenu.GetAllUnlockedTreasures();
        Debug.Log(($" encounter chest 1x Treasure {elligibleCards.Count}"));
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
        Debug.Log((($" encounter chest 1x Weapon {elligibleCards.Count}")));
        
        elligibleCards.AddRange(MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.FilterCardsWithTag(MenuControl.Instance.heroMenu.CurrentHeroAllCards(), MenuControl.Instance.spellTag), 3));
        cardsToShow.Add(ChooseFromElligibleCards(elligibleCards, true));
        
        
        foreach (Card card in cardsToShow.ToArray())
        {
            if (card == null)
                cardsToShow.Remove(null);
        }

        var newCardsToShow = new List<Card>();
        foreach (var card in cardsToShow)
        {
            if (MenuControl.Instance.heroMenu.isCardUnlockedAndAvailable(card))
            {
                newCardsToShow.Add(card);
            }
            else
            {
                Debug.LogError("card not valid in CalculateChestDrops"+card.name);
            }
        }

        return newCardsToShow;
    }
}
