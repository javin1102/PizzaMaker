using System;
using JetBrains.Annotations;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PizzaMaker
{
    public class PhoneChatPageUI : PhonePageUI, IDialogueUI
    {
        public event EventHandler<SelectedResponseEventArgs> SelectedResponseHandler;
        
        [SerializeField] private Button backButton;
        [SerializeField] private ChatBubbleUI chatBubbleUIOther;
        [SerializeField] private ChatBubbleUI chatBubbleUISelf;
        [SerializeField] private RectTransform chatContainer;
        [SerializeField] private StandardUIMenuPanel standardUIMenuPanel;
        [Inject] private PhoneController phoneController;
        
        private void Awake()
        {
            backButton.onClick.AddListener(phoneController.GoToContactPage);
        }

        public void Open()
        {
        }

        public void Close()
        {
        }

        public void ShowSubtitle(Subtitle subtitle)
        {
            var isSelf = subtitle.speakerInfo.isPlayer;
            var chatBubbleUI = Instantiate(isSelf ? chatBubbleUISelf : chatBubbleUIOther, chatContainer);
            chatBubbleUI.SetText(subtitle.dialogueEntry.subtitleText);
        }

        public void HideSubtitle(Subtitle subtitle)
        {
        }

        public void ShowResponses(Subtitle subtitle, Response[] responses, float timeout)
        {
            standardUIMenuPanel.ShowResponses(subtitle, responses, transform);
        }

        public void HideResponses()
        {
            standardUIMenuPanel.HideResponses();
        }

        public void ShowQTEIndicator(int index)
        {
        }

        public void HideQTEIndicator(int index)
        {
        }

        public void ShowAlert(string message, float duration)
        {
        }

        public void HideAlert()
        {
        }

        /// This method is called when a response is selected in the standard UI menu.
        [UsedImplicitly]
        void OnClick(Response response)
        {
            SelectedResponseHandler?.Invoke(this, new SelectedResponseEventArgs(response));
        }
    }
}