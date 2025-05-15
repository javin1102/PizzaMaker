using PrimeTween;
using TMPro;
using UnityEngine;

namespace PizzaMaker
{
    public class AdditionalInformationUI : MonoBehaviour
    {
        public bool IsVisible => gameObject.activeInHierarchy;
        [SerializeField] private TMP_Text additionalInformationText;
        [SerializeField] private GameObject visualGameObject;
        [SerializeField] private CanvasGroup canvasGroup;

        public void SetText(string text)
        {
            additionalInformationText.text = text;
        }

        public void FadeIn()
        {
            gameObject.SetActive(true);
            Tween.Alpha(canvasGroup, 0f, 1f, 0.1f);
        }

        public void FadeOut()
        {
            Tween.Alpha(canvasGroup, 0f, 0.03f).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}