using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace PizzaMaker
{
    [CreateAssetMenu(fileName = "PizzaMenu", menuName = "PizzaMaker/PizzaMenu")]
    public class PizzaMenuSO : ScriptableObject
    {
        [FormerlySerializedAs("menuItem")] [FormerlySerializedAs("pizzaMenu")] public Menu menuType;
        public PizzaCooked cookedPizza;
        [ValueDropdown("allIngredients")] public List<string> ingredients;
        private string[] allIngredients => Ingredient.All;
    }
}