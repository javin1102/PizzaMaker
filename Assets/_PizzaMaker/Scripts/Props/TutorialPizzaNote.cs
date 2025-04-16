using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    [RequireComponent(typeof(Focusable))]
    public class TutorialPizzaNote : Interactable
    {
        private Focusable focusable;
        private bool didAssignConversationEnded;

        protected override void Awake()
        {
            base.Awake();
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

        public override void OnClick(PlayerController playerController)
        {
            if (!IsInteractable)
                return;

            focusable.Focus();
        }

        public override void OnHover(PlayerController playerController)
        {
            if (!IsInteractable)
                return;
            
        }

        public override void OnUnhover(PlayerController playerController)
        {
            if (!IsInteractable)
                return;
            
        }
    }
}