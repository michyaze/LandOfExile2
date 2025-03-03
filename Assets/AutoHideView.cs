using System.Collections;
using UnityEngine;
using Doozy.Engine.UI;

public class AutoHideView : MonoBehaviour
{
    public UIView view;
    public float delay = 1f;
    private Coroutine autoHideCoroutine;

    public void ShowView()
    {
        view.Show();

        if (autoHideCoroutine != null)
        {
            StopCoroutine(autoHideCoroutine);
        }

        autoHideCoroutine = StartCoroutine(AutoHideCoroutine());
    }

    private IEnumerator AutoHideCoroutine()
    {
        yield return new WaitForSeconds(delay);
        view.Hide();
    }
}