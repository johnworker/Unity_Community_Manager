using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PostScheduler : MonoBehaviour
{
    private List<ScheduledPost> scheduledPosts = new List<ScheduledPost>();

    // 單例模式
    public static PostScheduler Instance;

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

    void Start()
    {
        InvokeRepeating("CheckScheduledPosts", 0f, 60f); // 每分鐘檢查一次排程貼文
    }

    // 新增排程貼文
    public void SchedulePost(string postContent, DateTime scheduledTime)
    {
        ScheduledPost newPost = new ScheduledPost(postContent, scheduledTime);
        scheduledPosts.Add(newPost);
        Debug.Log("排程新增成功: " + postContent + " 於 " + scheduledTime.ToString());
    }

    // 獲取所有排程貼文
    public List<ScheduledPost> GetScheduledPosts()
    {
        return scheduledPosts;
    }

    // 檢查是否有到達發布時間的貼文
    void CheckScheduledPosts()
    {
        DateTime now = DateTime.Now;
        for (int i = scheduledPosts.Count - 1; i >= 0; i--)
        {
            if (now >= scheduledPosts[i].scheduledTime)
            {
                StartCoroutine(APIManager.Instance.PostToFacebook(scheduledPosts[i].postContent));
                Debug.Log("正在發佈貼文：" + scheduledPosts[i].postContent);
                scheduledPosts.RemoveAt(i); // 發布後移除排程
            }
        }
    }
}
