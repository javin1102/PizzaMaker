using UnityEngine;

namespace PizzaMaker
{
    public abstract class PhonePageUI : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup;

        public virtual void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        
        public virtual void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}