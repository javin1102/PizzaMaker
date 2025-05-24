using PixelCrushers.DialogueSystem;

namespace PizzaMaker
{
    public interface IQuest
    {
        public abstract QuestId QuestId { get; }
        public abstract bool IsQuestActive { get; }
    }
}