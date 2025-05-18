using System;
using PixelCrushers.DialogueSystem;

namespace PizzaMaker
{
    public static class GameEvents
    {
        public static Action<Contact, string> OnChatReceived { get; set; }
        public static Action<QuestId, QuestState> OnQuestStateChanged { get; set; }
}
}