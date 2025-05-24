using PixelCrushers.DialogueSystem;

namespace PizzaMaker
{
    public interface IQuestHandler
    {
        public QuestId QuestId { get; }
        public bool IsQuestActive { get; }
    }
}