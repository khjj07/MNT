using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : Singleton<ChatManager>
{
    public List<Chat> chats;

    public class Chat
    {
        public Player player;
        public string chat;
    }
}
