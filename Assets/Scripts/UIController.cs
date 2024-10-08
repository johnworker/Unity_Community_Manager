using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_InputField postInputField; // 貼文內容輸入框
    public TMP_InputField dateInputField; // 日期輸入框（格式：yyyy-MM-dd）
    public TMP_InputField timeInputField; // 時間輸入框（格式：HH:mm）
    public TextMeshProUGUI feedbackText; // 反饋訊息顯示
    public TextMeshProUGUI scheduledPostsText; // 顯示排程貼文列表

    void Start()
    {
        UpdateScheduledPostsUI();
    }

    // 當用戶點擊排程按鈕時，呼叫此方法
    public void OnSchedulePostButtonClicked()
    {
        string postContent = postInputField.text;
        string dateString = dateInputField.text;
        string timeString = timeInputField.text;

        DateTime scheduledTime;
        if (DateTime.TryParseExact(dateString + " " + timeString, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out scheduledTime))
        {
            if (scheduledTime > DateTime.Now)
            {
                PostScheduler.Instance.SchedulePost(postContent, scheduledTime);
                feedbackText.text = "排程成功！貼文將於 " + scheduledTime.ToString() + " 發佈。";
                UpdateScheduledPostsUI();
            }
            else
            {
                feedbackText.text = "請選擇未來的時間。";
            }
        }
        else
        {
            feedbackText.text = "時間格式錯誤，請使用 yyyy-MM-dd 和 HH:mm 格式。";
        }
    }

    // 更新排程貼文列表的UI
    void UpdateScheduledPostsUI()
    {
        List<ScheduledPost> scheduledPosts = PostScheduler.Instance.GetScheduledPosts();
        scheduledPostsText.text = "排程貼文列表：\n";
        foreach (ScheduledPost post in scheduledPosts)
        {
            scheduledPostsText.text += post.scheduledTime.ToString("yyyy-MM-dd HH:mm") + " - " + post.postContent + "\n";
        }
    }
}
