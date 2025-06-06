﻿using System;
using Reflex.Attributes;
using UnityEngine;

namespace PizzaMaker
{
    public class Focusable : MonoBehaviour
    {
        private Vector3 originalPosition;
        private Vector3 originalRotation;

        public static Action OnFocus { get; set; }
        public static Action OnOutFocus { get; set; }
        [Inject] private PlayerController playerController;
        private void Awake()
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation.eulerAngles;
        }

        public void Focus()
        {
            // transform.LookAt(Camera.main!.transform.position, Vector3.up);
            transform.position = Camera.main.transform.position + Camera.main.transform.forward.normalized * 0.7f;
            transform.forward = -Camera.main.transform.forward;
            gameObject.SetGameLayerRecursive(GlobalVars.LayerFocus);
            InGameUIController.Instance.ShowOverlayScreenSpace();
            playerController.SetMovement(false);
            playerController.SetFocusable(this);
            Cursor.lockState = CursorLockMode.None;
            OnFocus?.Invoke();
        }

        public void OutFocus()
        {
            transform.position = originalPosition;
            transform.rotation = Quaternion.Euler(originalRotation);
            gameObject.SetGameLayerRecursive(GlobalVars.LayerDefault);
            InGameUIController.Instance.HideOverlayScreenSpace();
            playerController.SetMovement(true);
            OnOutFocus?.Invoke();
        }
    }
    
}
