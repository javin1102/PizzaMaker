using System;
using PixelCrushers.DialogueSystem;
using PizzaMaker.Tools;
using UnityEngine;

namespace PizzaMaker
{
    public class PlayerController : MonoBehaviour
    {
        public FirstPersonController FirstPersonController => firstPersonController;
        public PhoneController PhoneController => phoneController;
        [SerializeField] private FirstPersonController firstPersonController;
        [SerializeField] private PhoneController phoneController;
        private Focusable currentFocusable;

        protected void Awake()
        {
            //Note: Uses PlayerPrefs for temp save data (testing purposes)
            // PersistentDataManager.ApplySaveData(PlayerPrefs.GetString(GlobalVars.SaveData));
            Debug.LogError(PlayerPrefs.GetString(GlobalVars.SaveData));
            var spawnPoint = GameObject.FindGameObjectWithTag(GlobalVars.TagSpawn);
            if (spawnPoint != null)
            {
                transform.position = spawnPoint.transform.position;
                transform.rotation = spawnPoint.transform.rotation;
            }
            phoneController.Initialize();
        }


        void Start()
        {
            DialogueManager.Instance.conversationEnded += OnConversationEnded;
            DialogueManager.Instance.conversationStarted += OnConversationStarted;
        }

        private void Update()
        {
            //Temp input for testing
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                TogglePhone();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (currentFocusable == null && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, 10f) 
                                             && hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.OnClick();
                }
            }

            if (currentFocusable && Input.GetMouseButton(0))
            {
                currentFocusable.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 200f);               
            }
            if (Input.GetKeyDown(KeyCode.Q) && currentFocusable != null)
            {
                currentFocusable.OutFocus();
                firstPersonController.enabled = true;
                currentFocusable = null;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }


        void OnDestroy()
        {
            if (DialogueManager.Instance)
            {
                DialogueManager.Instance.conversationStarted -= OnConversationStarted;
                DialogueManager.Instance.conversationEnded -= OnConversationEnded;
            }
        }

        public void TogglePhone()
        {
            if (phoneController.gameObject.activeInHierarchy)
            {
                HidePhone();
                return;
            }
            ShowPhone();
        }

        public void ShowPhone()
        {
            if (phoneController.gameObject.activeInHierarchy)
                return;
            
            phoneController.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            firstPersonController.cameraCanMove = false;

            var openPhoneQuestState = QuestLog.GetQuestState(LuaVariables.Quests.Day1OpenPhone.id);
            if (openPhoneQuestState == QuestState.Active)
            {
                QuestLog.SetQuestState(LuaVariables.Quests.Day1OpenPhone.id, QuestState.Success);
                GameEvents.OnQuestStateChanged?.Invoke(LuaVariables.Quests.Day1OpenPhone, QuestState.Success);
            }
        }

        public void HidePhone()
        {
            if (!phoneController.gameObject.activeInHierarchy)
                return;

            phoneController.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            firstPersonController.cameraCanMove = true;
        }

        public void SetFocusable(Focusable focusable)
        {
            currentFocusable = focusable;
        }


        private void OnConversationStarted(Transform t)
        {
            phoneController.OnConversationStarted(t);
            if (!phoneController.gameObject.activeInHierarchy)
                return;
            firstPersonController.cameraCanMove = false;
        }

        private void OnConversationEnded(Transform t)
        {
            phoneController.OnConversationEnded(t);
            if (phoneController.gameObject.activeInHierarchy)
                return;

            Cursor.lockState = CursorLockMode.Locked;
            firstPersonController.cameraCanMove = true;
        }
    }
}