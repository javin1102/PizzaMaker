using System.Collections.Generic;
using UnityEngine;

namespace PizzaMaker
{
    public class PizzaCooked : Interactable, IGrabbable
    {
        public GrabbableState CurrentGrabbableState { get; set; } = GrabbableState.Placed;
        private Collider _collider;
        [SerializeField] private PizzaMenu pizzaMenu;
        private List<string> extraToppingList = new();
        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider>();
        }

        public void SetExtraToppingList(List<string> extraToppingList)
        {
            this.extraToppingList = extraToppingList;
        }

        public override void OnClick(PlayerController playerController)
        {
            if(!IsInteractable)
                return;
            
            playerController.Grab<PizzaCooked>(this);
            CurrentGrabbableState = GrabbableState.Grabbed;
        }

        public override void OnHover(PlayerController playerController)
        {
            if (CurrentGrabbableState == GrabbableState.Placed)
            {
                usable.overrideUseMessage = "<sprite name=\"lmb\">Grab";
            }
            else
            {
                usable.overrideUseMessage = "<sprite name=\"lmb\">Place";
            }
        }

        public override void OnUnhover(PlayerController playerController)
        {
        }

        public T GetGrabbableObject<T>() where T : MonoBehaviour, IGrabbable
        {
            return this as T;
        }

        public void OnGrab(PlayerController playerController)
        {
            CurrentGrabbableState = GrabbableState.Grabbed;
        }

        public void OnGrabUsed(PlayerController playerController)
        {
        }

        public void OnRelease(PlayerController playerController)
        {
            Destroy(gameObject);
        }

        public void AttachedTo(Transform parentTransform)
        {
            transform.SetParent(parentTransform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            _collider.enabled = true;
            gameObject.SetGameLayerRecursive(GlobalVars.LayerDefault);
            CurrentGrabbableState = GrabbableState.Placed;
        }
    }
}