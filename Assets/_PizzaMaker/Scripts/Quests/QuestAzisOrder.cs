using System;
using PixelCrushers.DialogueSystem;

namespace PizzaMaker
{
    public class QuestAzisOrder : IQuest, IDisposable
    {
        public QuestId QuestId => CustomLua.Quests.Day1PizzaOrderAzis;
        public bool IsQuestActive => QuestLog.IsQuestActive(QuestId);
        
        public void Dispose()
        {
            
        }

    }
}