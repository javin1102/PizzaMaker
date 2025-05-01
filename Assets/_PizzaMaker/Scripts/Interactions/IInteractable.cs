using UnityEngine;

namespace PizzaMaker
{
    public interface IInteractable
    {
        public bool IsInteractable { get; set; }
        /// <summary>
        /// Called when the object is clicked
        /// </summary>
        void OnClick(PlayerController playerController, ref RaycastHit raycastHit);

        /// <summary>
        /// Called when the object is hovered
        /// </summary>
        void OnHover(PlayerController playerController, ref RaycastHit raycastHit);

        /// <summary>
        /// Called when the object is unhovered
        /// </summary>
        void OnUnhover(PlayerController playerController);
    }
}