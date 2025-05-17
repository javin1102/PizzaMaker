using System;
using System.Collections.Generic;
using Obvious.Soap;
using PixelCrushers.DialogueSystem;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PizzaMaker
{
    [System.Serializable]
    public class OrderMenu
    {
        public Menu menuType;
        [ValueDropdown("allIngredients")] public List<string> extraToppings;
        private string[] allIngredients => Ingredient.All;
    }

    public class NPCOrder : MonoBehaviour
    {
        [field: SerializeField] public string CustomerName { get; private set; }
        [Inject] private OrderFulFillManager orderFulFillManager;
        [field: SerializeField] public List<OrderMenu> OrderMenus { get; private set; }
        [SerializeField] private ScriptableEventString orderFulFillChannel;

        private void OnEnable()
        {
            orderFulFillManager.AddNPCOrder(this);
            orderFulFillChannel.OnRaised += OnOrderFulfilled;
        }

        private void OnOrderFulfilled(string customerName)
        {
            if (customerName != CustomerName)
                return;

            var npcAgent = GetComponent<NPCAgent>();
            if (npcAgent is null)
                return;
            
            npcAgent.ForceDisableInteractable = true;
            npcAgent.IsInteractable = false;
        }
        
        private void OnDisable()
        {
            orderFulFillManager.RemoveNPCOrder(this);
            orderFulFillChannel.OnRaised -= OnOrderFulfilled;
        }
    }
}