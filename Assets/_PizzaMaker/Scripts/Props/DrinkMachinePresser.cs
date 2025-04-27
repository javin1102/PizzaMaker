using Reflex.Attributes;
using UnityEngine;

namespace PizzaMaker
{
    public class DrinkMachinePresser : Interactable
    {
        [SerializeField] private GameObject flowGameObject;
        [Inject] private DrinkMachine drinkMachine { get; set; }
        public override void OnClick(PlayerController playerController)
        {
            var attachment = drinkMachine.DrinkPairs[this];
            if (attachment.DrinkCup != null)
            {
                flowGameObject.gameObject.SetActive(true);
                attachment.DrinkCup.FillDrink(()=>flowGameObject.gameObject.SetActive(false));
            }
        }

        public override void OnHover(PlayerController playerController)
        {
            
        }

        public override void OnUnhover(PlayerController playerController)
        {
            
        }

    }
}