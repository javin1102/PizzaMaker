using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZLinq;

namespace PizzaMaker
{
    public class OrderFulFillManager : MonoBehaviour
    {
        private readonly Dictionary<string, NPCOrder> npcOrders = new();
        private readonly Dictionary<Menu, List<OrderItem>> orderedItems = new();

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
            if (orderItem.MenuType == null)
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

        public bool CanFulFillOrder(string customerName)
        {
            return npcOrders.TryGetValue(customerName, out NPCOrder order) && CanFulFillOrder(order.OrderMenus);
        }

        public void FulFillOrder(string customerName)
        {
            if (!npcOrders.TryGetValue(customerName, out NPCOrder npcOrder))
                return;

            var matchingItems = GetMatchingOrderItems(npcOrder);
            for (int i = 0; i < matchingItems.Count; i++)
            {
                var matchingItem = matchingItems[i];
                RemoveItem(matchingItem);
                Destroy(matchingItem.gameObject);
            }

            npcOrders.Remove(customerName);
        }

        public List<OrderItem> GetMatchingOrderItems(NPCOrder npcOrder)
        {
            List<OrderItem> matchingItems = new();

            foreach (var orderMenu in npcOrder.OrderMenus)
            {
                if (!orderedItems.TryGetValue(orderMenu.menuType, out var itemList))
                    continue;

                foreach (var item in itemList)
                {
                    if (orderMenu.extraToppings is { Count: > 0 })
                    {
                        if (item is PizzaBox pizzaBox)
                        {
                            bool extraToppingsMatch = pizzaBox.PizzaCooked.ExtraToppingList is { Count: > 0 } &&
                                                      pizzaBox.PizzaCooked.ExtraToppingList.All(topping => orderMenu.extraToppings.Contains(topping)) &&
                                                      pizzaBox.PizzaCooked.ExtraToppingList.Count == orderMenu.extraToppings.Count;

                            if (extraToppingsMatch)
                            {
                                matchingItems.Add(item);
                                break; // Found matching pizza for this order menu
                            }
                        }
                    }
                    else
                    {
                        if (item is PizzaBox pizzaBox)
                        {
                            if (pizzaBox.PizzaCooked.ExtraToppingList is { Count: <= 0 })
                            {
                                matchingItems.Add(item);
                                break; // Found matching pizza for this order menu
                            }
                        }
                        else
                        {
                            matchingItems.Add(item);
                            break; // Found matching item for this order menu
                        }
                    }
                }
            }

            return matchingItems;
        }

        public bool CanFulFillOrder(List<OrderMenu> orderMenus)
        {
            var orderFulfilled = 0;
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
                        {
                            orderFulfilled++;
                            if (orderFulfilled == orderMenus.Count)
                                return true;
                            break; // Found matching pizza for this order menu
                        }
                    }

                    if (!containsAllExtraTopping)
                        return false;
                }
                else
                {
                    var quantityMatch = itemList.Any(item => item is PizzaBox pizzaBox && pizzaBox.PizzaCooked.ExtraToppingList is { Count: <= 0 });
                    if (quantityMatch)
                    {
                        orderFulfilled++;
                        if (orderFulfilled == orderMenus.Count)
                            return true;
                    }
                }
            }

            return true;
        }
    }
}