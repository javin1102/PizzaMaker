using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace PizzaMaker
{
    public class PizzaTray : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform attachPoint;
        [SerializeField] private Mesh mesh;
        [SerializeField] private Material mat;
        private PizzaDough placedPizzaDough;

        public bool IsInteractable { get; set; }


        public void OnClick(PlayerController playerController)
        {
            if (playerController.CurrentIGrabbable == null || playerController.CurrentIGrabbable.GetGrabbableObject<PizzaDough>() is not PizzaDough grabbedPizzaDough)
                return;

            grabbedPizzaDough.transform.parent = attachPoint;
            grabbedPizzaDough.transform.localPosition = Vector3.zero;
            grabbedPizzaDough.transform.rotation = Quaternion.identity;
            grabbedPizzaDough.Collider.enabled = true;
            grabbedPizzaDough.CurrentState = PizzaDough.State.Placed;
            placedPizzaDough = grabbedPizzaDough;
            playerController.UnGrab();
        }

        public void OnHover(PlayerController playerController)
        {
            if (playerController.CurrentIGrabbable == null)
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