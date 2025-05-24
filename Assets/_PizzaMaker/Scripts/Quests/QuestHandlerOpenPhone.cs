using System;
using PixelCrushers.DialogueSystem;

namespace PizzaMaker
{
    public class QuestHandlerOpenPhone : IQuestHandler, IDisposable
    {
        public QuestId QuestId => CustomLua.Quests.Day1OpenPhone;
        public bool IsQuestActive => QuestLog.IsQuestActive(QuestId);

        public QuestHandlerOpenPhone()
        {
            GameEvents.OnPhoneShow += CompleteQuest;
        }
            
        private void CompleteQuest()
        {
            
            if (IsQuestActive)
            {
                QuestLog.SetQuestState(CustomLua.Quests.Day1OpenPhone.id, QuestState.Success);
                Dispose();
            }
        }

        public void Dispose()
        {
            GameEvents.OnPhoneShow -= CompleteQuest;
        }

    }
}