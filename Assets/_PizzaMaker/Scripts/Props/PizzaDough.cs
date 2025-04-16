using System.Collections.Generic;
using UnityEngine;

namespace PizzaMaker
{
    public class PizzaDough : Interactable, IGrabbable
    {
        public enum State
        {
            None,
            Placed,
            Grabbed
        }

        public Collider Collider => _collider;
        public State CurrentState { get; set; } = State.None;

        private Collider _collider;
        private readonly List<string> ingredients = new();
        
        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider>();
        }


        public override void OnClick(PlayerController playerController)
        {
            if (!IsInteractable)
                return;

            if(CurrentState == State.Placed && playerController.CurrentIGrabbable?.GetGrabbableObject<PizzaIngredient>() is {} pizzaIngredient)
            {
                ingredients.Add(pizzaIngredient.IngredientType);
                playerController.CurrentIGrabbable.OnRelease(playerController);
                playerController.UnGrab();
            }
            else
            {
                playerController.Grab<PizzaDough>(this);
            }
        }

        public override void OnHover(PlayerController playerController)
        {
            IsInteractable = playerController.CurrentIGrabbable == null || playerController.CurrentIGrabbable?.GetGrabbableObject<PizzaIngredient>() != null;
        }

        public override void OnUnhover(PlayerController playerController)
        {
        }


        public T GetGrabbableObject<T>() where T : MonoBehaviour, IGrabbable
        {
            if (CurrentState != State.None)
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
            CurrentState = State.Grabbed;
        }


        public void OnGrabUsed(PlayerController playerController)
        {
        }

        public void OnRelease(PlayerController playerController)
        {
            Destroy(gameObject);
            IsInteractable = playerController.CurrentIGrabbable == null;
        }
    }
}