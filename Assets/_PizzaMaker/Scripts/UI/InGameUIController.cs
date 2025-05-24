using Obvious.Soap;
using PixelCrushers.DialogueSystem;
using PizzaMaker.Tools;
using PrimeTween;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace PizzaMaker
{
    public class InGameUIController : SceneSingleton<InGameUIController>
    {
        [SerializeField] private InformationUI topRightInformationUI, bottomLeftInformationUI; // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] private Image imageOverlay, imageOverlayScreenSpace;
        [SerializeField] private Canvas canvasOverlay, canvasScreenSpace;
        [SerializeField] private AdditionalInformationUI additionalInformationUI;
        private int overlayCount = 0;
        private int overlayScreenSpaceCount = 0;

        protected override void Awake()
        {
            base.Awake();
            topRightInformationUI.gameObject.SetActive(false);
            additionalInformationUI.gameObject.SetActive(false);
            bottomLeftInformationUI.gameObject.SetActive(false);
        }

        private void Start()
        {
            canvasScreenSpace.worldCamera = Camera.main;
            canvasScreenSpace.planeDistance = 0.35f;
        }

        public void ShowQuestInformationUI(string text, QuestId questId)
        {
            topRightInformationUI.FadeIn();
            topRightInformationUI.SetText(text);
            topRightInformationUI.CurrentQuestId = questId;
        }
        
        private void OnEnable()
        {
            GameEvents.OnQuestStateChanged += OnQuestStateChange;
            GameEvents.OnGrab += OnGrab;
            GameEvents.OnUnGrab += UnGrab;
            Focusable.OnFocus += OnFocus;
            Focusable.OnOutFocus += OnOutFocus;

        }


        private void OnDisable()
        {
            GameEvents.OnQuestStateChanged -= OnQuestStateChange;
            GameEvents.OnGrab -= OnGrab;
            GameEvents.OnUnGrab -= UnGrab;
            Focusable.OnFocus -= OnFocus;
            Focusable.OnOutFocus -= OnOutFocus;
        }

        private void UnGrab()
        {
            bottomLeftInformationUI.FadeOut(0.05f);
        }

        private void OnGrab(IGrabbable iGrabbable)
        {
            bottomLeftInformationUI.gameObject.SetActive(true);
            bottomLeftInformationUI.FadeIn(0.05f);
            bottomLeftInformationUI.SetText("<sprite name=\"Q\">Discard Item");
        }
        
        private void OnOutFocus()
        {
            bottomLeftInformationUI.FadeOut(0.05f);
        }

        private void OnFocus()
        {
            bottomLeftInformationUI.gameObject.SetActive(true);
            bottomLeftInformationUI.FadeIn(0.05f);
            bottomLeftInformationUI.SetText("<sprite name=\"Q\">Exit");
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
        
        private void OnQuestStateChange(QuestId quest, QuestState state)
        {
            if(string.IsNullOrEmpty(topRightInformationUI.CurrentQuestId))
                return;
            
            if (quest != topRightInformationUI.CurrentQuestId)
                return;

            if (state == QuestState.Success)
            {
                Tween.Delay(2, topRightInformationUI.FadeOut);
                topRightInformationUI.CurrentQuestId = "";
            }
        }
    }
}