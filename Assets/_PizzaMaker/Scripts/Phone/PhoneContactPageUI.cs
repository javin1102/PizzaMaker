using PixelCrushers.DialogueSystem;
using UnityEngine;
using Zenject;

namespace PizzaMaker
{
    public class PhoneContactPageUI : PhonePageUI
    {
        [SerializeField] private PhoneContactUI[] phoneContactUIs;
        [Inject] private PhoneController phoneController;
        [Inject(Id = GlobalVars.ZenDialogueMainDatabaseId)] 
        private DialogueDatabase dialogueDatabase;

        public void Initialize()
        {
            for (int i = 0; i < phoneController.Contacts.Count; i++)
            {
                phoneContactUIs[i].TextName.text = phoneController.Contacts[i].name;
                phoneContactUIs[i].Contact = phoneController.Contacts[i];
                phoneContactUIs[i].OnClicked = GoToChatPage;
            }
            
            GameEvents.OnChatReceived += OnChatReceived;
        }

        private void OnChatReceived(Contact arg1, string arg2)
        {
            if (arg1 == null) return;
            if (arg1.actorId == -1) return;

            var actor = dialogueDatabase.GetActor(arg1.actorId);
            if (actor == null) return;

            foreach (var contactUI in phoneContactUIs)
            {
                if (contactUI.Contact == arg1 && IsShowing)
                {
                    contactUI.ShowNotification(true);
                   break;
                }
            }
        }

        private void GoToChatPage(Contact contact) => phoneController.GoToChatPage(contact);
    }
}