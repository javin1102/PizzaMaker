using System;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PizzaMaker
{
    [System.Serializable]
    public class OrderMenu
    {
        public MenuType menuType;
        [ValueDropdown("allIngredients")] public List<string> extraToppings;
        private string[] allIngredients => Ingredient.All;
    }

    public class NPCOrder : MonoBehaviour
    {
        [field: SerializeField] public string CustomerName { get; private set; }
        [Inject] private OrderFulFillManager orderFulFillManager;
        [field: SerializeField] public List<OrderMenu> OrderMenus { get; private set; }

        private void OnEnable()
        {
            orderFulFillManager.AddNPCOrder(this);
        }

        private void OnDisable()
        {
            orderFulFillManager.RemoveNPCOrder(this);
        }

        private void OnUse()
        {
            if (orderFulFillManager.IsOrderFulfilled(OrderMenus))
            {
                DialogueLua.SetVariable(CustomLua.Variables.Day1AzisOrderFulfilled, true);
            }
            else
            {
                Debug.LogError("NO");
            }
        }
    }
}