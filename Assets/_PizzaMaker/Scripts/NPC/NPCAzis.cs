using PixelCrushers.DialogueSystem;

namespace PizzaMaker
{
    public class NPCAzis : NPCAgent
    {
        protected override void OnQuestStateChanged(QuestId questId, QuestState questState)
        {
            if (questId != CustomLua.Quests.Day1PizzaOrderAzis)
                return;

            if (questState == QuestState.Success)
                ForceDisableInteractable = true;
        }
    }
}