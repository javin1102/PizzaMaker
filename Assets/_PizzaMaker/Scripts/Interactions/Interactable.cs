using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    [RequireComponent(typeof(Usable))]
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        public Collider Collider { get; private set; }
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
            Collider = GetComponent<Collider>();
        }

        public abstract void OnClick(PlayerController playerController, ref RaycastHit raycastHit);

        public abstract void OnHover(PlayerController playerController, ref RaycastHit raycastHit);

        public abstract void OnUnhover(PlayerController playerController);
    }
}