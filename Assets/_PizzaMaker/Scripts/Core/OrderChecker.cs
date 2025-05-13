using UnityEngine;
using PixelCrushers.DialogueSystem;
using PizzaMaker;
using Reflex.Attributes;

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
        return orderFulFillManager.IsOrderFulfilled(customerName);
    }

}