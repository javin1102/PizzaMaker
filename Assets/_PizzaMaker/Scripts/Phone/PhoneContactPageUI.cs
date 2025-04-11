using System.Collections.Generic;
using UnityEngine;

namespace PizzaMaker
{
    public class PhoneContactPageUI : PhonePageUI
    {
        [SerializeField] private PhoneContactUI[] phoneContactUIs;
        public Contact[] Contacts { get; private set; }
        
        private void OnEnable()
        {
            
        }
    }
}