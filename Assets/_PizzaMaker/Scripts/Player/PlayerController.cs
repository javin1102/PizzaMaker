using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private FirstPersonController firstPersonController;
        [SerializeField] private PhoneController phoneController;
        protected void Awake()
        {
            var spawnPoint = GameObject.FindGameObjectWithTag(Constants.TagSpawn);
            if (spawnPoint != null)
            {
                transform.position = spawnPoint.transform.position;
                transform.rotation = spawnPoint.transform.rotation;
            }
        }

        private void Update()
        {
            //Temp input for testing
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                phoneController.gameObject.SetActive(!phoneController.gameObject.activeInHierarchy);
                Cursor.lockState = phoneController.gameObject.activeInHierarchy ? CursorLockMode.None : CursorLockMode.Locked;
                firstPersonController.enabled = !phoneController.gameObject.activeInHierarchy;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DialogueManager.Instance.StartConversation("chat/boss/1", transform, phoneController.transform, 0, phoneController.DialogueUI);
            }

        }

        void Start()
        {
            DialogueManager.Instance.conversationEnded += OnConversationEnded;
            DialogueManager.Instance.conversationStarted += OnConversationStarted;
        }

        void OnDestroy()
        {
            if (DialogueManager.Instance)
            {
                DialogueManager.Instance.conversationStarted -= OnConversationStarted;
                DialogueManager.Instance.conversationEnded -= OnConversationEnded;
            }
        }

        private void OnConversationStarted(Transform t)
        {
            firstPersonController.enabled = false;
        }

        private void OnConversationEnded(Transform t)
        {
            if (phoneController.gameObject.activeInHierarchy)
                return;
            
            Cursor.lockState = CursorLockMode.Locked;
            firstPersonController.enabled = true;
        }
    }

}
