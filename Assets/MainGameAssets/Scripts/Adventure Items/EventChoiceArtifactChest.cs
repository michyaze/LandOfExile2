using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EventChoiceArtifactChest : EventChoice
{
    public int candidateArtifactCount = 3;
    public override void PerformChoice()
    {

        int seed = MenuControl.Instance.ApplySeed();
        Random.InitState(seed + MenuControl.Instance.adventureMenu.currentMapTileIndex);

        List<Card> artifacts = MenuControl.Instance.heroMenu.GetArtifacts(true);

        int artifactWithSchoolCount = 2;
        //找到所有卡牌(剔除初始）的流派

        var startingCards = new List<Card>( MenuControl.Instance.heroMenu.heroClass.startCards);
        startingCards.AddRange(MenuControl.Instance.pathMenu. GetUnlockbleCardList().startingDecks[0].startingCards);
        
        var startingCardUniqueIDs = startingCards.Select(x => x.UniqueID).ToList();
        
        Dictionary<string, int> cardSchools = new Dictionary<string, int>();
        foreach (Card card in MenuControl.Instance.heroMenu.cardsInDeck())
        {
            if (startingCardUniqueIDs.Contains(card.UniqueID))
            {
                startingCardUniqueIDs.Remove((card.UniqueID));
                continue;
            }

            if (!MenuControl.Instance.csvLoader.uniqueIdToPlayerCardInfo.ContainsKey(card.UniqueID))
            {
                continue;
            }
            var schools = MenuControl.Instance.csvLoader.uniqueIdToPlayerCardInfo[card.UniqueID].school;
            if (schools == null)
            {
                continue;
            }
            foreach (var school in schools)
            {
                if (cardSchools.ContainsKey(school))
                {
                     cardSchools[school]++;
                }
                else
                {
                    cardSchools.Add(school, 1);
                }
            }
        }
        
        //find top three schools with the biggest value in cardSchools
        // Step 1: Create a list of key-value pairs
        var keyValuePairs = cardSchools.ToList();

        // Step 2: Sort the list by value in descending order
        keyValuePairs.Sort((x, y) => y.Value.CompareTo(x.Value));

        // Step 3: Get the top 3 key-value pairs
        var top3 = keyValuePairs.Take(3).Select(pair => pair.Key).ToList();;

        List<Card> candidateArtifactsWithSchoolList = new List<Card>();
        //流派在前三，并且没有被拥有的artifact
        foreach (var artifact in artifacts)
        {
            if (!MenuControl.Instance.csvLoader.uniqueIdToPlayerCardInfo.ContainsKey(artifact.UniqueID))
            {
                continue;
            }
            var artifactSchools = MenuControl.Instance.csvLoader.uniqueIdToPlayerCardInfo[artifact.UniqueID].school;
            if (artifactSchools == null)
            {
                continue;
            }
            foreach (var artifactSchool in artifactSchools)
            {
                if (top3.Contains(artifactSchool) && !MenuControl.Instance.heroMenu.artifactsOwned.Contains(artifact) && !candidateArtifactsWithSchoolList.Contains(artifact))
                {
                    candidateArtifactsWithSchoolList.Add(artifact);
                }
            }
        }

        List<Card> cardsToShow = new List<Card>();
        var finalCandidateArtifactCount = candidateArtifactCount;
        if (candidateArtifactsWithSchoolList.Count > artifactWithSchoolCount)
        {
            finalCandidateArtifactCount -= artifactWithSchoolCount;
            for (int i = 0; i < artifactWithSchoolCount; i++)
            {
                var picked = candidateArtifactsWithSchoolList.PickItem();
                cardsToShow.Add(picked);
                artifacts.Remove(picked);
            }
            
        }
        else
        {
            finalCandidateArtifactCount-= candidateArtifactsWithSchoolList.Count;
            foreach (var candidateArtifact in candidateArtifactsWithSchoolList)
            {
                cardsToShow.Add(candidateArtifact);
                artifacts.Remove(candidateArtifact);
            }
        }
        
        //剩下的artifact从所有的里面挑
        Debug.Log($"高级宝箱掉落，初选{artifacts.Count}个");
        if (artifacts.Count < finalCandidateArtifactCount)
        {
            artifacts = MenuControl.Instance.heroMenu.GetArtifacts(false);
        }
        Debug.Log($"高级宝箱掉落，复选{artifacts.Count}个（可以重复）");
        var artifactCount = artifacts.Count;
        if (artifactCount < finalCandidateArtifactCount)
        {
            Debug.LogError($"高级宝箱掉落数量小于{finalCandidateArtifactCount}");
        }
        
        for(int i = 0;i<Math.Min(finalCandidateArtifactCount,artifactCount);i++)
        {
            Card card = artifacts[Random.Range(0, artifacts.Count)];
            artifacts.Remove(card);
            if (!cardsToShow.Contains(card))
            {
                cardsToShow.Add(card);
            }
        }

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => { });
        actions.Add(() => {
            for (int ii = 0; ii < MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count; ii += 1)
            {
                MenuControl.Instance.heroMenu.AddCardToDeck(MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]]);
            }
            CloseEvent();
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("SelectAnArtifactPrompt"), 1, 1, true, 1, false);

    }

}
