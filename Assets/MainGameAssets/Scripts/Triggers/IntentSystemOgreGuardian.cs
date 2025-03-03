using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IntentSystemOgreGuardian : IntentSystem
{
    [Header("Switch State")]
    public int switchStateHp;
    public int switchStateHandIndex;
    private bool hasSwitched = false;
    public override void TurnEnded(Player player)
    {
        if (GetCard().player == player)
        {
            handIndex += 1;

            if (((LargeHero)GetCard()).GetHP() >= switchStateHp)
            {
                if (handIndex == switchStateHandIndex)
                    handIndex = 0;
            }
            else
            {
                if (handIndex == hands.Count)
                    handIndex = switchStateHandIndex;

            }
        }
    }

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (unit == GetCard() && unit.GetHP() < switchStateHp && !hasSwitched)
        {
            hasSwitched = true;
            handIndex = switchStateHandIndex;
            
            MenuControl.Instance.battleMenu.playerAI.ChangeCardsInHand(GetCurrentHand().cards);
            LeanTween.delayedCall(MenuControl.Instance.battleMenu.GetPlaySpeed() + 0.4f, () =>
            {
                MenuControl.Instance.battleMenu.playerAI.RenderIntent2ndHand();
                //playerAI.RenderIntent2ndHand();
            });
        }
    }



    public override IntentSystemHand GetNextHand()
    {
        int nextIndex = handIndex + 1;


        if (((LargeHero)GetCard()).GetHP() >= switchStateHp)
        {
            if (nextIndex == switchStateHandIndex)
                nextIndex = 0;
        }
        else
        {
            if (nextIndex == hands.Count)
                nextIndex = switchStateHandIndex;

        }



        return hands[nextIndex];
    }
}
