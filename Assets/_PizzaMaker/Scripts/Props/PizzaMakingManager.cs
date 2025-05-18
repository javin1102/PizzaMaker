using System;
using System.Collections.Generic;
using UnityEngine;
using ZLinq;

namespace PizzaMaker
{
    public class PizzaMakingManager : MonoBehaviour
    {
        [field: SerializeField] public List<PizzaMenuSO> PizzaMenuSOList { get; private set; }
        [SerializeField] private PizzaCooked invalidPizza;

        private void Awake()
        {
            // Sort by number of ingredients descendingly
            PizzaMenuSOList.Sort((a, b) => b.ingredients.Count.CompareTo(a.ingredients.Count));
        }

        public PizzaCooked BakePizza(PizzaDough pizzaDough)
        {
            foreach (PizzaMenuSO pizzaMenuSo in PizzaMenuSOList)
            {
                var isAllIngredients = pizzaDough.Ingredients?.Count >= pizzaMenuSo.ingredients.Count && 
                                      pizzaMenuSo.ingredients.AsValueEnumerable().All(i => pizzaDough.Ingredients.Contains(i));
                
                if (!isAllIngredients)
                    continue;

                var pizzaCooked = Instantiate(pizzaMenuSo.cookedPizza);
                var extraToppingList = pizzaDough.Ingredients.AsValueEnumerable().Except(pizzaMenuSo.ingredients).ToList();
                pizzaCooked.SetExtraToppingList(extraToppingList);
                return pizzaCooked;
            }
            return Instantiate(invalidPizza);
        }
    }
}