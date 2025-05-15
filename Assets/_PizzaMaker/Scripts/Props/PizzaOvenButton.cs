using System;
using PrimeTween;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace PizzaMaker
{
    public class PizzaOvenButton : Interactable
    {
        private const float DefaultBakeTime = 5f;
        [SerializeField] private TMP_Text bakeTimeText;
        private PizzaOven pizzaOven;
        private Collider _collider;
        [Inject] private PizzaMakingManager pizzaMakingManager;
        private float currentBakeTime;
        public Action<PizzaCooked> OnBakePizza { get; set; }

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider>();
            pizzaOven = GetComponentInParent<PizzaOven>();
            usable.overrideUseMessage = "<sprite name=\"lmb\">Bake Pizza";
            bakeTimeText.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (pizzaOven.CurrentState != PizzaOven.State.Baking)
                return;

            currentBakeTime -= Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentBakeTime);
            bakeTimeText.text = time.ToString(@"mm\:ss");
            if (currentBakeTime <= 0)
            {
                pizzaOven.CurrentState = PizzaOven.State.Closed;
                currentBakeTime = 0;
                var cookedPizzaRef = pizzaMakingManager.BakePizza(pizzaOven.PizzaDough);
                OnBakePizza?.Invoke(cookedPizzaRef);
                Tween.Delay(0.5f, () => bakeTimeText.gameObject.SetActive(false));
            }
        }

        public override void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (!IsInteractable)
                return;

            if (pizzaOven.CurrentState == PizzaOven.State.Closed)
            {
                pizzaOven.CurrentState = PizzaOven.State.Baking;
                currentBakeTime = DefaultBakeTime;
                bakeTimeText.gameObject.SetActive(true);
            }
        }

        public override void OnHover(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (pizzaOven.PizzaDough == null || pizzaOven.CurrentState == PizzaOven.State.Baking || pizzaOven.CurrentState == PizzaOven.State.Opened)
                IsInteractable = false;
            else
                IsInteractable = true;
        }

        public override void OnUnhover(PlayerController playerController)
        {
        }
    }
}