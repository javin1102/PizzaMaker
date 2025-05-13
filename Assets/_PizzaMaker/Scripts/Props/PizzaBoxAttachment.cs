using PixelCrushers.DialogueSystem.Wrappers;
using UnityEngine;

namespace PizzaMaker
{
    public class PizzaBoxAttachment : Interactable
    {
        protected override void Awake()
        {
            base.Awake();
            usable.overrideUseMessage = $"<sprite name=\"lmb\"> Place Pizza";
        }

        public override void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (playerController.CurrentIGrabbable?.GetGrabbableObject<PizzaBox>() is { } pizzaBox)
            {
                AttachPizzaBox(pizzaBox);
                playerController.UnGrab();
            }
        }

        public override void OnHover(PlayerController playerController, ref RaycastHit raycastHit)
        {
            usable.maxUseDistance = 0;
            usable.enabled = playerController.CurrentIGrabbable?.GetGrabbableObject<PizzaBox>();
            StandardUISelectorElements.instance.useMessageText.text = usable.overrideUseMessage;
        }

        public override void OnUnhover(PlayerController playerController)
        {
            
        }
        
        public void AttachPizzaBox(PizzaBox pizzaBox)
        {
            pizzaBox.transform.SetParent(transform);
            pizzaBox.transform.localRotation = Quaternion.identity;
            pizzaBox.CurrentGrabbableState = GrabbableState.Placed;
            pizzaBox.gameObject.SetGameLayerRecursive(GlobalVars.LayerDefault);
            UpdateStack();
        }

        public void UpdateStack()
        {
            Collider.enabled = transform.childCount <= 0;
            var pizzaBoxList = GetComponentsInChildren<PizzaBox>();
            for (int i = 0; i < pizzaBoxList.Length; i++)
            {
                pizzaBoxList[i].transform.localPosition = new Vector3(0f, i * 0.05f, 0f);
            }
        }
    }
}
