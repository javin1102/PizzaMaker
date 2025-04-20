using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PizzaMaker
{
    [CreateAssetMenu(fileName = "PizzaMenu", menuName = "PizzaMaker/PizzaMenu")]
    public class PizzaMenuSO : ScriptableObject
    {
        public PizzaMenu pizzaMenu;
        public PizzaCooked cookedPizza;
        [ValueDropdown("allIngredients")] public List<string> ingredients;
        private string[] allIngredients => Ingredients.All;
    }
}