using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Doozy.Engine.UI;
using I2.Loc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventTile : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [HideInInspector]public MapEventInfo info;
    public float posX; 
    public float posY;
    public int adventureItemIndex;
    public Transform bks;

    public GameObject eventBK;
    public GameObject enemyBK;
    public GameObject eliteBK;
    public GameObject bossBK;
    public GameObject chatBubblePrefab;
    public RectTransform chatBubbleTransform;

    public GameObject visitedCover;

    private float upAndDownAnimTime = 1;
    GameObject selectedBK = null;

    public bool isRevealed = false;
    [HideInInspector]public bool isFirstReveal = true;
    public bool isFinished = false;

    [SerializeField]private Sprite RandomEventIcon;
    [SerializeField]private Sprite StoryEventIcon;

    private bool isAnimating = false;

    [HideInInspector]
    public AdventureItemStory story;

    private float originalScale = 0.75f;
    
    public void Render()
    {
        isFinished = MenuControl.Instance.adventureMenu.adventureItemCompletions[adventureItemIndex] && !(info.eventType == "旅行商人"||info.eventType == "吟游诗人"||info.eventType == "驱灵人");

        if (MenuControl.Instance.adventureMenu.adventureItemCompletions[adventureItemIndex] && !isFinished)
        {
            visitedCover.SetActive( true);
        }
        else
        {
            visitedCover.SetActive( false);
            
        }
        if (isFinished)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (isRevealed)
            {
                if (isFirstReveal)
                {
                    isAnimating = true;
                    isFirstReveal = false;

                    // if (!MenuControl.Instance.csvLoader.EncounterNameToChineseNameMap.ContainsKey(
                    //         GetAdventureItem().name))
                    // {
                    //     Debug.LogError();
                    // }
                    // var ChineseName =
                    //     MenuControl.Instance.csvLoader.EncounterNameToChineseNameMap[GetAdventureItem().GetChineseName()];

                    var chatId = "EventName" + info.eventId;
                    
                    var chatText = MenuControl.Instance.GetLocalizedString(chatId);
                    if (chatText==chatId)
                    {
                        
                        chatId = GetAdventureItem().GetChineseName() + "_chat";
                        chatText = MenuControl.Instance.GetLocalizedString(chatId);
                    }

                    bks.transform.localScale = Vector3.one*0.1f;
                    var sequence = DOTween.Sequence().Append(bks.transform.DOScale(Vector3.one, 0.3f));
                    if (chatText!=chatId)
                    {
                        var chatBubble = Instantiate(chatBubblePrefab, MenuControl.Instance.adventureMenu.spawnAboveEverythingPos);
                        chatBubble.transform.position = chatBubbleTransform.transform.position;
                        chatBubble.transform.localScale = Vector3.zero;
                        chatBubble.GetComponentInChildren<Text>().text = chatText;
                        MenuControl.Instance.adventureMenu.HideLevelName();
                        sequence.Append(chatBubble.transform.DOScale(Vector3.one, 0.3f))
                            .AppendInterval(3f)
                            .Append(chatBubble.transform.DOScale(Vector3.zero, 0.3f)).Play().
                            OnComplete(() =>
                            {
                                chatBubble.transform.DOScale(Vector3.zero, 0.3f);
                                Destroy(chatBubble,0.3f);
                                selectedBK.transform.Find("upper").transform.DOLocalMoveY(5, upAndDownAnimTime).SetLoops(-1, LoopType.Yoyo);
                                selectedBK.transform.Find("circle").DOScale(Vector3.one * 0.5f,upAndDownAnimTime).SetLoops(-1, LoopType.Yoyo);
                                //GetComponent<UIButton>().enabled = true;
                                MenuControl.Instance.adventureMenu.ShowLevelName();

                            });
                        chatBubble.SetActive(true);
                    }
                    else
                    {
                        sequence.OnComplete(() =>
                        {
                            selectedBK.transform.Find("upper").transform.DOLocalMoveY(5, upAndDownAnimTime).SetLoops(-1, LoopType.Yoyo);
                            selectedBK.transform.Find("circle").DOScale(Vector3.one * 0.5f,upAndDownAnimTime).SetLoops(-1, LoopType.Yoyo);
                            //GetComponent<UIButton>().enabled = true;
                        });
                    }

                    sequence.Play();
                }
                else
                {
                    if (!isAnimating)
                    {
                        
                        isAnimating = true;
                        selectedBK.transform.Find("upper").transform.DOLocalMoveY(5, upAndDownAnimTime).SetLoops(-1, LoopType.Yoyo);
                        selectedBK.transform.Find("circle").DOScale(Vector3.one * 0.5f,upAndDownAnimTime).SetLoops(-1, LoopType.Yoyo);
                        
                        //GetComponent<UIButton>().enabled = true;
                    }
                    //up and down anim
                    //chatBubble
                }
            
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        
        if (gameObject.activeInHierarchy)
        {
                        
            StartCoroutine(SortChildrenByYEnumerable());
        }
    }

    private IEnumerator SortChildrenByYEnumerable()
    {
        yield return new WaitForSeconds(0.01f);
        MenuControl.Instance.adventureMenu.SortChildrenByY();
    }

    private void Update()
    {
        transform.localPosition = new Vector2(posX, posY);
    }

    //event id is the id in adventureMenu.adventureItems
    public void init(MapEventInfo info,int infoID, int eventID)
    {
        this.info = info;
        this.adventureItemIndex = eventID;
        enemyBK.SetActive(false);
        eliteBK.SetActive(false);
        eventBK.SetActive(false);
        bossBK.SetActive(false);
        originalScale = transform.localScale.x;
        if (info.eventType == "怪物")
        {
            selectedBK = enemyBK;
        }
        else if (info.eventType == "精英")
        {
            selectedBK = eliteBK;
        }
        else if (info.eventType == "BOSS")
        {
            selectedBK = bossBK;
        }
        else
        {
            selectedBK = eventBK;
        }

        selectedBK.SetActive(true);
        var encounterIconImage = selectedBK.transform.Find("upper").Find("bk").Find("icon").GetComponent<Image>();
        var encounterNameLabel = selectedBK.transform.Find("upper").GetComponentInChildren<Text>();
        
        if (info.eventType =="随机事件")
        {
            encounterIconImage.sprite = RandomEventIcon;
            encounterNameLabel.GetComponent<Localize>().Term = "Random Event";
            //encounterNameLabel.text = MenuControl.Instance.GetLocalizedString("Random Event");
            
        }else if (info.eventType == "剧情事件")
        {
            encounterIconImage.sprite = StoryEventIcon;
            encounterNameLabel.GetComponent<Localize>().Term = "Story Event";
           // encounterNameLabel.text = MenuControl.Instance.GetLocalizedString("Story Event");
           var item = GetAdventureItem();
           if (item && item.GetComponent<AdventureItemStory>())
           {
               
               story = Instantiate(item.GetComponent<AdventureItemStory>());
               //GetAdventureItem().
               //GetComponent<AdventureItemStory>()
               story .init(MenuControl.Instance.csvLoader.storyEventInfoByEventId[info.storyType],info);
           }
           else
           {
               Debug.LogError(("story item not found"));
           }
        }
        else if(info.eventType == "怪物"||
                info.eventType == "精英"||
        info.eventType == "BOSS")
        {
            if (GetAdventureItem() is AdventureItemEncounter adventureItemEncounter &&
                adventureItemEncounter.allOwnedCards.Count > 0)
            {
                
                encounterIconImage.sprite = MenuControl.Instance.csvLoader.playerCardSprite(adventureItemEncounter.allOwnedCards[0]);
                adventureItemEncounter.eventInfo = info;
            }
            else
            {
                
                encounterIconImage.sprite = MenuControl.Instance.csvLoader.playerCardSprite(GetAdventureItem().UniqueID);
            }

            //encounterNameLabel.text = GetAdventureItem().GetName();
            if (info.eventType == "怪物")
            {
                
                encounterNameLabel.GetComponent<Localize>().Term = "eventTileName_normal";
               // encounterNameLabel.text =MenuControl.Instance.GetLocalizedString("eventTileName_normal");
            }else if (info.eventType == "精英")
            {
                encounterNameLabel.GetComponent<Localize>().Term = "eventTileName_elite";
               // encounterNameLabel.text =MenuControl.Instance.GetLocalizedString("eventTileName_elite");
            }else if (info.eventType == "BOSS")
            {
                encounterNameLabel.GetComponent<Localize>().Term = "eventTileName_boss";
               // encounterNameLabel.text =MenuControl.Instance.GetLocalizedString("eventTileName_boss");
            }
        }
        else
        {
            
            encounterIconImage.sprite = MenuControl.Instance.csvLoader.eventIcon( GetAdventureItem().GetChineseName());
            
            encounterNameLabel.GetComponent<Localize>().Term = GetAdventureItem().UniqueID + "CardName";
            
           // encounterNameLabel.text = GetAdventureItem().GetName();
        }
    }

    public AdventureItem GetAdventureItem()
    {
        if (adventureItemIndex == -1) return null;
        if (MenuControl.Instance.adventureMenu.adventureItems == null || adventureItemIndex >= MenuControl.Instance.adventureMenu.adventureItems.Count)
        {
            Debug.LogError("adventureItemIndex out of range");
            return null;
        }
        return MenuControl.Instance.adventureMenu.adventureItems[adventureItemIndex];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // if (!flareStoneIcon.activeInHierarchy && !unexploredIcon.activeInHierarchy &&
        //     !room.gameObject.activeInHierarchy) return;

        if (eventData.button == PointerEventData.InputButton.Middle)
            return;
        else if (eventData.button == PointerEventData.InputButton.Right)
            return;

        GetComponentInParent<AdventureMenu>().ClickMapTile(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(originalScale * 1.2f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, 0.2f);
    }
}