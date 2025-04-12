using System;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    public class PhoneController : MonoBehaviour
    {
        public IDialogueUI DialogueUI => phoneChatPageUI;
        [field: SerializeField] public List<Contact> Contacts { get; private set; }
        public GameObject StandardDialogueUI { get; private set; }
        [SerializeField] private PhoneContactPageUI phoneContactPageUI;
        [SerializeField] private PhoneChatPageUI phoneChatPageUI;

        private void Awake()
        {
            phoneContactPageUI.Initialize();
            StandardDialogueUI = DialogueManager.Instance.displaySettings.dialogueUI;
            GoToContactPage();
        }

        public void GoToContactPage()
        {
            phoneContactPageUI.Show();
            phoneChatPageUI.Hide();
        }

        public void GoToChatPage(Contact contact)
        {
            phoneContactPageUI.Hide();
            phoneChatPageUI.Show(contact);
        }



        public void OnConversationStarted(Transform t) => phoneChatPageUI.OnConversationStarted(t);
        public void OnConversationEnded(Transform t) => phoneChatPageUI.OnConversationEnded(t);
    }
}
