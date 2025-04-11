using UnityEngine;

namespace PizzaMaker
{
    public class PhoneController : MonoBehaviour
    {
        [SerializeField] private PhoneContactPageUI phoneContactPageUI;
        [SerializeField] private PhoneChatPageUI phoneChatPageUI;
        

        private void GoToContactPage()
        {
            phoneContactPageUI.gameObject.SetActive(true);
            phoneChatPageUI.gameObject.SetActive(false);
        }

        private void GoToChatPage(Contact contact)
        {
            phoneContactPageUI.gameObject.SetActive(false);
            phoneChatPageUI.gameObject.SetActive(true);
        }
    }
}
