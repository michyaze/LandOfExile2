
using System;
using System.Collections;
using System.Text;
using Doozy.Engine.UI;
using MetarCommonSupport;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginMenu : BasicMenu
{

    [SerializeField] private string webUrl = "http://account.thearky.cn:8090/";

    [SerializeField] private InputField account;

    [SerializeField] private InputField password;
    
    [SerializeField] private MainMenu mainMenu;

    /// <summary>等待下一次发送时间</summary>
    [SerializeField] private Text waitSendTime;

    [SerializeField] private GameObject popupTips;

    /// <summary>等待下次发送短信的时间</summary>
    private float waitTime = 60;

    /// <summary>是否可以发送短信验证码</summary>
    private bool canSendVerifyCode = true;
    
    [Serializable]
    private class UserCredentials
    {
        public string phone;
        public string smscode;
    }

    [Serializable]
    private class PhoneVerification
    {
        public string phone;
    }

    [Serializable]
    private class PhoneToken
    {
        public string phone;
        public string tmp_token;
    }

    public class ResponseData
    {
        public string access_token;
        public string token_type;
    }

    public override void ShowMenu()
    {
        base.ShowMenu();
        waitSendTime.text = "获取验证码";
        waitTime = 0;
        canSendVerifyCode = true;
    }


    public override void HideMenu(bool instantly = false)
    {
        base.HideMenu(instantly);
    }


    private void Update()
    {
        if (canSendVerifyCode)
        {
            return;
        }
        
        if (waitTime > 0)
        {
            canSendVerifyCode = false;
            waitTime -= Time.deltaTime;

            waitSendTime.text = $"{Mathf.FloorToInt(waitTime):D2}秒";
        }
        else
        {
            canSendVerifyCode = true;
            waitTime = 0;
            waitSendTime.text = "重新获取";
        }
    }

    /// <summary>发送短信验证码</summary>
    public void OnSendVerifyCodeClick()
    {
        if (waitTime > 0)
        {
            return;
        }
        SendMessageVerifyCode("get_sms_code", account.text);
        waitTime = 60;
        canSendVerifyCode = false;
    }
    
    public void OnRegisterBtnClick()
    {
        Register("register", account.text, password.text);
    }

    public void OnLoginBtnClick()
    {
        if (string.IsNullOrEmpty(account.text))
        {
            ShowPopMessage("请输入手机号码");
            return;
        }

        if (string.IsNullOrEmpty(password.text))
        {
            ShowPopMessage("请输入验证码");
            return;
        }
        
        if(!MetarnetRegex.IsMobilePhone(account.text))
        {
            ShowPopMessage("电话号码错误");
            return;
        }
        
        if(!MetarnetRegex.IsNumber(password.text))
        {
            ShowPopMessage("验证码不合法");
            return;
        }
        Login("login", account.text, password.text);
    }
    
    private IEnumerator SendPostRequest(string url, string jsonData, Action<string> callback)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            
            request.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var result = request.downloadHandler.text;
                Debug.Log("Response: " + result);
                callback?.Invoke(result);
            }
            else
            {
                Debug.LogError("SendRequest Error: " + request.error);
            }
        }
    }


    private void SendMessageVerifyCode(string request, string phone)
    {
        var user = new PhoneVerification { phone = phone };
        string jsonData = JsonUtility.ToJson(user);
        StartCoroutine(SendPostRequest(webUrl + request, jsonData, OnSendVerifyCodeSuccess));
    }

    private void OnSendVerifyCodeSuccess(string result)
    {
        
    }

    /// <summary>注册</summary>
    private void Register(string request, string phone, string pwd)
    {
        var user = new UserCredentials { phone = phone, smscode = pwd };
        string jsonData = JsonUtility.ToJson(user);

        StartCoroutine(SendPostRequest(webUrl + request, jsonData, OnRegisterSuccess));
    }
    
    
    private void OnRegisterSuccess(string jsonData)
    {
        var data = JsonUtility.FromJson<ResponseData>(jsonData);
        var request = "login_with_token";
        LoginWithToken(webUrl + request, account.text, data.access_token);
    }

    /// <summary>登录</summary>
    private void Login(string request, string phone, string pwd)
    {
        var user = new UserCredentials { phone = phone, smscode = pwd };
        string jsonData = JsonUtility.ToJson(user);
        StartCoroutine(SendPostRequest(webUrl + request, jsonData, OnLoginSuccess));
    }


    private void OnLoginSuccess(string jsonData)
    {
        ShowMainMenu();
    }


    private void LoginWithToken(string request, string phone, string token)
    {
        var user = new PhoneToken {phone = phone, tmp_token = token};
        string jsonData = JsonUtility.ToJson(user);
        StartCoroutine(SendPostRequest(webUrl + request, jsonData, OnLoginSuccess));
    }
    
    
    public void ShowMainMenu()
    {
        HideMenu();
        mainMenu.ShowMenu();
    }


    private void ShowPopMessage(string message)
    {
        if (LeanTween.isTweening(popupTips))
        {
            return;
        }
        var tipsMessage = popupTips.GetComponentInChildren<Text>();
        tipsMessage.text = message;
        popupTips.SetActive(true);
        popupTips.transform.localPosition = new Vector3(0, 100, 0);
        var tween = LeanTween.moveLocalY(popupTips, 300, 1f);
        LeanTween.alpha(popupTips, 0.5f, 0.5f);
        tween.setOnComplete(() =>
        {
            popupTips.SetActive(false);
        });
    }
}
