using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    [RequireComponent(typeof(Focusable))]
    public class TutorialPizzaNote : Interactable
    {
        private Focusable focusable;
        private bool didAssignConversationEnded;
        private bool hasConversationCompleted;

        protected override void Awake()
        {
            base.Awake();
            focusable = GetComponent<Focusable>();
        }

        private void Start()
        {
            hasConversationCompleted = DialogueLua.GetVariable(CustomLua.Variables.Day1BossIntro).asBool;
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
            hasConversationCompleted = DialogueLua.GetVariable(CustomLua.Variables.Day1BossIntro).asBool;
            if (hasConversationCompleted)
                IsInteractable = true;
        }

        public override void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (!IsInteractable)
                return;

            focusable.Focus();
        }

        public override void OnHover(PlayerController playerController, ref RaycastHit raycastHit)
        {
            IsInteractable = hasConversationCompleted && playerController.CurrentIGrabbable == null;
        }

        public override void OnUnhover(PlayerController playerController)
        {
            if (!IsInteractable)
                return;
            
        }
    }
}