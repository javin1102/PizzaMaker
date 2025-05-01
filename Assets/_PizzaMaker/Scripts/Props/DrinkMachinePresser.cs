using PrimeTween;
using Reflex.Attributes;
using UnityEngine;

namespace PizzaMaker
{
    public class DrinkMachinePresser : Interactable
    {
        private static readonly int MaskControl = Shader.PropertyToID("_MaskControl");
        private static readonly int FlowMode = Shader.PropertyToID("_FlowMode");
        [SerializeField] private GameObject flowGameObject;
        [SerializeField] private Material flowSharedMaterial;
        [SerializeField] private Color drinkColor;
        [SerializeField] private MenuItem drinkMenuType;
        [Inject] private DrinkMachine drinkMachine { get; set; }
        private DrinkMachineAttachment attachment;
        private Sequence tweenFlow;

        private void Start()
        {
            IsInteractable = false;
            usable.overrideUseMessage = "<sprite name=\"lmb\"> Fill Cup";
        }

        public override void OnClick(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (attachment.DrinkCup != null && !tweenFlow.isAlive && !attachment.DrinkCup.IsFilled)
            {
                attachment.DrinkCup.ChangeColor(drinkColor);
                flowSharedMaterial.SetInt(FlowMode, 0);
                tweenFlow = Sequence.Create();
                flowGameObject.gameObject.SetActive(true);
                attachment.DrinkCup.FilledDrink = drinkMenuType;
                tweenFlow.Chain(Tween.Custom(0f, 1f, 0.65f, val => flowSharedMaterial.SetFloat(MaskControl, val)))
                    .Chain(
                        attachment.DrinkCup.FillDrink(
                            onStart: () =>
                            {
                                IsInteractable = false;
                            }
                        )
                    ).ChainCallback(() => flowSharedMaterial.SetInt(FlowMode, 1))
                    .Chain(Tween.Custom(0f, 1f, 0.35f, val => flowSharedMaterial.SetFloat(MaskControl, val)))
                    .OnComplete(() => { flowGameObject.gameObject.SetActive(false); });
            }
        }

        public override void OnHover(PlayerController playerController, ref RaycastHit raycastHit)
        {
            if (!attachment)
                attachment = drinkMachine.DrinkPairs[this];

            IsInteractable = attachment.DrinkCup != null && !attachment.DrinkCup.FillTween.isAlive && !attachment.DrinkCup.IsFilled;
        }

        public override void OnUnhover(PlayerController playerController)
        {
        }
    }
}