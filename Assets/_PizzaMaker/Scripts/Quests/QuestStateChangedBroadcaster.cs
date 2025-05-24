using JetBrains.Annotations;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    public class QuestStateChangedBroadcaster : MonoBehaviour
    {
        [UsedImplicitly]
        void OnQuestStateChange(string questId)
        {
            GameEvents.OnQuestStateChanged?.Invoke(questId, QuestLog.GetQuestState(questId));
        }
    }
}