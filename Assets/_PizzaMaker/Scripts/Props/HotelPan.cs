using Sirenix.OdinInspector;
using UnityEngine;

namespace PizzaMaker
{
    public class HotelPan : Interactable
    {
        [ValueDropdown("ingredients"), SerializeField]
        private string ingredientType;

        private string[] ingredients => Ingredient.All;
        private PizzaIngredient pizzaIngredient;

        protected override void Awake()
        {
            base.Awake();
            usable.overrideUseMessage = $"<sprite name=\"lmb\"> {ingredientType}";
            var pizzaIngredientList = GetComponentsInChildren<PizzaIngredient>();
            pizzaIngredient = pizzaIngredientList[0];
            foreach (PizzaIngredient ingredient in pizzaIngredientList)
            {
                ingredient.IngredientType = ingredientType;
            }
        }

        public override void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (pizzaIngredient == null)
                return;

            playerController.Grab<PizzaIngredient>(pizzaIngredient);
        }

        public override void OnHover(PlayerController playerController, ref RaycastHit raycastHit)
        {
            IsInteractable = playerController.CurrentIGrabbable == null;
        }

        public override void OnUnhover(PlayerController playerController)
        {
        }
    }
}