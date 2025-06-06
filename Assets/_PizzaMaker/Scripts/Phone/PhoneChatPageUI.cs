using System;
using System.Collections;
using JetBrains.Annotations;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Reflex.Attributes;
using ToolBox.Serialization;

namespace PizzaMaker
{
    public class PhoneChatPageUI : PhonePageUI, IDialogueUI
    {
        public event EventHandler<SelectedResponseEventArgs> SelectedResponseHandler;

        [SerializeField] private TMP_Text textContactName;
        [SerializeField] private Button backButton;
        [SerializeField] private ChatBubbleUI chatBubbleUIOther;
        [SerializeField] private ChatBubbleUI chatBubbleUISelf;
        [SerializeField] private RectTransform chatContainer;
        [SerializeField] private StandardUIMenuPanel standardUIMenuPanel;
        [Inject] private PhoneController phoneController;
        [Inject] private PlayerController PlayerController;
        [Inject] private PixelCrushers.DialogueSystem.Wrappers.DialogueDatabase MainDatabase;

        private Contact contact;
        private Actor currentConversant;

        private void Awake()
        {
            backButton.onClick.AddListener(phoneController.GoToContactPage);
        }

        public void Show(Contact contact)
        {
            this.contact = contact;
            textContactName.text = contact.name;
            Show();

            //chat is ongoing
            if (IsConversantSameAsContact() && DialogueManager.currentActor == phoneController.transform)
            {
                backButton.interactable = false;
            }

            foreach (var conversationTitle in contact.conversationTitles)
            {
                var conv = MainDatabase.GetConversation(conversationTitle);
                foreach (DialogueEntry dialogueEntry in conv.dialogueEntries)
                {
                    if (DialogueLua.GetSimStatus(dialogueEntry) == DialogueLua.WasDisplayed)
                    {
                        var actor = MainDatabase.GetActor(dialogueEntry.ActorID);
                        if (!string.IsNullOrEmpty(dialogueEntry.DialogueText))
                            ShowChatBubble(dialogueEntry.DialogueText, actor.IsPlayer);
                    }
                }
            }
        }

        public override void Hide()
        {
            base.Hide();
            foreach (Transform chatBubbleUI in chatContainer)
            {
                Destroy(chatBubbleUI.gameObject);
            }

            contact = null;
        }

        public void Open()
        {
            var conversation = MainDatabase.GetConversation(DialogueManager.Instance.LastConversationID);
            var conversant = MainDatabase.GetActor(conversation.ConversantID);
            currentConversant = conversant;

            if (IsConversantSameAsContact() && DialogueManager.currentActor == phoneController.transform)
                backButton.interactable = false;
        }

        public void Close()
        {
            DataSerializer.Save(SaveKey.DataDialogueState, PersistentDataManager.GetSaveData());
        }

        public void ShowSubtitle(Subtitle subtitle)
        {
            var chatContact = phoneController.GetContactFromActor(currentConversant);
            if (chatContact != null)
                GameEvents.OnChatReceived?.Invoke(chatContact, subtitle.dialogueEntry.subtitleText);

            if (contact == null)
                return;

            if (!IsConversantSameAsContact())
                return;

            ShowChatBubble(subtitle.dialogueEntry.subtitleText, subtitle.speakerInfo.IsPlayer);
        }

        private void ShowChatBubble(string text, bool isSelf)
        {
            ChatBubbleUI chatBubbleUI = Instantiate(isSelf ? chatBubbleUISelf : chatBubbleUIOther, chatContainer);
            chatBubbleUI.gameObject.SetActive(true);
            chatBubbleUI.SetText(text);
        }

        public void HideSubtitle(Subtitle subtitle)
        {
        }

        public void ShowResponses(Subtitle subtitle, Response[] responses, float timeout)
        {
            InGameUIController.Instance.StartCoroutine(ResponseCoroutine());

            IEnumerator ResponseCoroutine()
            {
                while (!IsConversantSameAsContact())
                {
                    yield return null;
                }

                yield return new WaitForSeconds(1f);
                standardUIMenuPanel.ShowResponses(subtitle, responses, transform);
            }
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
        private void OnClick(Response response)
        {
            SelectedResponseHandler?.Invoke(this, new SelectedResponseEventArgs(response));
        }

        public void OnConversationEnded(Transform t)
        {
            if (t != phoneController.transform)
                return;

            currentConversant = null;
            backButton.interactable = true;
        }

        public void OnConversationStarted(Transform t)
        {
            if (t != phoneController.transform)
                return;
        }

        private bool IsConversantSameAsContact()
        {
            if (currentConversant == null || contact == null)
                return false;

            return currentConversant.id == contact.actorId;
        }
    }
}