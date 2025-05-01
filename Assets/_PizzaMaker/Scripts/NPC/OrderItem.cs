using UnityEngine;

namespace PizzaMaker
{
    public abstract class OrderItem : Interactable
    {
        public MenuType MenuType { get; set; }
    }
}