using UnityEngine;

namespace PizzaMaker
{
    public class PizzaDough : Interactable, IGrabbable
    {
        private Collider _collider;
        public GrabbableType GrabbableType => GrabbableType.PizzaDough;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider>();
        }

        public override void OnClick(PlayerController playerController)
        {
            if (!IsInteractable)
                return;
            
            playerController.Grab(this);
            IsInteractable = playerController.GrabbedGameObject == null;
        }

        public override void OnHover(PlayerController playerController)
        {
        }

        public override void OnUnhover(PlayerController playerController)
        {
        }


        public IGrabbable GetGrabbableObject(out GameObject objectToGrab)
        {
            var grabbableObject = Instantiate(gameObject);
            grabbableObject.GetComponent<Collider>().enabled = false;
            objectToGrab = grabbableObject;
            return grabbableObject.GetComponent<IGrabbable>();
        }

        public void OnGrab(PlayerController playerController)
        {
        }

        public void OnRelease(PlayerController playerController)
        {
            Destroy(gameObject);
            IsInteractable = playerController.GrabbedGameObject == null;
        }
    }
}