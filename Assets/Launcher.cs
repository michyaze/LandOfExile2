using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    
    /// <summary>加载场景进度</summary>
    [SerializeField] private Slider slider;
    
    /// <summary>当前进度</summary>
    private int currentProgress;
    
    /// <summary>目标进度</summary>
    private int targetProgress;
    
    AsyncOperation operation;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentProgress = 0;
        targetProgress  = 0;
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation asyncOperation       = SceneManager.LoadSceneAsync(1); //异步加载1号场景
        asyncOperation.allowSceneActivation = false;                          //不允许场景立即激活//异步进度在 allowSceneActivation= false时，会卡在0.89999的一个值，这里乘以100转整形
        while (asyncOperation.progress < 0.9f)                                //当异步加载小于0.9f的时候
        {
            targetProgress = (int) (asyncOperation.progress * 100); //异步进度在 allowSceneActivation= false时，会卡在0.89999的一个值，这里乘以100转整形
            yield return LoadProgress(false);
            slider.value = asyncOperation.progress;
        }
        targetProgress = 100; //循环后，当前进度已经为90了，所以需要设置目标进度到100；继续循环
        // currentProgress = 90;
        yield return LoadProgress(true);
        asyncOperation.allowSceneActivation = true; //加载完毕，这里激活场景 —— 跳转场景成功
    }
    
    
    
    /// <summary>
    /// 由于需要两次调用，在这里进行简单封装
    /// </summary>
    /// <returns>等一帧</returns>
    private IEnumerator<WaitForEndOfFrame> LoadProgress(bool setProgress = false)
    {
        while (currentProgress < targetProgress) //当前进度 < 目标进度时
        {
            ++currentProgress;                            //当前进度不断累加 （Chinar温馨提示，如果场景很小，可以调整这里的值 例如：+=10 +=20，来调节加载速度）
            if (setProgress)
            {
                slider.value = (float) currentProgress / 100; //给UI进度条赋值
            }
            yield return new WaitForEndOfFrame();         //等一帧
        }
    }
}
