using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace PizzaMaker
{
    [System.Serializable]
    public class OrderMenu
    {
        [FormerlySerializedAs("menuItem")] public MenuType menuType;
        public int quantity;
    }

    public class NPCOrder : MonoBehaviour
    {
        [field: SerializeField] public List<OrderMenu> OrderMenus { get; private set; }
    }
}
