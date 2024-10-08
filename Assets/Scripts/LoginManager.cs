using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField accessTokenInputField; // 用於手動輸入Access Token（方便測試）
    public TextMeshProUGUI feedbackText; // 顯示登入狀態的文本

    private string clientId = "你的Facebook應用程式ID"; // Facebook應用的ID
    private string redirectUri = "https://www.facebook.com/connect/login_success.html"; // Facebook的重定向URI

    // 單例模式
    public static LoginManager Instance;

    void Awake()
    {
        // 確保只有一個實例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 在場景切換時保留
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 當按下登入按鈕時，呼叫此方法
    public void LoginToFacebook()
    {
        string scope = "public_profile,publish_actions"; // 需要的權限
        string url = "https://www.facebook.com/dialog/oauth" +
                     "?client_id=" + clientId +
                     "&redirect_uri=" + redirectUri +
                     "&response_type=token" +
                     "&scope=" + scope;

        Application.OpenURL(url); // 打開瀏覽器，讓用戶登入並授權
        feedbackText.text = "請在瀏覽器中完成登入與授權。";
    }

    // 手動設置Access Token（測試用途）
    public void SetAccessToken()
    {
        string token = accessTokenInputField.text;
        if (!string.IsNullOrEmpty(token))
        {
            APIManager.Instance.SetAccessToken(token);
            feedbackText.text = "Access Token 設置成功！";
        }
        else
        {
            feedbackText.text = "請輸入有效的 Access Token。";
        }
    }
}
