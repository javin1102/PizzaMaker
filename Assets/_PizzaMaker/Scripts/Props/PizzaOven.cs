using PixelCrushers.DialogueSystem;
using PrimeTween;
using Reflex.Attributes;
using UnityEngine;

namespace PizzaMaker
{
    public class PizzaOven : Interactable
    {
        public PizzaDough PizzaDough => pizzaAttachTransform.GetComponentInChildren<PizzaDough>();

        public enum State
        {
            Closed,
            Baking,
            Opened
        }

        public State CurrentState { get; set; }

        private const string OpenCoverMessage = "<sprite name=\"lmb\">Open";
        private const string CloseCoverMessage = "<sprite name=\"lmb\">Close";
        // private const string BakePizzaMessage = "<sprite name=\"lmb\">Bake Pizza";

        [SerializeField] private Transform pizzaOvenCoverTransform;
        [SerializeField] private Transform pizzaAttachTransform;
        [SerializeField] private Material pizzaTransparentMaterial;
        [SerializeField] private Mesh pizzaMesh;
        [SerializeField] private PizzaOvenButton pizzaOvenButton;
        [SerializeField] private Collider boxCollider;
        [Inject] private PizzaMakingManager pizzaMakingManager;
        private Tween tweenCover;
        protected override void Awake()
        {
            base.Awake();
            pizzaOvenButton.OnBakePizza += OnPizzaBaked;
        }

        private void OnPizzaBaked(PizzaCooked obj)
        {
            foreach (Transform pizza in pizzaAttachTransform)
                Destroy(pizza.gameObject);
            
            obj.AttachedTo(pizzaAttachTransform);
        }

        public void OpenCover()
        {
            if (CurrentState == State.Baking)
                return;
            
            if (CurrentState == State.Opened || tweenCover.isAlive)
                return;

            tweenCover = Tween.LocalRotation(pizzaOvenCoverTransform, Vector3.zero, Vector3.right * 90, 0.5f, Ease.OutQuad);
            boxCollider.enabled = false;
            CurrentState = State.Opened;
        }

        public void CloseCover()
        {
            if (CurrentState == State.Baking)
                return;
            
            if (CurrentState == State.Closed || tweenCover.isAlive)
                return;

            boxCollider.enabled = true;
            tweenCover = Tween.LocalRotation(pizzaOvenCoverTransform, Vector3.right * 90, Vector3.zero, 0.5f, Ease.OutQuad);
            CurrentState = State.Closed;
        }

        public void ToggleCover()
        {
            if (CurrentState == State.Opened)
                CloseCover();
            else
                OpenCover();
        }

        public override void OnClick(PlayerController playerController)
        {
            if (CurrentState == State.Baking)
                return;
            
            if (CurrentState == State.Closed)
            {
                OpenCover();
            }

            else
            {
                if (playerController.CurrentIGrabbable is PizzaDough pizzaDough)
                {
                    if (pizzaAttachTransform.childCount <= 0)
                    {
                        pizzaDough.AttachedTo(pizzaAttachTransform);
                        playerController.UnGrab();
                    }
                }
                else
                {
                    CloseCover();
                }
            }
        }

        public override void OnHover(PlayerController playerController)
        {
            if (CurrentState == State.Baking)
            {
                IsInteractable = false;
                return;
            }

            IsInteractable = true;
            if (CurrentState == State.Closed)
            {
                usable.overrideUseMessage = OpenCoverMessage;
            }
            else
            {
                if (playerController.CurrentIGrabbable is PizzaDough { })
                {
                    if (pizzaAttachTransform.childCount <= 0)
                    {
                        pizzaTransparentMaterial.SetPass(0);
                        Graphics.DrawMesh(pizzaMesh, pizzaAttachTransform.position, pizzaAttachTransform.rotation, pizzaTransparentMaterial, 0);
                        usable.enabled = false;
                        return;
                    }
                }

                usable.overrideUseMessage = CloseCoverMessage;
            }

            usable.enabled = true;
            StandardUISelectorElements.instance.useMessageText.text = usable.overrideUseMessage;
            // Debug.LogError("IsInteractable: " + usable.overrideUseMessage);
            // IsInteractable = playerController.CurrentIGrabbable is PizzaDough;
        }

        public override void OnUnhover(PlayerController playerController)
        {
        }
    }
}