using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    public class PizzaDough : Interactable, IGrabbable
    {
        public List<string> Ingredients => ingredients;
        public GrabbableState CurrentGrabbableState { get; set; } = GrabbableState.None;

        private Collider _collider;
        private readonly List<string> ingredients = new();
        private const string defaultUsableName = "<sprite name=\"lmb\">Grab";

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider>();
        }


        public override void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (!IsInteractable)
                return;

            if (CurrentGrabbableState == GrabbableState.Placed && playerController.CurrentIGrabbable?.GetGrabbableObject<PizzaIngredient>() is { } pizzaIngredient)
            {
                if (!ingredients.Contains(pizzaIngredient.IngredientType))
                    ingredients.Add(pizzaIngredient.IngredientType);

                pizzaIngredient.OnRelease(playerController);
                playerController.UnGrab();
            }
            else
            {
                playerController.Grab<PizzaDough>(this);
            }
        }

        public override void OnHover(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (CurrentGrabbableState == GrabbableState.None)
            {
                IsInteractable = playerController.CurrentIGrabbable == null;
                usable.overrideUseMessage = defaultUsableName;
            }
            else
            {
                var pizzaIngredient = playerController.CurrentIGrabbable?.GetGrabbableObject<PizzaIngredient>();
                IsInteractable = playerController.CurrentIGrabbable == null || pizzaIngredient != null;
                if (CurrentGrabbableState == GrabbableState.Placed)
                {
                    usable.overrideUseMessage = pizzaIngredient ? $"<sprite name=\"lmb\">Add {pizzaIngredient.IngredientType}" : defaultUsableName;
                    if (ingredients.Count <= 0)
                    {
                        InGameUIController.Instance.HideAdditionalInformationUI();
                        StandardUISelectorElements.instance.useMessageText.text = usable.overrideUseMessage;
                        return;
                    }

                    var formatIngredients = Utils.FormatIngredients(ingredients);
                    InGameUIController.Instance.ShowAdditionalInformationUI(formatIngredients);
                }
                else
                    usable.overrideUseMessage = defaultUsableName;
            }

            StandardUISelectorElements.instance.useMessageText.text = usable.overrideUseMessage;
        }

        public override void OnUnhover(PlayerController playerController)
        {
            InGameUIController.Instance.HideAdditionalInformationUI();
        }


        public T GetGrabbableObject<T>() where T : MonoBehaviour, IGrabbable
        {
            if (CurrentGrabbableState != GrabbableState.None)
            {
                return this as T;
            }

            var grabbableObject = Instantiate(gameObject).GetComponent<PizzaDough>();
            grabbableObject.Collider.enabled = false;
            grabbableObject.GetComponent<IGrabbable>();
            return grabbableObject as T;
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
            IsInteractable = playerController.CurrentIGrabbable == null;
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