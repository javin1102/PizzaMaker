using UnityEngine;

namespace PizzaMaker
{
    public abstract class OrderItem : Interactable
    {
        public virtual Menu? MenuType { get; set; }
    }
}