using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    [RequireComponent(typeof(Focusable))]
    public class TutorialPizzaNote : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; set; }
        
        private Focusable focusable;
        private bool didAssignConversationEnded;

        private void Awake()
        {
            focusable = GetComponent<Focusable>();
        }

        private void Start()
        {
            var hasConversationCompleted = DialogueLua.GetVariable(LuaVariables.Conversatons.Day1BossIntro).asBool;
            IsInteractable = hasConversationCompleted;
            if (hasConversationCompleted)
                return;

            DialogueManager.Instance.conversationEnded += OnConversationEnded;
            didAssignConversationEnded = true;
        }

        private void OnDestroy()
        {
            if (didAssignConversationEnded && DialogueManager.Instance)
                DialogueManager.Instance.conversationEnded -= OnConversationEnded;
        }

        private void OnConversationEnded(Transform t)
        {
            var hasConversationCompleted = DialogueLua.GetVariable(LuaVariables.Conversatons.Day1BossIntro).asBool;
            if (hasConversationCompleted)
                IsInteractable = true;
        }


        public void OnClick()
        {
            if (!IsInteractable)
                return;

            focusable.Focus();
            Debug.LogError("Clicked");
        }

        public void OnHover()
        {
            if (!IsInteractable)
                return;
            
            Debug.LogError("hover");
        }

        public void OnUnhover()
        {
            if (!IsInteractable)
                return;
            
            Debug.LogError("Unhover");
        }
    }
}