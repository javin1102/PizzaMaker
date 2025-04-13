using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PizzaMaker
{
    public class PhoneContactUI : MonoBehaviour
    {
        public UnityAction<Contact> OnClicked { get; set; }
        public Contact Contact { get; set; }
        [field: SerializeField] public TMP_Text TextName { get; private set; }
        [field: SerializeField] public Image imageNotification; 
        
        [SerializeField] private Button button;

        private void Awake()
        {
            button.onClick.AddListener(() =>
            {
                ShowNotification(false);
                OnClicked?.Invoke(Contact);
            });
        }
        public void ShowNotification(bool show)
        {
            imageNotification.gameObject.SetActive(show);
        }
    }
}