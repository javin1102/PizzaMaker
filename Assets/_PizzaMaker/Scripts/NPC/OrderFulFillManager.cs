using System.Collections.Generic;
using Reflex.Extensions;
using Reflex.Injectors;
using UnityEngine;

namespace PizzaMaker
{
    public class OrderFulFillManager : MonoBehaviour
    {
        private readonly Dictionary<MenuType, List<OrderItem>> orderedItems = new();

        private void Awake()
        {
            var parentContainer = gameObject.scene.GetSceneContainer();
            var newContainer = parentContainer.Scope(builder =>
            {
                builder.SetName("Order Fulfill");
                builder.AddSingleton(factory: (container) => this);
            });
            GameObjectInjector.InjectRecursive(gameObject, newContainer);
            newContainer.Dispose();
        }

        public void AddItem(OrderItem orderItem)
        {
            if (!orderedItems.TryGetValue(orderItem.MenuType, out var itemList))
                orderedItems.Add(orderItem.MenuType, new List<OrderItem>() { orderItem });
            else
                itemList.Add(orderItem);
        }

        public void RemoveItem(OrderItem orderItem)
        {
            if (!orderedItems.TryGetValue(orderItem.MenuType, out var itemList)) return;
            if (itemList.Remove(orderItem) && itemList.Count <= 0)
                orderedItems.Remove(orderItem.MenuType);
        }
    }
}