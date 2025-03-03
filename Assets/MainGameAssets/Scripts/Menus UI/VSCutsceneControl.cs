using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VSCutsceneControl : MonoBehaviour
{
    public CanvasGroup versusImageCanvasGroup;
    public Doozy.Engine.Soundy.SoundyData audioToPlay;
    public Doozy.Engine.Soundy.SoundyData drawSound;
    public Doozy.Engine.Soundy.SoundyData hitSound;

    public float scaleUpTo = 1.3f;

    public void ShowVSCutScene()
    {
        Doozy.Engine.Soundy.SoundyManager.Play(audioToPlay);
        Doozy.Engine.Soundy.SoundyManager.Play(drawSound);
        gameObject.SetActive(true);
        VisibleCard player1VC = Instantiate(MenuControl.Instance.visibleCardPrefab, transform);
        player1VC.RenderCard(MenuControl.Instance.battleMenu.player1.GetHero());
        player1VC.transform.localScale = Vector3.one * scaleUpTo;
        player1VC.GetComponent<RectTransform>().anchoredPosition = Vector2.left * 500f;
        player1VC.GetComponent<CanvasGroup>().alpha = 0f;
        player1VC.disableInteraction = true;

        VisibleCard playerAIVC = Instantiate(MenuControl.Instance.visibleCardPrefab, transform);
        playerAIVC.RenderCard(MenuControl.Instance.battleMenu.playerAI.GetHero());
        playerAIVC.transform.localScale = Vector3.one * scaleUpTo;
        playerAIVC.GetComponent<RectTransform>().anchoredPosition = Vector2.right * 500f;
        playerAIVC.GetComponent<CanvasGroup>().alpha = 0f;
        playerAIVC.disableInteraction = true;

        versusImageCanvasGroup.alpha = 0f;

        LeanTween.alphaCanvas(player1VC.GetComponent<CanvasGroup>(), 1f, 0.3f);

        LeanTween.rotateZ(player1VC.gameObject, 15f, 0.4f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.rotateZ(player1VC.gameObject, -15f, 0.4f).setEaseInOutSine().setOnComplete(() =>
            {
                LeanTween.rotateZ(player1VC.gameObject, 0f, 0.5f).setEaseInOutSine();
            });

        });

        LeanTween.move(player1VC.GetComponent<RectTransform>(), Vector2.left * 800f + Vector2.up*200f, 0.7f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.move(player1VC.GetComponent<RectTransform>(), Vector2.left * 150f, 0.2f).setEaseInCubic().setOnComplete(() =>
            {
                Doozy.Engine.Soundy.SoundyManager.Play(hitSound);
                LeanTween.move(player1VC.GetComponent<RectTransform>(), Vector2.left * 500f + Vector2.down * 200f, 0.4f).setEaseOutSine().setOnComplete(()=> {

                    LeanTween.delayedCall(0.7f, () =>
                    {
                        Vector2 pos = MenuControl.Instance.battleMenu.player1.GetVisibleBoardCardForCard(MenuControl.Instance.battleMenu.player1.GetHero()).GetComponent<RectTransform>().anchoredPosition;
                        LeanTween.move(player1VC.GetComponent<RectTransform>(), pos, 0.3f).setEaseInOutSine();
                    });

                    LeanTween.delayedCall(1f, () =>
                    {
                        LeanTween.alphaCanvas(player1VC.GetComponent<CanvasGroup>(), 0f, 0.3f);
                        LeanTween.scale(player1VC.gameObject, Vector3.one * 0.7f, 0.3f);
                    });

                });

                MenuControl.Instance.battleMenu.GetComponent<CameraShake>().ShakeFromHit(5);

            });

        });

        LeanTween.rotateZ(playerAIVC.gameObject, -15f, 0.4f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.rotateZ(playerAIVC.gameObject, 15f, 0.4f).setEaseInOutSine().setOnComplete(() =>
            {
                LeanTween.rotateZ(playerAIVC.gameObject, 0f, 0.5f).setEaseInOutSine();
            });

        });

        LeanTween.alphaCanvas(playerAIVC.GetComponent<CanvasGroup>(), 1f, 0.3f);
        LeanTween.move(playerAIVC.GetComponent<RectTransform>(), Vector2.right * 800f + Vector2.down * 200f, 0.7f).setEaseInOutSine().setOnComplete(() =>
        {
            LeanTween.move(playerAIVC.GetComponent<RectTransform>(), Vector2.right * 150f, 0.2f).setEaseInCubic().setOnComplete(() =>
            {

                LeanTween.alphaCanvas(versusImageCanvasGroup, 1f, 0.7f);

                LeanTween.move(playerAIVC.GetComponent<RectTransform>(), Vector2.right * 500f + Vector2.up * 200f, 0.4f).setEaseOutSine().setOnComplete(() =>
                {
                    LeanTween.delayedCall(0.7f, () =>
                    {
                        Vector2 pos = MenuControl.Instance.battleMenu.playerAI.GetVisibleBoardCardForCard(MenuControl.Instance.battleMenu.playerAI.GetHero()).GetComponent<RectTransform>().anchoredPosition;
                        if (MenuControl.Instance.battleMenu.playerAI.GetHero() is LargeHero)
                        {
                            pos += Vector2.right * 100f + Vector2.down * 100f;
                        }
                        LeanTween.move(playerAIVC.GetComponent<RectTransform>(), pos, 0.3f).setEaseInOutSine();
                    });

                    LeanTween.delayedCall(1f, () =>
                    {
                        LeanTween.alphaCanvas(playerAIVC.GetComponent<CanvasGroup>(), 0f, 0.3f);
                        LeanTween.scale(playerAIVC.gameObject, Vector3.one * 0.7f, 0.3f);

                    });

                });

            });

        });

        LeanTween.delayedCall(3f, () => {
            Destroy(player1VC.gameObject);
            Destroy(playerAIVC.gameObject);
            gameObject.SetActive(false); 
        });
    }
}
