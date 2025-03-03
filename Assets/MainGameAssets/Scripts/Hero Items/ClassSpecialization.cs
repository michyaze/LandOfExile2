using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClassSpecialization : MonoBehaviour
{
    public virtual List<Card> CalculateVictoryDrops(AdventureItemEncounter encounter)
    {

        List<Card> cardsToShow = new List<Card>();


        return cardsToShow;
    }

    public virtual List<Card> CalculatePurchaseDrops()
    {
        List<Card> cardsToShow = new List<Card>();


        return cardsToShow;
    }

    public virtual List<Card> CalculateChestDrops()
    {
        List<Card> cardsToShow = new List<Card>();


        return cardsToShow;
    }

    float ModiferAmount(Card card)
    {
        float returnAmount = 1f;

        if (card.level == 2 || card.level == 3)
        {
            int timesToModify = MenuControl.Instance.CountOfCardsInList(MenuControl.Instance.levelUpMenu.extraDropRate23CardsTalent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired);
            if (timesToModify > 0)
            {
                returnAmount *= timesToModify * 2f;
            }

            timesToModify =  MenuControl.Instance.CountOfCardsInList(MenuControl.Instance.levelUpMenu.extraDropRate23MinionsTalent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired);
            if (timesToModify > 0 && card is Minion)
            {
                returnAmount *= timesToModify * 1.5f;
            }

            timesToModify = MenuControl.Instance.CountOfCardsInList(MenuControl.Instance.levelUpMenu.extraDropRate23SpellsTalent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired);
            if (timesToModify > 0 && card is Castable && card.cardTags.Contains(MenuControl.Instance.spellTag))
            {
                returnAmount *= timesToModify * 1.5f;
            }

            if (MenuControl.Instance.heroMenu.ascensionMode >= 2)
            {
                returnAmount *= 0.5f;
            }
        }

        return returnAmount;

    }

    public List<Card> RestrictForAreasVisited(List<Card> allCards,int areaVisitAdd = 0)
    {
        //first step, filter out cards not supported 
        var cards = new List<Card>();
        foreach (var card in allCards)
        {
            if (MenuControl.Instance.heroMenu.isCardUnlockedAndAvailable(card) )
            {
                cards.Add(card);
            }
        }

        allCards = cards;
        
        
        //On Area 1 of the Dungeon 80 % of a card being a Level 1 card, and 20 % of it being a level 2 card.
        //On Area 2, //75% level 1, 20% level 2, 5% level 3
        //On Area 3, //65% level 1, 25% level 2, 10% level 3

        List<Card> elligibleCards = new List<Card>();

        foreach (Card card in allCards)
        {

            var areaVisited = MenuControl.Instance.areaMenu.areasVisited - 1 + areaVisitAdd;
            areaVisited = Math.Min(areaVisited,
                MenuControl.Instance.areaMenu.restrictLevelForAreasVisitedAreas.Count - 1);
            List<int> restrictLevelForAreasVisited = new List<int>();
            if (MenuControl.Instance.areaMenu.restrictLevelForAreasVisitedAreas.Count > areaVisited)
            {
                restrictLevelForAreasVisited =
                    MenuControl.Instance.areaMenu.restrictLevelForAreasVisitedAreas[areaVisited].percentageForEachLevel;
            }
            else
            {
                restrictLevelForAreasVisited =
                    MenuControl.Instance.areaMenu.restrictLevelForAreasVisitedAreas.LastItem().percentageForEachLevel;
            }

            var level = RandomUtil.RandomBasedOnProbability(restrictLevelForAreasVisited);
            if (card.level == level+1 || card.level <=0)
            {
                elligibleCards.Add(card);
            }
            
            // if (MenuControl.Instance.areaMenu.areasVisited == 1)
            // {
            //     if (randomInt <=  25 * ModiferAmount(card))
            //     {
            //         if (card.level == 2 || card.level <=0)
            //         {
            //             elligibleCards.Add(card);
            //         }
            //
            //     }
            //     else
            //     {
            //         if (card.level == 1|| card.level <=0)
            //         {
            //             elligibleCards.Add(card);
            //         }
            //     }
            // }
            // else if (MenuControl.Instance.areaMenu.areasVisited == 2) //75% level 1, 20% level 2, 5% level 3
            // {
            //     if (randomInt <= 8 * ModiferAmount(card))
            //     {
            //         if (card.level == 3|| card.level <=0)
            //         {
            //             elligibleCards.Add(card);
            //         }
            //     }
            //     else if (randomInt <= 40 * ModiferAmount(card))
            //     {
            //         if (card.level == 2|| card.level <=0)
            //         {
            //             elligibleCards.Add(card);
            //         }
            //     }
            //     else
            //     {
            //         if (card.level == 1|| card.level <=0)
            //         {
            //             elligibleCards.Add(card);
            //         }
            //     }
            // }
            // else if (MenuControl.Instance.areaMenu.areasVisited == 3) //65% level 1, 25% level 2, 10% level 3
            // {
            //     if (randomInt <= 15 * ModiferAmount(card))
            //     {
            //         if (card.level == 3|| card.level <=0)
            //         {
            //             elligibleCards.Add(card);
            //         }
            //     }
            //     else if (randomInt <= 60 * ModiferAmount(card))
            //     {
            //         if (card.level == 2|| card.level <=0)
            //         {
            //             elligibleCards.Add(card);
            //         }
            //     }
            //     else
            //     {
            //         if (card.level == 1|| card.level <=0)
            //         {
            //             elligibleCards.Add(card);
            //         }
            //     }
            // }
        }

        if (MenuControl.Instance.heroMenu.skippedLastLootDrops && MenuControl.Instance.levelUpMenu.variableTalentsAcquired.Contains(MenuControl.Instance.levelUpMenu.extra3DropNoPickTalent))
        {
            bool needAlevel3Card = true;
            foreach (Card card in elligibleCards)
            {
                if (card.level == 3|| card.level <=0)
                {
                    needAlevel3Card = false;
                }
            }
            if (needAlevel3Card)
            {
                List<Card> newList = new List<Card>();
                foreach (Card card in allCards)
                {
                    if (card.level == 3|| card.level <=0)
                    {
                        newList.Add(card);
                    }
                }
                if (newList.Count > 0)
                {
                    MenuControl.Instance.heroMenu.skippedLastLootDrops = false;
                    return newList;
                }
            }
        }

        return elligibleCards;
    }

    public Card ChooseFromElligibleCards(List<Card> elligibleCards, bool includeBetterCards = false,bool exceptTreasure = false)
    {
        List<int> dropValues = new List<int>();
        if (exceptTreasure)
        {
            
            var newElligibleCards = new List<Card>();
            foreach (Card card in elligibleCards)
            {
                if (!card.IsTreasure())
                {
                    newElligibleCards.Add(card);
                }
            }

            elligibleCards = newElligibleCards;
        }
        
        foreach (Card card in elligibleCards)
        {
            if (includeBetterCards)
            {
                int finalScore = Mathf.RoundToInt(card.GetBaseDropScore() * (card.GetPointScore() / 2f));
                dropValues.Add(finalScore);
            }
            else
            {
                dropValues.Add(card.GetBaseDropScore());
            }
        }

        //Adjust for duplicate lockout
        int lockoutDuplicatesAfter = 3;
        for (int ii = 0; ii < elligibleCards.Count; ii += 1)
        {
            int count = 0;
            foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
            {
                if (card == elligibleCards[ii])
                {
                    count += 1;
                }
            }
            foreach (Card card in MenuControl.Instance.heroMenu.weaponsOwned)
            {
                if (card == elligibleCards[ii])
                {
                    count += lockoutDuplicatesAfter + 1;
                }
            }

            if (count > lockoutDuplicatesAfter)
            {
                dropValues[ii] = Mathf.RoundToInt(dropValues[ii] * Mathf.Pow(0.5f, (float)(count - lockoutDuplicatesAfter))); //50% reduction in value for each additional card above lockout
                dropValues[ii] = Mathf.Max(1, dropValues[ii]);
            }
        }

        //Adjust for premium numbers
        int premiumCardAllowance = MenuControl.Instance.areaMenu.areasVisited;
        for (int ii = 0; ii < elligibleCards.Count; ii += 1)
        {
            int count = 0;
            foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
            {
                if (card.GetPremiumScore() >= 80)
                {
                    count += 1;
                }
            }

            if (count > premiumCardAllowance)
            {
                dropValues[ii] = Mathf.RoundToInt(dropValues[ii] * Mathf.Pow(0.5f, (float)(count - premiumCardAllowance))); //50% reduction in value for each additional card above allowance
                dropValues[ii] = Mathf.Max(1, dropValues[ii]);
            }
        }

        //Adjust for synergy
        int totalSynergyPoints = 0;
        List<string> synergyTags = new List<string>();
        foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
        {
            foreach (string synergyTag in card.GetSynergyTags())
            {
                if (!synergyTags.Contains(synergyTag))
                {
                    synergyTags.Add(synergyTag);
                }
                else
                {
                    totalSynergyPoints += card.GetPointScore();
                }
            }
        }

        int upperSynergyAllowance = 60;
        if (totalSynergyPoints > upperSynergyAllowance)
        {
            for (int ii = 0; ii < elligibleCards.Count; ii += 1)
            {
                foreach (string synergyTag in elligibleCards[ii].GetSynergyTags())
                {
                    if (synergyTags.Contains(synergyTag))
                    {
                        dropValues[ii] =  Mathf.RoundToInt(dropValues[ii] * Mathf.Pow(0.97f, (float)(upperSynergyAllowance - totalSynergyPoints))); //3% reduction in value for each additional synergy point above allowance
                        dropValues[ii] = Mathf.Max(1, dropValues[ii]);
                    }
                }
            }
        }

        int lowerSynergyAllowance = 40;
        if (totalSynergyPoints <= lowerSynergyAllowance)
        {
            //increase synergy droprates for every time they have not picked a synergistic card
            for (int ii = 0; ii < elligibleCards.Count; ii += 1)
            {
                foreach (string synergyTag in elligibleCards[ii].GetSynergyTags())
                {
                    if (synergyTags.Contains(synergyTag))
                    {
                        dropValues[ii] = Mathf.RoundToInt(dropValues[ii] + (0.07f * TotalValue(dropValues) * Mathf.Min(2, MenuControl.Instance.heroMenu.synergisticDropsSkipped))); //7% (max 2 times) increase in value for each additional synergy point above allowance
                        dropValues[ii] = Mathf.Max(1, dropValues[ii]);
                    }
                }
            }
        }

        //Pick a card
        int randomInt = Random.Range(1, TotalValue(dropValues) + 1);

        for (int ii = 0; ii < dropValues.Count; ii += 1)
        {
            int value = dropValues[ii];
            randomInt -= value;
            if (randomInt <= 0)
            {
                return elligibleCards[ii];
            }
        }

        if (elligibleCards.Count == 0)
        {
            return null;
        }
        return elligibleCards[Random.Range(0,elligibleCards.Count)]; //Never should have a null value but something probably went wrong
    }

    int TotalValue(List<int> values)
    {
        int totalValue = 0;
        foreach (int value in values)
        {
            totalValue += value;
        }

        return totalValue;
    }
}
