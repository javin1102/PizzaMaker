using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PizzaMaker
{
    [CreateAssetMenu(fileName = "PizzaMenu", menuName = "PizzaMaker/PizzaMenu")]
    public class PizzaMenuSO : ScriptableObject
    {
        public string pizzaName;
        public GameObject cookedPizza;
        [ValueDropdown("allIngredients")] public List<string> ingredients;
        private string[] allIngredients => Ingredients.All;
    }
}