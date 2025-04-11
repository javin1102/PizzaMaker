using UnityEngine;

namespace PizzaMaker
{
    public class PhoneChatPageUI : PhonePageUI
    {
        [SerializeField] private ChatBubbleUI chatBubbleUIOther;
        [SerializeField] private ChatBubbleUI chatBubbleUISelf;
        
        private void Initialize(Contact contact)
        {
            
        }
    }
}