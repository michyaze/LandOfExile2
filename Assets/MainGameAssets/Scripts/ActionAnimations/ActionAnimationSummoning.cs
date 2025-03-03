using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActionAnimationSummoning : ActionAnimation
{
    public override float PerformAnimation(VisibleCard vc, Tile tile)
    {
        vc.GetComponent<Canvas>().overrideSorting = true;
        vc.GetComponent<Canvas>().sortingOrder = 2;
        float playSpeed = MenuControl.Instance.battleMenu.GetPlaySpeed();
        //Vector3 origPos = vc.transform.position;

        Vector3 pos1 = tile.transform.position;

        if (vc.card.player == MenuControl.Instance.battleMenu.player1)
        {
            LeanTween.move(vc.gameObject, pos1, playSpeed).setEaseInOutSine();
            LeanTween.rotateAround(vc.gameObject, Vector3.up, 360f, playSpeed).setEaseInOutSine();
            LeanTween.scale(vc.gameObject, Vector3.one * 0.7f, playSpeed).setOnComplete(()=>
            {
                vc.Hide();
            });

        }
        else
        {
            vc.transform.DOMove(pos1, playSpeed * 0.7f).SetEase(Ease.InOutCubic);
            vc.transform.DORotate(Vector3.zero, playSpeed * 0.7f).SetEase(Ease.OutSine);
            vc.transform.DOScale(Vector3.one * 1.5f, playSpeed * 0.7f).SetEase(Ease.OutSine);
            vc.transform.DOScale(Vector3.one * 0.7f, playSpeed * 0.7f).SetDelay(playSpeed * 2.3f).OnComplete(() =>
            {
                vc.Hide();
            });

            playSpeed *= 3f;
        }

        return playSpeed;

    }
}
