using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshButton : MonoBehaviour
{
    public GameObject downFace;
    public GameObject upFace;
    public Text costText;
    public void UpdateView(int cost)
    {
         costText.text = cost.ToString();
         if (MenuControl.Instance.heroMenu.flareStones >= MenuControl.Instance.shopMenu.refreshFlareStoneCost())
         {
             costText.color = Color.white;
             GetComponentInChildren<Button>(true).interactable = true;
         }
         else
         {
             costText.color = Color.red;
             GetComponentInChildren<Button>(true).interactable = false;
         }
         upFace.SetActive(true);
         downFace.SetActive(false);
    }

    public void UpdateView()
    {
        
        upFace.SetActive(false);
        downFace.SetActive(true);
        GetComponentInChildren<Button>(true).interactable = false;
    }

}
