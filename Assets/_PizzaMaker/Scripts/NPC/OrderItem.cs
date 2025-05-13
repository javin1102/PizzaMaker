using UnityEngine;

namespace PizzaMaker
{
    public abstract class OrderItem : Interactable
    {
        public virtual MenuType? MenuType { get; set; }
    }
}