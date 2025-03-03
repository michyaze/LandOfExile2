using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicMenu : MonoBehaviour
{

    public bool isAutoHide = false;
    private Coroutine autoHideCoroutine;

    public Animator [] animators;
    
    public virtual void ShowMenu()
    {
        GetComponent<UIView>().Show();
        if (animators!=null)
        {
            
            foreach (var animator in animators)
            {
                animator.SetTrigger("ShowMenu");
            }
        }
        foreach (var animator in animators)
        {
            animator.SetTrigger("ShowMenu");
        }
        if (isAutoHide)
        {
            
            if (autoHideCoroutine != null)
            {
                StopCoroutine(autoHideCoroutine);
            }

            autoHideCoroutine = StartCoroutine(AutoHideCoroutine());
        }
    }
    
    private IEnumerator AutoHideCoroutine()
    {
        yield return new WaitForSeconds(1);
        HideMenu();
    }
	
	public virtual void HideMenu (bool instantly = false) {

        GetComponent<UIView>().Hide(instantly);
    }

    public bool isVisible()
    {
        return GetComponent<UIView>().Visibility == VisibilityState.Visible;
    }

    public virtual void OpenMenu()
    {
        ShowMenu();
    }

    public virtual void CloseMenu()
    {

        HideMenu();
        MenuControl.Instance.adventureMenu.UpdateScreenIfOnTop();
    }

    public virtual void SelectVisibleCard(VisibleCard vc, bool withClick = true)
    {

    }

    public virtual void DeSelectVisibleCard(VisibleCard vc, bool withClick = true)
    {

    }

    public virtual void ClickVisibleCard(VisibleCard vc)
    {

    }

}
