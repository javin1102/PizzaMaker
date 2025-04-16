using System.Collections.Generic;
using UnityEngine;

namespace PizzaMaker
{
    [CreateAssetMenu(fileName = "PizzaMenu", menuName = "PizzaMaker/PizzaMenu")]
    public class PizzaMenuSO : ScriptableObject
    {
        public string pizzaName;
        public GameObject cookedPizza;
        public List<IngredientType> ingredients;
    }
}