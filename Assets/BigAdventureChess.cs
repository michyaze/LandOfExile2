using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BigAdventureChess : MonoBehaviour, IPointerClickHandler
{
    public Image chessImage;

    public GameObject lockImage;

    public Color lockColor;

    private int index;
    BigAdventureMenu bigAdventureMenu;

    public void Init(int i, BigAdventureMenu bigAdventureMenu)
    {
        index = i;
        this.bigAdventureMenu = bigAdventureMenu;

        if (MenuControl.Instance.heroMenu.isHeroUnlocked(i))
        {
            chessImage.color = Color.white;
            lockImage.SetActive(false);
        }
        else{
            chessImage.color = lockColor;
            lockImage.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        bigAdventureMenu.ShowHeroDescription(index);
    }
}
