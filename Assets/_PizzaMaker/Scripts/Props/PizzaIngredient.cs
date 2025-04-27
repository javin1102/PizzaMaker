using UnityEngine;

namespace PizzaMaker
{
    public class PizzaIngredient : MonoBehaviour, IGrabbable
    {
        public GrabbableState CurrentGrabbableState { get; set; } = GrabbableState.None;
         public string IngredientType { get; set; }
        
        public T GetGrabbableObject<T>() where T : MonoBehaviour, IGrabbable
        {
            if (CurrentGrabbableState != GrabbableState.None)
                return this as T;
            
            var instantiatedIngredient = Instantiate(this);
            instantiatedIngredient.IngredientType = IngredientType;
            return instantiatedIngredient as T;
        }

        public void OnGrab(PlayerController playerController)
        {
            CurrentGrabbableState = GrabbableState.Grabbed;
        }

        public void OnGrabUsed(PlayerController playerController)
        {
            
        }

        public void OnRelease(PlayerController playerController)
        {
            Destroy(gameObject);
        }

        public void AttachedTo(Transform parentTransform)
        {
            
        }
    }
}