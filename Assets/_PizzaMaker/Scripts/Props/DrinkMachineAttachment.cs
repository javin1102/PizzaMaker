using PixelCrushers.DialogueSystem.Wrappers;
using Reflex.Attributes;
using UnityEngine;

namespace PizzaMaker
{
    public class DrinkMachineAttachment : Interactable
    {
        public DrinkCup DrinkCup => GetComponentInChildren<DrinkCup>();

        [SerializeField] private Mesh cupMesh;
        [SerializeField] private Material cupMaterial;
        [SerializeField] private Material invalidCupMaterial;
        
        [Inject] private DrinkMachine drinkMachine;
        [Inject] private PlayerController playerController;
        private bool isFailPlacement;

        protected override void Awake()
        {
            base.Awake();
            Collider.enabled = false;
        }

        public override void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (!isFailPlacement && playerController.CurrentIGrabbable?.GetGrabbableObject<DrinkCup>() is { } drinkCup)
            {
                drinkCup.AttachedTo(transform);
                drinkCup.CurrentGrabbableState = GrabbableState.Placed;
                drinkCup.Collider.enabled = true;
                playerController.UnGrab();
            }
        }

        private void Update()
        {
            if (playerController is null)
                return;
            
            Collider.enabled = transform.childCount <= 0 && playerController.CurrentIGrabbable?.GetGrabbableObject<DrinkCup>() is not null;
            
        }

        public override void OnHover(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (playerController.CurrentIGrabbable?.GetGrabbableObject<DrinkCup>() is { } drinkCup)
            {
                var boxCollider = drinkCup.Collider as BoxCollider;
                Matrix4x4 tfMatrix4X4 = Matrix4x4.TRS(transform.position, transform.rotation, new Vector3(1, 0.75f, 1f));
                var colliders = new Collider[10];
                var hitCount = Physics.OverlapBoxNonAlloc(transform.position, boxCollider.size , colliders, Quaternion.identity );

                if (hitCount > 0)
                {
                    foreach (Collider hitCollider in colliders)
                    {
                        isFailPlacement = false;
                        if (hitCollider && hitCollider.TryGetComponent(out DrinkCup _))
                        {
                            if (invalidCupMaterial.SetPass(0))
                                Graphics.DrawMesh(cupMesh, tfMatrix4X4, invalidCupMaterial, 0);

                            usable.overrideUseMessage = " ";
                            usable.enabled = false;
                            isFailPlacement = true;
                            break;
                        }
                    }
                }

                if (!isFailPlacement && cupMaterial.SetPass(0))
                {
                    Graphics.DrawMesh(cupMesh, tfMatrix4X4, cupMaterial, 0);
                    usable.overrideUseMessage = "<sprite name=\"lmb\"> Place";
                    StandardUISelectorElements.instance.useMessageText.text = usable.overrideUseMessage;
                    usable.enabled = true;
                }

                IsInteractable = true;
            }
            else
            {
                IsInteractable = false;
            }
        }

        public override void OnUnhover(PlayerController playerController)
        {
        }

    }
}