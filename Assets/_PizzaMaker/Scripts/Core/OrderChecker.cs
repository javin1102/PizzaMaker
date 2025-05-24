using Obvious.Soap;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PizzaMaker;
using Reflex.Attributes;

namespace PizzaMaker
{
    public class OrderChecker : MonoBehaviour
    {
        [Inject] private OrderFulFillManager orderFulFillManager;

        void OnEnable()
        {
            Lua.RegisterFunction(nameof(CheckOrder), this, SymbolExtensions.GetMethodInfo(() => CheckOrder(string.Empty)));
        }

        void OnDisable()
        {
            Lua.UnregisterFunction(nameof(CheckOrder));
        }

        public bool CheckOrder(string customerName)
        {
            bool canFulFillOrder = orderFulFillManager.CanFulFillOrder(customerName);
            if (canFulFillOrder)
            {
                orderFulFillManager.FulFillOrder(customerName);
                GameEvents.OnOrderFulfilled?.Invoke(customerName);
            }

            return canFulFillOrder;
        }
    }
}