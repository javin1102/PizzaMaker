using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    [RequireComponent(typeof(Usable))]
    public class NPCAgent : MonoBehaviour, IInteractable
    {
        public bool IsInteractable
        {
            get => isInteractable;
            set
            {
                isInteractable = value;
                usable.enabled = isInteractable;
            }
        }
        
        private bool isInteractable = true;
        private Usable usable;
        public bool ForceDisableInteractable { get; set; }

        protected virtual void Awake()
        {
            usable = GetComponent<Usable>();
            GameEvents.OnQuestStateChanged += OnQuestStateChanged;
        }
        
        protected virtual void OnDestroy()
        {
            GameEvents.OnQuestStateChanged -= OnQuestStateChanged;
        }

        protected virtual void Update()
        {
            if (ForceDisableInteractable)
            {
                IsInteractable = false;
                return;
            }
            
            IsInteractable = !DialogueManager.IsConversationActive;
        }
        
        protected virtual void OnQuestStateChanged(QuestId questId, QuestState questState)
        {
        }

        public void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            
        }

        public void OnHover(PlayerController playerController, ref RaycastHit raycastHit)
        {
            
        }

        public void OnUnhover(PlayerController playerController)
        {
            
        }
    }
    
}
