﻿using System;
using UnityEngine;

namespace PizzaMaker
{
    public interface IGrabbable
    {
        public GrabbableState CurrentGrabbableState { get; set; }
        public T GetGrabbableObject<T>() where T : MonoBehaviour, IGrabbable;

        /// <summary>
        /// Called when the object is grabbed
        /// </summary>
        void OnGrab(PlayerController playerController);
        
        /// <summary>
        /// Called when the object is used
        /// </summary>
        void OnGrabUsed(PlayerController playerController);

        /// <summary>
        /// Called when the object is released
        /// </summary>
        void OnRelease(PlayerController playerController);
        
        void AttachedTo(Transform parentTransform);
    }
}