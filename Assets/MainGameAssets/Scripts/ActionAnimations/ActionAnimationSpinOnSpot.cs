using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAnimationSpinOnSpot : ActionAnimation
{
    public GameObject rangedProjectilePrefab;

    public override float PerformAnimation(VisibleCard vc, Tile tile)
    {
        float playSpeed = MenuControl.Instance.battleMenu.GetPlaySpeed();
        Vector3 origPos = vc.transform.position;

        Vector3 pos1 = origPos - (tile.transform.position - vc.transform.position) * 0.25f;

        LeanTween.rotateAroundLocal(vc.gameObject, Vector3.up, 360f, playSpeed).setEaseInOutSine();
        LeanTween.move(vc.gameObject, pos1, playSpeed * 0.5f).setEaseInOutSine();
        LeanTween.move(vc.gameObject, origPos, playSpeed * 0.5f).setEaseInOutSine().setDelay(0.5f * playSpeed);

        LeanTween.delayedCall(playSpeed / 2f, () =>
         {
             GameObject rangedProjectile = Instantiate(rangedProjectilePrefab, MenuControl.Instance.battleMenu.transform) as GameObject;
             rangedProjectile.transform.position = vc.transform.position;
             rangedProjectile.transform.rotation = Quaternion.LookRotation(-Vector3.forward, tile.transform.position - rangedProjectile.transform.position);

             LeanTween.move(rangedProjectile, tile.transform.position, playSpeed / 2f);
             Destroy(rangedProjectile, playSpeed / 2f);
         });

        return playSpeed;
    }
}
