using TMPro;
using UnityEngine;

namespace PizzaMaker
{
    public class ChatBubbleUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        public void SetText(string text)
        {
            this.text.text = text;
        }
    }
}