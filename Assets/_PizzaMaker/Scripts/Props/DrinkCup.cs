using System;
using PixelCrushers.DialogueSystem.Wrappers;
using PrimeTween;
using UnityEngine;

namespace PizzaMaker
{
    public class DrinkCup : OrderItem, IGrabbable
    {
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        public Sequence FillTween => fillTween;
        public bool IsFilled { get; private set; }
        public GrabbableState CurrentGrabbableState { get; set; }
        [SerializeField] private Transform drinkMeshTransform;
        [SerializeField] private Transform drinkLidTransform;
        [SerializeField] private MeshRenderer drinkMeshRenderer;
        private Sequence fillTween;

        public void ChangeColor(Color color)
        {
            var mpb = new MaterialPropertyBlock();
            mpb.SetColor(BaseColor, color);
            drinkMeshRenderer.SetPropertyBlock(mpb);
        }
        
        public Sequence FillDrink(Action onStart = null, Action onComplete = null)
        {
            if (IsFilled || fillTween.isAlive)
                return Sequence.Create();

            IsInteractable = false;
            fillTween = Sequence.Create();
            fillTween.ChainCallback(
                    callback: () =>
                    {
                        onStart?.Invoke();
                        drinkMeshTransform.gameObject.SetActive(true);
                    }
            )
            .Chain(Tween.LocalPositionY(drinkMeshTransform, -0.01f, 0.275f, 4f))
            .Group(Tween.Scale(drinkMeshTransform, 0.52f, 0.85f, 4f))
            .OnComplete(() =>
            {
                IsFilled = true;
                IsInteractable = true;
                onComplete?.Invoke();
            });
            return fillTween;
        }

        public override void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (fillTween.isAlive)
                return;

            playerController.Grab<DrinkCup>(this);
        }

        public override void OnHover(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (CurrentGrabbableState == GrabbableState.None)
                usable.overrideUseMessage = $"<sprite name=\"lmb\">Grab Cup";
            else
            {
                var cupName = IsFilled ? $"{MenuType?.name}" : "Cup";
                usable.overrideUseMessage = $"<sprite name=\"lmb\">Grab {cupName}";
            }

            StandardUISelectorElements.instance.useMessageText.text = usable.overrideUseMessage;

            IsInteractable = !fillTween.isAlive && playerController.CurrentIGrabbable == null;
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
            if (IsFilled)
            {
                drinkMeshTransform.gameObject.SetActive(false);
                drinkLidTransform.gameObject.SetActive(true);
            }
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