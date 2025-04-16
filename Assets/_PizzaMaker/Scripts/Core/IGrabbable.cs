using UnityEngine;

namespace PizzaMaker
{
    public interface IGrabbable
    {
        public GrabbableType GrabbableType { get; }
        public IGrabbable GetGrabbableObject(out GameObject objectToGrab);

        /// <summary>
        /// Called when the object is grabbed
        /// </summary>
        void OnGrab(PlayerController playerController);

        /// <summary>
        /// Called when the object is released
        /// </summary>
        void OnRelease(PlayerController playerController);
    }
}