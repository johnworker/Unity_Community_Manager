using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIManager : MonoBehaviour
{
    private string accessToken = ""; // 用戶的Access Token

    // 單例模式
    public static APIManager Instance;

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

    // 設置Access Token
    public void SetAccessToken(string token)
    {
        accessToken = token;
    }

    // 發送貼文到Facebook
    public IEnumerator PostToFacebook(string message)
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            Debug.LogError("Access Token 未設置！");
            yield break;
        }

        string postUrl = "https://graph.facebook.com/me/feed";
        WWWForm form = new WWWForm();
        form.AddField("message", message);
        form.AddField("access_token", accessToken);

        UnityWebRequest request = UnityWebRequest.Post(postUrl, form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("貼文發佈成功：" + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("貼文發佈失敗：" + request.error);
        }
    }
}
