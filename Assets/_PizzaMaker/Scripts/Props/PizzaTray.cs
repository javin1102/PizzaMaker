using UnityEngine;

namespace PizzaMaker
{
    public class PizzaTray : MonoBehaviour, IInteractable
    {
        public bool IsInteractable { get; set; }
        [SerializeField] private Transform attachPoint;
        [SerializeField] private Mesh mesh;
        [SerializeField] private Material mat;

        public void OnClick(PlayerController playerController)
        {
            if (playerController.CurrentIGrabbable?.GetGrabbableObject<PizzaDough>() is not { } grabbedPizzaDough || attachPoint.childCount > 0)
                return;

            grabbedPizzaDough.AttachedTo(attachPoint);
            playerController.UnGrab();
        }

        public void OnHover(PlayerController playerController)
        {
            if (playerController.CurrentIGrabbable?.GetGrabbableObject<PizzaDough>() is null || attachPoint.childCount > 0)
                return;

            if (mat.SetPass(0))
            {
                Graphics.DrawMesh(mesh, attachPoint.position, attachPoint.rotation, mat, 0);
            }
        }

        public void OnUnhover(PlayerController playerController)
        {
        }
    }
}