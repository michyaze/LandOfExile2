using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControl : Trigger
{
    public BattleMenu battleMenu;

    public int stepNumber;
    public float timeSinceLastStepShown;
    public Sprite tutorialIconSprite;
    public RectTransform handPointer;

    void Start()
    {
        battleMenu = MenuControl.Instance.battleMenu;
    }

    void Update()
    {
        if (battleMenu.inBattle && battleMenu.tutorialMode)
        {
            if (!handPointer.gameObject.activeInHierarchy && Doozy.Engine.UI.UIPopupManager.CurrentVisibleQueuePopup != null) 
            {
                if (stepNumber == 0 && Doozy.Engine.UI.UIPopupManager.CurrentVisibleQueuePopup.IsHiding)
                {
                    PositionHand(new Vector2(0f, -600f), new Vector2(100f, -300f));
                }
                else if (stepNumber == 4 && Doozy.Engine.UI.UIPopupManager.CurrentVisibleQueuePopup.Data.Labels.Count > 0 && Doozy.Engine.UI.UIPopupManager.CurrentVisibleQueuePopup.Data.Labels[0].GetComponentInChildren<Text>().text == MenuControl.Instance.GetLocalizedString("TutorialTitle7", "Targeting Cards") && Doozy.Engine.UI.UIPopupManager.CurrentVisibleQueuePopup.IsShowing)
                {
                    PositionHand(new Vector2(0f, -600f), new Vector2(100f, 100f));
                }
            }
        }
    }

    public override void GameStarted()
    {
        base.GameStarted();
        if (battleMenu.tutorialMode)
        {
            isTutorialEnded = false;
            stepNumber = 0;
            //LeanTween.cancel(battleMenu.tutorialButton);
            //battleMenu.tutorialButton.transform.localScale = Vector3.one;

            MenuControl.Instance.LogEvent("StartTutorial");
        }
    }

    public bool isTutorialEnded = false;
    public override void GameEnded(bool victory)
    {
        base.GameEnded(victory);
        if (battleMenu.tutorialMode)
        {
            foreach (var popup in GameObject.FindObjectsOfType<UIPopup>())
            {
                if (popup.Data.Images[0].sprite == tutorialIconSprite)
                {
                    popup.Hide(true);
                }
            }

            UIPopupManager.ClearQueue(true);
            ShowStep(6);
            isTutorialEnded = true;
        }

    }

    public override void TurnStarted(Player player)
    {
        base.TurnStarted(player);

        if (battleMenu.tutorialMode)
        {
            if (battleMenu.currentPlayer == battleMenu.player1)
            {
                if (battleMenu.currentRound == 1)
                    ShowStep(0);
                if (battleMenu.currentRound == 2)
                    ShowStep(3);
            }
        }
    }

    public override void MinionSummoned(Minion minion)
    {
        if (battleMenu.tutorialMode)
        {
            base.MinionSummoned(minion);
            if (minion.player == battleMenu.player1 && battleMenu.currentRound == 1)
            {
                ShowStep(1);
            }
        }
    }

    public override void UnitMoved(Unit unit, Tile originalTile, Tile destinationTile)
    {
        base.UnitMoved(unit, originalTile, destinationTile);
   
        if (battleMenu.tutorialMode)
        {
            if (battleMenu.currentPlayer == battleMenu.player1 && battleMenu.currentRound == 1 && unit == battleMenu.currentPlayer.GetHero())
            {
                ShowStep(2);
            }
        }
    }

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        base.UnitDamaged(sourceCard, unit, ability, damageAmount);

        if (battleMenu.tutorialMode)
        {
            if (battleMenu.currentPlayer == battleMenu.player1 && battleMenu.currentRound == 2 && unit.player == battleMenu.currentPlayer.GetOpponent())
            {
                if (ability is Attack)
                {
                    if (stepNumber < 4)
                        ShowStep(4);
                }
                else
                {
                    if (stepNumber < 5)
                        ShowStep(5);
                }
            }
           
        }
    }

    public void ReShowStep()
    {
        ShowStep(stepNumber);
    }
    
    

    public void ShowStep(int stepNumber)
    {
        if (isTutorialEnded)
        {
            return;
        }
        //LeanTween.cancel(battleMenu.tutorialButton);
        //LeanTween.scale(battleMenu.tutorialButton, Vector3.one, 0.7f);
        timeSinceLastStepShown = 20f;

        this.stepNumber = stepNumber;
        if (stepNumber == 0)
        {

            MenuControl.Instance.ShowNotification(tutorialIconSprite, MenuControl.Instance.GetLocalizedString("TutorialTitle1", "Tutorial"), MenuControl.Instance.GetLocalizedString("TutorialPrompt1"), true, true, true);
            MenuControl.Instance.ShowNotification(tutorialIconSprite, MenuControl.Instance.GetLocalizedString("TutorialTitle2", "Summoning"), MenuControl.Instance.GetLocalizedString("TutorialPrompt2"), false, false, false);
            //PositionHand(new Vector2(0f, -600f), new Vector2(100f, -300f));
        }
        else if (stepNumber == 1)
        {
            MenuControl.Instance.ShowNotification(tutorialIconSprite, MenuControl.Instance.GetLocalizedString("TutorialTitle3", "Movement"), MenuControl.Instance.GetLocalizedString("TutorialPrompt3"), false, true, false);
            if (battleMenu.player1.GetHero().GetTile().GetTileUp().isMoveable())
            {
                PositionHand(new Vector2(300f, -300f), new Vector2(300f, -100f));
            }
            else
            {
                PositionHand(new Vector2(300f, -300f), new Vector2(100f, -300f));
            }
        }
        else if (stepNumber == 2)
        {
            MenuControl.Instance.ShowNotification(tutorialIconSprite, MenuControl.Instance.GetLocalizedString("TutorialTitle4", "End Turn"), MenuControl.Instance.GetLocalizedString("TutorialPrompt4"), false, true, false,false);
            handPointer.transform.position = battleMenu.endTurnButton.transform.position;
            PositionHand(handPointer.anchoredPosition, handPointer.anchoredPosition + Vector2.left * 150f);
        }
        else if (stepNumber == 3)
        {
            MenuControl.Instance.ShowNotification(tutorialIconSprite, MenuControl.Instance.GetLocalizedString("TutorialTitle5", "Attacking and Damage"), MenuControl.Instance.GetLocalizedString("TutorialPrompt5"), false, true, false);
            //PositionHand(new Vector2(100f, -300f), new Vector2(100f, -100f));
            PositionHand(new Vector2(300f, -100f), new Vector2(100f, -100f));
            
        }
        else if (stepNumber == 4)
        {
            MenuControl.Instance.ShowNotification(tutorialIconSprite, MenuControl.Instance.GetLocalizedString("TutorialTitle6", "Range"), MenuControl.Instance.GetLocalizedString("TutorialPrompt6"), true, true, true);
            MenuControl.Instance.ShowNotification(tutorialIconSprite, MenuControl.Instance.GetLocalizedString("TutorialTitle7", "Targeting Cards"), MenuControl.Instance.GetLocalizedString("TutorialPrompt7"), false, false, false);

        }
        else if (stepNumber == 5)
        {
            MenuControl.Instance.ShowNotification(tutorialIconSprite, MenuControl.Instance.GetLocalizedString("TutorialTitle8", "Mana Costs"), MenuControl.Instance.GetLocalizedString("TutorialPrompt8"), true, true, true);
            MenuControl.Instance.ShowNotification(tutorialIconSprite, MenuControl.Instance.GetLocalizedString("TutorialTitle9", "Discarding"), MenuControl.Instance.GetLocalizedString("TutorialPrompt9"), true, false, true);

        }
        else if (stepNumber == 6)
        {
            MenuControl.Instance.ShowNotification(tutorialIconSprite, MenuControl.Instance.GetLocalizedString("TutorialTitle10", "Tutorial Complete"), MenuControl.Instance.GetLocalizedString("TutorialPrompt10"), true, true, true);
            MenuControl.Instance.LogEvent("CompleteTutorial");
        }
    }

    public void PositionHand(Vector2 fromPos, Vector2 toPos)
    {
        LeanTween.cancel(handPointer);
        handPointer.anchoredPosition = fromPos;
        LeanTween.move(handPointer, toPos, 1f).setEaseInSine().setLoopCount(-1).setDelay(0.3f);
        handPointer.GetComponent<Image>().color = Color.white;
        LeanTween.color(handPointer, new Color(1f, 1f, 1f, 0f), 1f).setEaseInSine().setLoopCount(-1).setDelay(0.3f);
        handPointer.gameObject.SetActive(true);
    }

    public void HideHandPointer()
    {
        LeanTween.cancel(handPointer);
        handPointer.gameObject.SetActive(false);
    }

    public bool SetEndTurnButton(bool intendedEnabled)
    {
        if (stepNumber == 2 || stepNumber == 5) return intendedEnabled;
        return false;
    }
    
}
