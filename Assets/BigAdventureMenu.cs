using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class BigAdventureMenu : BasicMenu
{
    public Button[] heroButtons;
    public GameObject heroDescription;
    public Sprite[] HeroSprites;
    public Image heroImage;
    public Localize heroName;
    public Localize heroDescriptionText;
    public Localize heroStateDescriptionText;
    public GameObject lockedObject;
    public GameObject unlockedObject;
    public Localize heroLockText;
    public Button startHeroButton;
    public Color lockColor;

    private int selectedHeroIndex = 0;

    public GameObject unlockView;

    public Image unlockImage;
    public Localize unlockName;
public Button unlockButton;
    public GameObject[] doorObjects;

    public GameObject unlockMenuLocked;
    public GameObject unlockMenuUnlocked;

    public GameObject unlockMenuShinyStuff;

    public float unlockImageScale = 0.5f;

    public GameObject notImage;
    // Start is called before the first frame update
    void Start()
    {
        unlockViewOriginalColor = unlockView.GetComponent<Image>().color;
        startHeroButton.onClick.AddListener(StartHero);
        unlockButton.onClick.AddListener(HideUnlockView);
        // for (int i = 0; i < heroButtons.Length; i++)
        // {
        //     int capI = i;
        //     heroButtons[i].onClick.AddListener(()=>{
        //         
        //         MenuControl.Instance.heroMenu.StartNewHero(true, false,false,false,false);
        //         MenuControl.Instance.heroMenu.SelectClass(MenuControl.Instance.heroMenu.heroClasses[capI]);
        //         //MenuControl.Instance.heroMenu.heroClass = MenuControl.Instance.heroMenu. heroClasses[capI];
        //         MenuControl.Instance.heroMenu.CreateHeroAndClose();
        //     });
        // }
    }

    void StartHero()
    {
        
        MenuControl.Instance.heroMenu.StartNewHero(true, false,false,false,false);
        MenuControl.Instance.heroMenu.SelectClass(MenuControl.Instance.heroMenu.heroClasses[selectedHeroIndex]);
        //MenuControl.Instance.heroMenu.heroClass = MenuControl.Instance.heroMenu. heroClasses[capI];
        MenuControl.Instance.heroMenu.CreateHeroAndClose();
    }

    public override void ShowMenu()
    {
        base.ShowMenu();
        for (int i = 0; i < heroButtons.Length; i++)
        {
            heroButtons[i].GetComponent<BigAdventureChess>().Init(i,this);
            if (MenuControl.Instance.heroMenu.isHeroFinished(i))
            {
                doorObjects[i].SetActive(true);
            }
            else
            {
                doorObjects [i].SetActive(false);
            }
        }
        heroDescription.SetActive(false);
    }

    public void ShowHeroDescription(int index)
    {
        selectedHeroIndex = index;
        var heroMenu = MenuControl.Instance.heroMenu;
        heroDescription.SetActive(true);
        heroImage.sprite = HeroSprites[index];
        if (heroMenu.isHeroUnlocked(index))
        {
            heroName.Term = $"Hero{index}_Name";
            heroDescriptionText.Term = $"Hero{index}_Description";
            if (heroMenu.isHeroFinished(index))
            {
                heroStateDescriptionText.Term = $"BigMap_FinishedStory";
                heroStateDescriptionText.gameObject.SetActive(true);
            }
            else
            {
                heroStateDescriptionText.gameObject.SetActive(false);
            }
            lockedObject.SetActive(false);
            unlockedObject.SetActive(true);
            heroImage.color = Color.white;
            startHeroButton.interactable = true;
        }
        else
        {
            heroLockText.Term = heroMenu.heroLockString(index);
            lockedObject.SetActive(true);
            unlockedObject.SetActive(false);
            heroImage.color = lockColor;
            startHeroButton.interactable = false;
            
        }
    }

    void HideUnlockView()
    {
        
        //unlockView.SetActive(false);
    }

    private Color unlockViewOriginalColor;
    public void ShowUnlockView(int index)
    {
        notImage.SetActive(true);
        unlockView.SetActive(true);
        unlockImage.sprite = HeroSprites[index];
        unlockName.Term = $"Hero{index}_Name";
        
        MenuControl.Instance.heroMenu.finishShowUnlockClassVisual(index);
        unlockMenuLocked.SetActive(true);
        unlockMenuUnlocked.SetActive(false);
        unlockMenuShinyStuff.SetActive(false);
        unlockImage.color = lockColor;
        unlockImage.transform.localScale = Vector3.one;
        unlockImage.transform.position = Vector3.zero;
        unlockView.GetComponent<Image>().color = unlockViewOriginalColor;
        StartCoroutine((showUnlockViewEnumerator(index)));
    }

    private float time = 0.4f;
    IEnumerator showUnlockViewEnumerator(int index)
    {
        unlockMenuLocked.transform.DOShakeRotation( time);
        yield return new WaitForSeconds(time);
        unlockMenuLocked.SetActive(false);
        unlockMenuUnlocked.SetActive(true);
        
        yield return new WaitForSeconds(time);
        unlockMenuShinyStuff.SetActive(true);
        unlockImage.color = Color.white;
        
        
        yield return new WaitForSeconds(time);
        unlockView.GetComponent<Image>().DOFade(0, time);
        
        notImage.SetActive(false);
        unlockMenuShinyStuff.SetActive(false);
        
        unlockImage.transform.DOScale(unlockImageScale, time);
        unlockImage.GetComponent<RectTransform>().DOLocalMove(heroButtons[index].GetComponent<RectTransform>().anchoredPosition, time);
        unlockImage.DOFade(0, time);
        yield return new WaitForSeconds(time);
        unlockView.SetActive(false);
    }

    private void Update()
    {
        if (!MenuControl.Instance.testMode)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ShowUnlockView(0);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            ShowUnlockView(1);
        }
    }
}
