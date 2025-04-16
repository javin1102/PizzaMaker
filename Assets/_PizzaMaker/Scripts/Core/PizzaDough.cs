using UnityEngine;

namespace PizzaMaker
{
    public class PizzaDough : Interactable, IGrabbable
    {
        public Collider Collider => _collider;
        private Collider _collider;

        public enum State
        {
            None,
            Placed,
            Grabbed
        }

        public State CurrentState { get; set; } = State.None;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider>();
        }


        public override void OnClick(PlayerController playerController)
        {
            if (!IsInteractable)
                return;

            playerController.Grab<PizzaDough>(this);
        }

        public override void OnHover(PlayerController playerController)
        {
            IsInteractable = playerController.CurrentIGrabbable == null;
        }

        public override void OnUnhover(PlayerController playerController)
        {
        }


        public Component GetGrabbableObject<T>() where T : MonoBehaviour, IGrabbable
        {
            if (CurrentState != State.None)
            {
                return this;
            }

            var grabbableObject = Instantiate(gameObject).GetComponent<PizzaDough>();
            grabbableObject.Collider.enabled = false;
            grabbableObject.GetComponent<IGrabbable>();
            return grabbableObject;
        }

        public void OnGrab(PlayerController playerController)
        {
            CurrentState = State.Grabbed;
        }


        public void OnGrabUsed(PlayerController playerController)
        {
        }

        public void OnRelease(PlayerController playerController)
        {
            Destroy(gameObject);
            IsInteractable = playerController.CurrentIGrabbable == null;
        }
    }
}