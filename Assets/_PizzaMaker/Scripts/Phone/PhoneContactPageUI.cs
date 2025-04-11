using PixelCrushers.DialogueSystem;
using UnityEngine;
using Zenject;

namespace PizzaMaker
{
    public class PhoneContactPageUI : PhonePageUI
    {
        [SerializeField] private PhoneContactUI[] phoneContactUIs;
        [Inject] private PhoneController phoneController;
        [Inject(Id = Constants.ZenDialogueMainDatabaseId)] 
        private DialogueDatabase dialogueDatabase;

        public void Initialize()
        {
            for (int i = 0; i < phoneController.Contacts.Count; i++)
            {
                phoneContactUIs[i].TextName.text = phoneController.Contacts[i].name;
                phoneContactUIs[i].OnClicked = GoToChatPage;
            }

        }

        private void GoToChatPage() => phoneController.GoToChatPage();
    }
}