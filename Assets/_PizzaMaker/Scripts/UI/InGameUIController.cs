using PizzaMaker.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace PizzaMaker
{
    public class InGameUIController : SceneSingleton<InGameUIController>
    {
        [SerializeField] private InformationUI informationUI; // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] private Image imageOverlay, imageOverlayScreenSpace;
        [SerializeField] private Canvas canvasOverlay, canvasScreenSpace;
        [SerializeField] private AdditionalInformationUI additionalInformationUI;
        private int overlayCount = 0;
        private int overlayScreenSpaceCount = 0;

        protected override void Awake()
        {
            base.Awake();
            informationUI.gameObject.SetActive(false);
            additionalInformationUI.gameObject.SetActive(false);
        }

        private void Start()
        {
            canvasScreenSpace.worldCamera = Camera.main;
            canvasScreenSpace.planeDistance = 0.35f;
        }

        public void ShowQuestInformationUI(string text, QuestId questId)
        {
            informationUI.FadeIn();
            informationUI.SetText(text);
            informationUI.SetQuest(questId);
        }

        public void ShowAdditionalInformationUI(string text)
        {
            additionalInformationUI.SetText(text);

            if (additionalInformationUI.IsVisible)
                return;

            additionalInformationUI.FadeIn();
        }

        public void HideAdditionalInformationUI()
        {
            if (!additionalInformationUI.IsVisible)
                return;

            additionalInformationUI.FadeOut();
        }

        public void ShowOverlay()
        {
            overlayCount++;
            imageOverlay.gameObject.SetActive(true);
            imageOverlay.CrossFadeAlpha(1, 0.25f, true);
        }

        public void HideOverlay()
        {
            overlayCount--;
            if (overlayCount <= 0)
            {
                overlayCount = 0;
                imageOverlay.CrossFadeAlpha(0, 0.25f, true);
                imageOverlay.gameObject.SetActive(false);
            }
        }

        public void ShowOverlayScreenSpace()
        {
            overlayScreenSpaceCount++;
            imageOverlayScreenSpace.gameObject.SetActive(true);
            imageOverlayScreenSpace.CrossFadeAlpha(1, 0.25f, true);
        }

        public void HideOverlayScreenSpace()
        {
            overlayScreenSpaceCount--;
            if (overlayScreenSpaceCount <= 0)
            {
                overlayScreenSpaceCount = 0;
                imageOverlayScreenSpace.CrossFadeAlpha(0, 0.25f, true);
                imageOverlayScreenSpace.gameObject.SetActive(false);
            }
        }


        public void HideInformationUI()
        {
            informationUI.FadeOut();
        }
    }
}