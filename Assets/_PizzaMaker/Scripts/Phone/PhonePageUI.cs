using UnityEngine;

namespace PizzaMaker
{
    public abstract class PhonePageUI : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup;
        public bool IsShowing { get; private set; }

        public virtual void Show()
        {
            IsShowing = true;
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        
        public virtual void Hide()
        {
            IsShowing = false;
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}