using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChatManager : Singleton<ChatManager>
{
    public List<Chat> startChats;
    private int curStartChatCount = 0;

    public List<Chat> endChats;
    private int curEndChatCount = 0;

    public UnityAction startAction;
    public UnityAction endAction;

    [System.Serializable]
    public class Chat
    {
        public Player player;
        [Multiline(4)]
        public string chat;
    }

    public Chat GetStartChat()
    {
        if (curStartChatCount < startChats.Count)
        {
            return startChats[curStartChatCount++];
        }
        else
        {
            return null;
        }
    }
}
