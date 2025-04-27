using System;
using PrimeTween;
using UnityEngine;

namespace PizzaMaker
{
    public class DrinkCup : Interactable, IGrabbable
    {
        public GrabbableState CurrentGrabbableState { get; set; }
        [SerializeField] private Transform drinkMeshTransform;
        private Sequence fillTween;
        private bool isFilled;

        public void FillDrink(Action onComplete = null)
        {
            if (isFilled || fillTween.isAlive)
                return;
            
            drinkMeshTransform.gameObject.SetActive(true);
            fillTween = Tween.LocalPositionY(drinkMeshTransform, -0.01f, 0.275f, 4f)
                .Group(Tween.Scale(drinkMeshTransform, 0.52f, 0.85f, 4f))
                .OnComplete(() =>
                {
                    isFilled = true;
                    onComplete?.Invoke();
                });
        }

        public override void OnClick(PlayerController playerController)
        {
            if (fillTween.isAlive)
                return;
            
            playerController.Grab<DrinkCup>(this);
        }

        public override void OnHover(PlayerController playerController)
        {
            if (CurrentGrabbableState == GrabbableState.None)
            {
                usable.overrideUseMessage = $"<sprite name=\"lmb\">Grab Cup";
            } 
        }

        public override void OnUnhover(PlayerController playerController)
        {
            
        }


        public T GetGrabbableObject<T>() where T : MonoBehaviour, IGrabbable
        {
            if (CurrentGrabbableState != GrabbableState.None) return this as T;
            var instantiatedDrink = Instantiate(this);
            instantiatedDrink.Collider.enabled = false;
            instantiatedDrink.GetComponent<IGrabbable>();
            return instantiatedDrink as T;

        }

        public void OnGrab(PlayerController playerController)
        {
            CurrentGrabbableState = GrabbableState.Grabbed;
        }

        public void OnGrabUsed(PlayerController playerController)
        {
            
        }

        public void OnRelease(PlayerController playerController)
        {
            Destroy(gameObject);
        }

        public void AttachedTo(Transform parentTransform)
        {
            transform.SetParent(parentTransform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            Collider.enabled = true;
            gameObject.SetGameLayerRecursive(GlobalVars.LayerDefault);
            CurrentGrabbableState = GrabbableState.Placed;
        }
    }
}