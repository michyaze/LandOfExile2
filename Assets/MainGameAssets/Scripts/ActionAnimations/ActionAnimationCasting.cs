using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActionAnimationCasting : ActionAnimation
{

    public GameObject vfxEnemyTargetPrefab;
    public GameObject vfxFriendlyTargetPrefab;
    public Doozy.Engine.Soundy.SoundyData burningSound;

    public override float PerformAnimation(VisibleCard vc, Tile tile)
    {
        vc.GetComponent<Canvas>().overrideSorting = true;
        vc.GetComponent<Canvas>().sortingOrder = 2;
        float playSpeed = MenuControl.Instance.battleMenu.GetPlaySpeed();

        vc.transform.DOMove(Vector3.left * (tile.transform.position.x > 0 ? -4f : 4f) + Vector3.up * 0.5f, playSpeed * 0.7f).SetEase(Ease.InOutCubic);
        vc.transform.DORotate(Vector3.zero, playSpeed * 0.7f).SetEase(Ease.InSine);
        vc.transform.DOScale(Vector3.one * 1.5f, playSpeed * 0.7f);
        GameObject vfxPrefab = vfxEnemyTargetPrefab;
        if (tile.GetUnit() != null && tile.GetUnit().player == vc.card.player)
            vfxPrefab = vfxFriendlyTargetPrefab;

        GameObject vCToDiscard = Instantiate(vc.gameObject, vc.transform.parent) as GameObject;
        vCToDiscard.transform.localScale = Vector3.zero;

        LeanTween.delayedCall(playSpeed * 2f, () =>
    {
        vCToDiscard.transform.position = vc.transform.position;
        vCToDiscard.transform.localScale = vc.transform.localScale;

        //Vector3 pos = vc.card.player.discardPileText.transform.position;
        //if (vc.card.GetZone() == MenuControl.Instance.battleMenu.removedFromGame)
        //{
        //    pos = MenuControl.Instance.battleMenu.cardHistoryMenu.transform.position;
        //}

        Vector3 pos = tile.transform.position;

        LeanTween.move(vCToDiscard, pos, playSpeed).setEaseOutSine().setDestroyOnComplete(true);
        LeanTween.scale(vCToDiscard, Vector3.one * 0.5f, playSpeed);
        LeanTween.rotateAround(vCToDiscard, Vector3.forward, 720f, playSpeed);

        GameObject vfx = Instantiate(vfxPrefab, MenuControl.Instance.battleMenu.transform) as GameObject;
        vfx.transform.position = vc.transform.position;
        Destroy(vfx, 3f);
        MenuControl.Instance.battleMenu.arrowControl.HideArrow();
        Doozy.Engine.Soundy.SoundyManager.Play(burningSound);

    });
        if (tile.GetUnit())
            MenuControl.Instance.battleMenu.arrowControl.DrawArrowBetween(vc, tile.GetUnit().player.GetVisibleBoardCardForCard(tile.GetUnit()));



        return playSpeed * 3f;

    }
}
