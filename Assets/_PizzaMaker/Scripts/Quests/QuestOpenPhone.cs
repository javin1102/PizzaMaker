using System;
using PixelCrushers.DialogueSystem;

namespace PizzaMaker
{
    public class QuestOpenPhone : IQuest, IDisposable
    {
        public QuestId QuestId => CustomLua.Quests.Day1OpenPhone;
        public bool IsQuestActive => QuestLog.IsQuestActive(QuestId);

        public QuestOpenPhone()
        {
            GameEvents.OnPhoneShow += CompleteQuest;
        }
            
        private void CompleteQuest()
        {
            
            if (IsQuestActive)
            {
                QuestLog.SetQuestState(CustomLua.Quests.Day1OpenPhone.id, QuestState.Success);
                GameEvents.OnQuestStateChanged?.Invoke(CustomLua.Quests.Day1OpenPhone, QuestState.Success);
                QuestLog.InformQuestStateChange(QuestId);
                Dispose();
            }
        }

        public void Dispose()
        {
            GameEvents.OnPhoneShow -= CompleteQuest;
        }

    }
}