using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceTreasureHoardSteal : EventChoice
{
    public int chanceToBeCaught;
    public AdventureItemEncounter encounter;
    public Card lastTreasure;
    public EventDefinition nextEventDefinition;

    // public override string GetName()
    // {
    //     return base.GetName().Replace("XX", chanceToBeCaught.ToString());
    // }
    public override string GetDescription()
    {
        return base.GetDescription().Replace("XX", (100-chanceToBeCaught).ToString()).Replace("YY", chanceToBeCaught.ToString());
    }

    public override void PerformChoice()
    {
        Card card = lastTreasure;
        if (chanceToBeCaught != 85)
        {
            card = MenuControl.Instance.heroMenu.GetAllUnlockedTreasures()[Random.Range(0, MenuControl.Instance.heroMenu.GetAllUnlockedTreasures().Count)];
        }

        if (Random.Range(1, 101) <= chanceToBeCaught)
        {

            MenuControl.Instance.adventureMenu.adventureItems[MenuControl.Instance.adventureMenu.selectedIndex] = encounter;
            MenuControl.Instance.dataControl.SaveData();
            MenuControl.Instance.adventureMenu.RenderScreen();
            MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter().PerformItem(MenuControl.Instance.adventureMenu.selectedIndex);
            
        }
        else
        {
            MenuControl.Instance.battleMenu.GetComponent<InBattleDialogueController>().AddPreDialogueInfo("EventTreasureHoard");
            List<Card> cardsToShow = new List<Card>();
            cardsToShow.Add(card);

            List<System.Action> actions = new List<System.Action>();
            actions.Add(() =>
            {
                if (chanceToBeCaught == 85)
                    CloseEvent();
                else
                {
                    MenuControl.Instance.eventMenu.ShowEvent(nextEventDefinition);
                }
            });
            actions.Add(() =>
            {
                MenuControl.Instance.heroMenu.AddCardToDeck(card);
                MenuControl.Instance.dataControl.SaveData();
                if (chanceToBeCaught == 85)
                    CloseEvent();
                else
                {
                    MenuControl.Instance.eventMenu.ShowEvent(nextEventDefinition);
                }
            });

            List<string> buttonStrings = new List<string>();
            buttonStrings.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
            buttonStrings.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonStrings, actions, MenuControl.Instance.GetLocalizedString("ChooseCardToAddPrompt"), 1, 1, true, 2, true);

        }

    }

}
