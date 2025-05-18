using PixelCrushers.DialogueSystem.Wrappers;
using Reflex.Attributes;
using UnityEngine;

namespace PizzaMaker
{
    public class DrinkCupReadyPlacement : Interactable
    {
        [SerializeField] private Mesh cupMesh;
        [SerializeField] private Material cupMaterial;
        [SerializeField] private Material invalidCupMaterial;
        [SerializeField] private ScriptableEventIGrabbable iGrabbableEvent;
        [Inject] private OrderFulFillManager _orderFulFillManager;
        private bool isFailPlacement;
        private Vector3 cupPosition;

        protected override void Awake()
        {
            base.Awake();
            iGrabbableEvent.OnRaised += OnGrab;
        }

        private void OnDestroy()
        {
            iGrabbableEvent.OnRaised -= OnGrab;
        }

        private void OnGrab(IGrabbable grabbable)
        {
            if (grabbable.GetGrabbableObject<DrinkCup>() is not { } drinkCup) return;
            _orderFulFillManager.RemoveItem(drinkCup);
        }

        public override void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (!isFailPlacement && playerController.CurrentIGrabbable?.GetGrabbableObject<DrinkCup>() is { } drinkCup)
            {
                drinkCup.AttachedTo(transform);
                drinkCup.CurrentGrabbableState = GrabbableState.Placed;
                drinkCup.transform.position = cupPosition;
                drinkCup.Collider.enabled = true;
                playerController.UnGrab();
                _orderFulFillManager.AddItem(drinkCup);
            }
        }

        public override void OnHover(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (playerController.CurrentIGrabbable?.GetGrabbableObject<DrinkCup>() is { IsFilled: true } drinkCup)
            {
                var boxCollider = drinkCup.Collider as BoxCollider;
                var colliders = new Collider[10];
                var hitCount = Physics.OverlapBoxNonAlloc(raycastHit.point, boxCollider.size, colliders, Quaternion.identity);

                if (hitCount > 0)
                {
                    foreach (Collider hitCollider in colliders)
                    {
                        isFailPlacement = false;
                        if (hitCollider && hitCollider.TryGetComponent(out DrinkCup _))
                        {
                            if (invalidCupMaterial.SetPass(0))
                            {
                                var meshFilters = drinkCup.GetComponentsInChildren<MeshFilter>();
                                foreach (var meshFilter in meshFilters)
                                {
                                    var localPositionY = meshFilter.transform.localPosition.y > 0 ? meshFilter.transform.localPosition.y - 0.08f : 0;
                                    Matrix4x4 tfMatrix4X4 = Matrix4x4.TRS(raycastHit.point + new Vector3(0, localPositionY, 0), Quaternion.identity, meshFilter.transform.localScale);
                                    Graphics.DrawMesh(meshFilter.mesh, tfMatrix4X4, invalidCupMaterial, 0);
                                }
                            }

                            usable.overrideUseMessage = " ";
                            usable.enabled = false;
                            isFailPlacement = true;
                            break;
                        }
                    }
                }

                if (!isFailPlacement && cupMaterial.SetPass(0))
                {
                    var meshFilters = drinkCup.GetComponentsInChildren<MeshFilter>();
                    foreach (var meshFilter in meshFilters)
                    {
                        var localPositionY = meshFilter.transform.localPosition.y > 0 ? meshFilter.transform.localPosition.y - 0.08f : 0;
                        Matrix4x4 tfMatrix4X4 = Matrix4x4.TRS(raycastHit.point + new Vector3(0, localPositionY, 0), Quaternion.identity, meshFilter.transform.localScale);
                        Graphics.DrawMesh(meshFilter.mesh, tfMatrix4X4, cupMaterial, 0);
                    }
                    
                    usable.overrideUseMessage = "<sprite name=\"lmb\"> Place";
                    StandardUISelectorElements.instance.useMessageText.text = usable.overrideUseMessage;
                    usable.enabled = true;
                    cupPosition = raycastHit.point;
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