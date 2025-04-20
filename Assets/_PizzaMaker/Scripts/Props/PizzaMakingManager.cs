using System.Collections.Generic;
using UnityEngine;
using ZLinq;

namespace PizzaMaker
{
    public class PizzaMakingManager : MonoBehaviour
    {
        [field: SerializeField] public List<PizzaMenuSO> PizzaMenuSOList { get; set; }
        [SerializeField] private PizzaCooked invalidPizza;

        public PizzaCooked BakePizza(PizzaMenu pizzaMenu, PizzaDough pizzaDough)
        {
            foreach (PizzaMenuSO pizzaMenuSo in PizzaMenuSOList)
            {
                if (pizzaMenuSo.pizzaMenu != pizzaMenu) continue;
                HashSet<string> requiredIngredients = new(pizzaMenuSo.ingredients);
                pizzaDough.Ingredients.ForEach(i => requiredIngredients.Remove(i));
                if (requiredIngredients.Count > 0)
                    return Instantiate(invalidPizza);

                var pizzaCooked = Instantiate(pizzaMenuSo.cookedPizza);
                var extraToppingList = pizzaDough.Ingredients.AsValueEnumerable().Except(pizzaMenuSo.ingredients).ToList();
                pizzaCooked.SetExtraToppingList(extraToppingList);
                return pizzaCooked;
            }

            return null;
        }
    }
}