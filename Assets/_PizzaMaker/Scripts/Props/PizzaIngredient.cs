using UnityEngine;

namespace PizzaMaker
{
    public class PizzaIngredient : MonoBehaviour, IGrabbable
    {
         public string IngredientType { get; set; }
        
        public T GetGrabbableObject<T>() where T : MonoBehaviour, IGrabbable
        {
            var instantiatedIngredient = Instantiate(this);
            instantiatedIngredient.IngredientType = IngredientType;
            return instantiatedIngredient as T;
        }

        public void OnGrab(PlayerController playerController)
        {
            
        }

        public void OnGrabUsed(PlayerController playerController)
        {
            
        }

        public void OnRelease(PlayerController playerController)
        {
            Destroy(gameObject);
        }
    }
}