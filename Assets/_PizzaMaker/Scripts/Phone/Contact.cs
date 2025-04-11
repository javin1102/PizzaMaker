using System;
using System.Collections.Generic;

namespace PizzaMaker
{
    [Serializable]
    public class Contact
    {
        public string name;
        public List<Chat> chats;
        public class Chat
        {
            public bool isOther;
            public string text;
        }
    }
}