using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ActionAnimationMeleeHit : ActionAnimation
{
    public override float PerformAnimation(VisibleCard vc, Tile tile)
    {
        Vector3 origPos = vc.transform.position;
        Vector3 origLocalPos = vc.transform.localPosition;

        Vector3 perp = Vector2.Perpendicular(tile.transform.position - vc.transform.position);

        Vector3 pos1 = origPos - (tile.transform.position - vc.transform.position) * 0.5f + perp * 0.5f;
        Vector3 pos2 = tile.transform.position + (vc.transform.position - tile.transform.position).normalized * 0.25f;

        GameObject obj1 = vc.gameObject;
        float playSpeed = MenuControl.Instance.battleMenu.GetPlaySpeed();

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(vc.transform.DOMove(pos1, playSpeed * 0.8f).SetEase(Ease.InOutQuad));
        mySequence.Append(vc.transform.DOMove(pos2, playSpeed * 0.2f).SetEase(Ease.OutQuad));
        mySequence.Append(vc.transform.DOLocalMove(origLocalPos, playSpeed * 0.7f).SetEase(Ease.InOutQuad));

        LeanTween.rotateZ(obj1, 15f * (vc.card.player == MenuControl.Instance.battleMenu.player1 ? 1f : -1f), playSpeed * 0.8f).setOnComplete(() =>
        {
            LeanTween.rotateZ(obj1, 0f, playSpeed * 0.2f);
        });


        return playSpeed;
    }

 

}
