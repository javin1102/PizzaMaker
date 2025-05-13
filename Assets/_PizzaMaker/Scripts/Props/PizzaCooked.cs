using System.Collections.Generic;
using System.Runtime.CompilerServices;
using PixelCrushers.DialogueSystem.Wrappers;
using UnityEngine;
using UnityEngine.Serialization;
[assembly: InternalsVisibleTo("Tests")]
namespace PizzaMaker
{
    public class PizzaCooked : Interactable, IGrabbable
    {
        public List<string> ExtraToppingList { get; private set; } = new();
        public GrabbableState CurrentGrabbableState { get; set; } = GrabbableState.Placed;

        public MenuType MenuType
        {
            get => menuType;
            internal set => menuType = value;
        }

        [FormerlySerializedAs("menuItem")] [SerializeField]
        private MenuType menuType;


        public void SetExtraToppingList(List<string> extraToppingList)
        {
            this.ExtraToppingList = extraToppingList;
        }

        public override void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (!IsInteractable)
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