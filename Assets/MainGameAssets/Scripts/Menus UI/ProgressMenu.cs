using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMenu : BasicMenu {

    public int highestDamageDealt;
    //public int highestGoldRemaining;
    int totalCompletions;
    public List<int> classCompletions = new List<int>();
    public List<int> raceCompletions = new List<int>();
    public List<int> pathCompletions = new List<int>();

    public HashSet<string> cardsDiscovered = new HashSet<string>();
    public List<string> encounterCompletions = new List<string>();

    public int encountersWon;
    public int potionsConsumed;
    public int spellsCast;
    public int actionsPlayed;
    public int skillsUsed;
    public int areasExplored;
    public int totalDamageDealt;
    //public int goldCollected;
    public int xPEarned;
    public int leastTurnsUsed;

    //Non data items
    public Text progressText;
    public GameObject leaderBoardsButton;

    public void discoverCard(Card card)
    {
        cardsDiscovered.Add(card.UniqueID);
    }

    public override void ShowMenu()
    {
        base.ShowMenu();

        totalCompletions = 0;
        foreach (int integer in classCompletions)
        {
            totalCompletions += integer;
        }
     
        string stringToShow = "";
        stringToShow += MenuControl.Instance.GetLocalizedString("Highest Damage Dealt (in a run)") + ": " + highestDamageDealt;
        //stringToShow += "\n" + MenuControl.Instance.GetLocalizedString("Highest Gold Kept (in a run)") + ": " + highestGoldRemaining;
        stringToShow += "\n" + MenuControl.Instance.GetLocalizedString("Least Turns Used (in a run)") + ": " + leastTurnsUsed;
        stringToShow += "\n" + MenuControl.Instance.GetLocalizedString("Total Runs Completed") + ": " + totalCompletions;

        stringToShow += "\n" + MenuControl.Instance.GetLocalizedString("Cards Discovered") + ": " + cardsDiscovered.Count;
        stringToShow += "\n" + MenuControl.Instance.GetLocalizedString("Encounters Won") + ": " + encountersWon;
        stringToShow += "\n" + MenuControl.Instance.GetLocalizedString("Potions Consumed") + ": " + potionsConsumed;
        stringToShow += "\n" + MenuControl.Instance.GetLocalizedString("Spells Cast") + ": " + spellsCast;
        stringToShow += "\n" + MenuControl.Instance.GetLocalizedString("Actions Played") + ": " + actionsPlayed;
        stringToShow += "\n" + MenuControl.Instance.GetLocalizedString("Skills Used") + ": " + skillsUsed;
        stringToShow += "\n" + MenuControl.Instance.GetLocalizedString("Areas Explored") + ": " + areasExplored;
        stringToShow += "\n" + MenuControl.Instance.GetLocalizedString("Total Damage Dealt") + ": " + totalDamageDealt;
        //stringToShow += "\n" + MenuControl.Instance.GetLocalizedString("Gold Collected") + ": " + goldCollected;
        stringToShow += "\n" + MenuControl.Instance.GetLocalizedString("Experience Earned") + ": " + xPEarned;

        stringToShow += "\n";
        for (int ii = 0; ii < classCompletions.Count; ii += 1)
        {
            stringToShow += "\n" + MenuControl.Instance.heroMenu.heroClasses[ii].GetName() + " " + MenuControl.Instance.GetLocalizedString("Runs Completed") + ": " + classCompletions[ii];
        }
       
        for (int ii = 0; ii < pathCompletions.Count; ii += 1)
        {
            stringToShow += "\n" + MenuControl.Instance.heroMenu.heroPaths[ii].GetName() + " " + MenuControl.Instance.GetLocalizedString("Runs Completed") + ": " + pathCompletions[ii];
        }

        progressText.text = stringToShow;

#if UNITY_STANDALONE
        leaderBoardsButton.SetActive(false);
#endif


    }
}
