using System;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    public class PhoneController : MonoBehaviour
    {
        public GameObject DialogueUIGO => phoneChatPageUI.gameObject;
        public IDialogueUI DialogueUI => phoneChatPageUI;
        [field: SerializeField] public List<Contact> Contacts { get; private set; }
        [SerializeField] private PhoneContactPageUI phoneContactPageUI;
        [SerializeField] private PhoneChatPageUI phoneChatPageUI;

        public void Initialize()
        {
            phoneContactPageUI.Initialize();
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

        public Contact GetContactFromActor(Actor actor)
        {
            foreach (var contact in Contacts)
            {
                if (contact.actorId == actor.id)
                {
                    return contact;
                }
            }

            return null;
        }


        public void OnConversationStarted(Transform t) => phoneChatPageUI.OnConversationStarted(t);
        public void OnConversationEnded(Transform t) => phoneChatPageUI.OnConversationEnded(t);
    }
}
