using System.Collections.Generic;
using PixelCrushers.DialogueSystem.Wrappers;
using UnityEngine;
using UnityEngine.Serialization;

namespace PizzaMaker
{
    public class PizzaCooked : Interactable, IGrabbable
    {
        public GrabbableState CurrentGrabbableState { get; set; } = GrabbableState.Placed;
        public MenuItem MenuItem => menuItem;
        [FormerlySerializedAs("pizzaMenu")] [SerializeField] private MenuItem menuItem;
        private List<string> extraToppingList = new();


        public void SetExtraToppingList(List<string> extraToppingList)
        {
            this.extraToppingList = extraToppingList;
        }

        public override void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if(!IsInteractable)
                return;
            
            playerController.Grab<PizzaCooked>(this);
            CurrentGrabbableState = GrabbableState.Grabbed;
        }

        public override void OnHover(PlayerController playerController, ref RaycastHit raycastHit)
        {
            IsInteractable = playerController.CurrentIGrabbable == null;
            usable.overrideUseMessage = CurrentGrabbableState == GrabbableState.Placed ? "<sprite name=\"lmb\">Grab" : "<sprite name=\"lmb\">Place";
            StandardUISelectorElements.instance.useMessageText.text = usable.overrideUseMessage;
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
            Collider.enabled = true;
            gameObject.SetGameLayerRecursive(GlobalVars.LayerDefault);
            CurrentGrabbableState = GrabbableState.Placed;
        }
    }
}