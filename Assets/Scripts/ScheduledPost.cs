using System;

[Serializable]
public class ScheduledPost
{
    public string postContent; // 貼文內容
    public DateTime scheduledTime; // 排程時間

    public ScheduledPost(string content, DateTime time)
    {
        postContent = content;
        scheduledTime = time;
    }
}
