using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private FirstPersonController firstPersonController;
        [SerializeField] private GameObject phoneGO;
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
                phoneGO.SetActive(!phoneGO.activeInHierarchy);
                Cursor.lockState = phoneGO.activeInHierarchy ? CursorLockMode.None : CursorLockMode.Locked;
                firstPersonController.enabled = !phoneGO.activeInHierarchy;
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
            Cursor.lockState = CursorLockMode.Locked;
            firstPersonController.enabled = true;
        }
    }

}
