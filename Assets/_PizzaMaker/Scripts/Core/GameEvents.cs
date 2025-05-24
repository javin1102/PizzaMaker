using System;
using PixelCrushers.DialogueSystem;

namespace PizzaMaker
{
    public static class GameEvents
    {
        public static Action<Contact, string> OnChatReceived { get; set; }
        public static Action<QuestId, QuestState> OnQuestStateChanged { get; set; }
        public static Action OnPhoneShow { get; set; }
        public static Action OnPhoneHide { get; set; }
        public static Action<IGrabbable> OnGrab { get; set; } 
        public static Action OnUnGrab { get; set; }
        public static Action<string> OnOrderFulfilled { get; set; }
    }
}
