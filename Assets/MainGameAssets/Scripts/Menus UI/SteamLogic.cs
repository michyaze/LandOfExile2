using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_STANDALONE
using Steamworks;
#endif

public class SteamLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_STANDALONE
        if (SteamManager.Initialized)
        {
            string steamUser = SteamFriends.GetPersonaName();
            Debug.Log(steamUser);

            SyncAchievements();
        }
#endif 
    }

    public void SetAchievementComplete(Achievement achievement)
    {
        #if UNITY_STANDALONE
        if (SteamManager.Initialized)
        {
            SteamUserStats.SetAchievement(achievement.UniqueID);
            SteamUserStats.StoreStats();
        }
#endif
    }

    public void ResetAchievements()
    {
        #if UNITY_STANDALONE
        if (SteamManager.Initialized)
        {
            SteamUserStats.ResetAllStats(true);
            SteamUserStats.StoreStats();
        }
#endif
    }

    public void SyncAchievements()
    {
        #if UNITY_STANDALONE
        if (SteamManager.Initialized)
        {
            foreach (Achievement achievement in MenuControl.Instance.achievementsMenu.achievements)
            {
                if (achievement.IsCompleted())
                {
                    SteamUserStats.SetAchievement(achievement.UniqueID);
                }
            }
            SteamUserStats.StoreStats();
        }
#endif
    }
}
