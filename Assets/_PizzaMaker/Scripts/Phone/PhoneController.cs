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
        }

        public void GoToContactPage()
        {
            phoneContactPageUI.gameObject.SetActive(true);
            phoneChatPageUI.gameObject.SetActive(false);
        }

        public void GoToChatPage()
        {
            phoneContactPageUI.gameObject.SetActive(false);
            phoneChatPageUI.gameObject.SetActive(true);
        }
    }
}
