using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PizzaMaker
{
    public class OrderFulFillManager : MonoBehaviour
    {
        private readonly Dictionary<string, NPCOrder> npcOrders = new();
        private readonly Dictionary<MenuType, List<OrderItem>> orderedItems = new();

        public void AddNPCOrder(NPCOrder npcOrder)
        {
            npcOrders.Add(npcOrder.CustomerName, npcOrder);
        }
        
        public void RemoveNPCOrder(NPCOrder npcOrder)
        {
            npcOrders.Remove(npcOrder.CustomerName);
        }

        public void AddItem(OrderItem orderItem)
        {
            if(orderItem.MenuType == null) 
                return;
            if (!orderedItems.TryGetValue(orderItem.MenuType.Value, out var itemList))
                orderedItems.Add(orderItem.MenuType.Value, new List<OrderItem>() { orderItem });
            else
                itemList.Add(orderItem);
        }

        public void RemoveItem(OrderItem orderItem)
        {
            if (orderItem.MenuType == null)
                return;
            if (!orderedItems.TryGetValue(orderItem.MenuType.Value, out var itemList)) return;
            if (itemList.Remove(orderItem) && itemList.Count <= 0)
                orderedItems.Remove(orderItem.MenuType.Value);
        }

        public bool IsOrderFulfilled(string customerName)
        {
            return IsOrderFulfilled(npcOrders[customerName].OrderMenus);
        }

        public bool IsOrderFulfilled(List<OrderMenu> orderMenus)
        {
            foreach (var orderMenu in orderMenus)
            {
                if (!orderedItems.TryGetValue(orderMenu.menuType, out var itemList))
                    return false;

                if (orderMenu.extraToppings is { Count: > 0 })
                {
                    var containsAllExtraTopping = false;
                    foreach (var item in itemList)
                    {
                        if (item is not PizzaBox pizzaBox)
                            continue;


                        containsAllExtraTopping = pizzaBox.PizzaCooked.ExtraToppingList is { Count: > 0 } &&
                                                  pizzaBox.PizzaCooked.ExtraToppingList.All(topping => orderMenu.extraToppings.Contains(topping)) &&
                                                  pizzaBox.PizzaCooked.ExtraToppingList.Count == orderMenu.extraToppings.Count;
                        
                        if (containsAllExtraTopping)
                            break;
                    }

                    if (!containsAllExtraTopping)
                        return false;
                }
                else
                {
                    var quantityNotMatch = itemList.Any(item => item is PizzaBox pizzaBox && pizzaBox.PizzaCooked.ExtraToppingList is { Count: > 0 });
                    if (quantityNotMatch)
                        return false;
                }
            }

            return true;
        }
    }
}