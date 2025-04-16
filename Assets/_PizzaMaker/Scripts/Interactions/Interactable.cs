using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    [RequireComponent(typeof(Usable))]
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        public virtual bool IsInteractable
        {
            get => isInteractable;
            set
            {
                isInteractable = value;
                usable.enabled = isInteractable;
            }
        }

        protected Usable usable;
        protected bool isInteractable = true;

        protected virtual void Awake()
        {
            usable = GetComponent<Usable>();
        }

        public abstract void OnClick(PlayerController playerController);

        public abstract void OnHover(PlayerController playerController);

        public abstract void OnUnhover(PlayerController playerController);
    }
}