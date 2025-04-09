using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private FirstPersonController firstPersonController;
        protected void Awake()
        {
            var spawnPoint = GameObject.FindGameObjectWithTag(Constants.TagSpawn);
            if (spawnPoint != null)
            {
                transform.position = spawnPoint.transform.position;
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
