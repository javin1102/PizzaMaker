using System;
using PixelCrushers.DialogueSystem;

namespace PizzaMaker
{
    public static class GameEvents
    {
        public static Action<Contact, string> OnChatReceived;
        public static Action<QuestId, QuestState> OnQuestStateChanged;
    }
}