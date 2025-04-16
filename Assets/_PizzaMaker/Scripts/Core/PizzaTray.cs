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

        public bool IsInteractable { get; set; }


        public void OnClick(PlayerController playerController)
        {
        }

        public void OnHover(PlayerController playerController)
        {
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