using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PizzaMaker
{
    public class PhoneContactUI : MonoBehaviour
    {
        [SerializeField] private Button button;
        [field: SerializeField] public TMP_Text TextName { get; private set; }
        public UnityAction<Contact> OnClicked { get; set; }
        public Contact Contact { get; set; }

        private void Awake()
        {
            button.onClick.AddListener(() => OnClicked?.Invoke(Contact));
        }
    }
}