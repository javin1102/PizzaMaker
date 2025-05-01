using PixelCrushers.DialogueSystem;
using Reflex.Attributes;
using UnityEngine;

namespace PizzaMaker
{
    public class PizzaBox : Interactable
    {
        private PizzaCooked pizzaCooked;
        [SerializeField] private Transform pizzaCookedTransform;
        [SerializeField] private Transform pizzaTop;

        public override void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (pizzaCooked == null 
                && playerController.CurrentIGrabbable?.GetGrabbableObject<PizzaCooked>() is { } grabbedPizzaCooked
                && grabbedPizzaCooked.MenuItem != MenuItem.PizzaBurnt
            )
            {
                var childCount = pizzaCookedTransform.childCount;
                var instantiatedPizzaBox = Instantiate(this, pizzaCookedTransform);
                instantiatedPizzaBox.transform.localRotation = Quaternion.identity;
                instantiatedPizzaBox.pizzaTop.transform.localRotation = Quaternion.identity;
                instantiatedPizzaBox.transform.localPosition = new Vector3(0f, childCount * 0.05f, 0f);
                instantiatedPizzaBox.pizzaCooked = grabbedPizzaCooked;
                grabbedPizzaCooked.AttachedTo(instantiatedPizzaBox.transform);
                grabbedPizzaCooked.transform.position = instantiatedPizzaBox.transform.position;
                grabbedPizzaCooked.transform.localPosition += Vector3.one * 0.006f;
                playerController.UnGrab();
            }
        }

        public override void OnHover(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (pizzaCooked)
            {
                usable.overrideUseMessage = $"{pizzaCooked.MenuItem.name}";
                usable.enabled = true;
            }
            
            else if (pizzaCooked == null 
                     && playerController.CurrentIGrabbable?.GetGrabbableObject<PizzaCooked>() is { } grabbedPizzaCooked 
                     && grabbedPizzaCooked.MenuItem != MenuItem.PizzaBurnt)
            {
                usable.overrideUseMessage = "Wrap Pizza";
                usable.enabled = true;
            }
            else
            {
                usable.enabled = false;
            }

            StandardUISelectorElements.instance.useMessageText.text = usable.overrideUseMessage;
        }

        public override void OnUnhover(PlayerController playerController)
        {
        }
    }
}