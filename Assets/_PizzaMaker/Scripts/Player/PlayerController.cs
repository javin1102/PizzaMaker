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
                transform.rotation = spawnPoint.transform.rotation;
            }

            PersistentDataManager.ApplySaveData(PlayerPrefs.GetString(Constants.SaveData));
            var s = PersistentDataManager.GetSaveData();
            // Debug.LogError(s);
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
            PersistentDataManager.Record();
            var s = PersistentDataManager.GetSaveData();
            PlayerPrefs.SetString(Constants.SaveData, s);
            // Debug.LogWarning(s);
        }
    }

}
