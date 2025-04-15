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

        private void Awake()
        {
            usable = GetComponent<Usable>();
        }

        private void Update()
        {
            IsInteractable = !DialogueManager.IsConversationActive;
        }


        public void OnClick()
        {
            
        }

        public void OnHover()
        {
            
        }

        public void OnUnhover()
        {
        }
    }
    
}
