using System.Runtime.CompilerServices;
using PixelCrushers.DialogueSystem;
using Reflex.Attributes;
using UnityEngine;

[assembly: InternalsVisibleTo("Tests")]
namespace PizzaMaker
{
    public class PizzaBox : OrderItem, IGrabbable
    {
        public override MenuType? MenuType => PizzaCooked != null ? PizzaCooked.MenuType : null;
        public PizzaCooked PizzaCooked { get; internal set; }
        [SerializeField] private Transform pizzaCookedTransform;
        [SerializeField] private Transform pizzaTop;
        [Inject] internal OrderFulFillManager orderFulFillManager;
        public GrabbableState CurrentGrabbableState { get; set; } = GrabbableState.Placed;
        public T GetGrabbableObject<T>() where T : MonoBehaviour, IGrabbable
        {
            return this as T;
        }

        public void OnGrab(PlayerController playerController)
        {
            if(PizzaCooked)
                PizzaCooked.gameObject.SetActive(false);
            
            CurrentGrabbableState = GrabbableState.Grabbed;
        }

        public void OnGrabUsed(PlayerController playerController)
        {
            
        }

        public void OnRelease(PlayerController playerController)
        {
            Destroy(gameObject);
        }

        public override void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (PizzaCooked == null 
                && playerController.CurrentIGrabbable?.GetGrabbableObject<PizzaCooked>() is { } grabbedPizzaCooked
                && grabbedPizzaCooked.MenuType != PizzaMaker.MenuType.PizzaBurnt
            )
            {
                var childCount = pizzaCookedTransform.childCount;
                var instantiatedPizzaBox = Instantiate(this, pizzaCookedTransform);
                instantiatedPizzaBox.transform.localRotation = Quaternion.identity;
                instantiatedPizzaBox.pizzaTop.transform.localRotation = Quaternion.identity;
                instantiatedPizzaBox.transform.localPosition = new Vector3(0f, childCount * 0.05f, 0f);
                instantiatedPizzaBox.PizzaCooked = grabbedPizzaCooked;
                grabbedPizzaCooked.AttachedTo(instantiatedPizzaBox.transform);
                grabbedPizzaCooked.transform.position = instantiatedPizzaBox.transform.position;
                grabbedPizzaCooked.transform.localPosition += Vector3.one * 0.006f;
                orderFulFillManager.AddItem(instantiatedPizzaBox);
                playerController.UnGrab();
            }

            else if (PizzaCooked && playerController.CurrentIGrabbable == null)
            {
                playerController.Grab<PizzaBox>(this);
            }
        }

        public override void OnHover(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (PizzaCooked)
            {
                usable.overrideUseMessage = $"<sprite name=\"lmb\">Grab {PizzaCooked.MenuType.name}";
                usable.enabled = true;
            }
            
            else if (PizzaCooked == null 
                     && playerController.CurrentIGrabbable?.GetGrabbableObject<PizzaCooked>() is { } grabbedPizzaCooked 
                     && grabbedPizzaCooked.MenuType != PizzaMaker.MenuType.PizzaBurnt)
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