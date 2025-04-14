using System;
using PixelCrushers.DialogueSystem;
using PizzaMaker.Tools;
using UnityEngine;

namespace PizzaMaker
{
    public class PlayerController : MonoBehaviour
    {
        public bool IsPhoneActive => phoneController.gameObject.activeInHierarchy;
        public FirstPersonController FirstPersonController => firstPersonController;
        public PhoneController PhoneController => phoneController;
        [field: SerializeField] public StandardDialogueUI StandardDialogueUI { get; private set; }
        
        [SerializeField] private FirstPersonController firstPersonController;
        [SerializeField] private PhoneController phoneController;
        private Focusable currentFocusable;
        private Selector selector;

        protected void Awake()
        {
            //Note: Uses PlayerPrefs for temp save data (testing purposes)
            // PersistentDataManager.ApplySaveData(PlayerPrefs.GetString(GlobalVars.SaveData));
            // Debug.LogError(PlayerPrefs.GetString(GlobalVars.SaveData));
            selector = GetComponent<Selector>();
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
            StandardDialogueUI = DialogueManager.Instance.standardDialogueUI;
            DialogueManager.Instance.conversationEnded += OnConversationEnded;
            DialogueManager.Instance.conversationStarted += OnConversationStarted;
        }

        private void Update()
        {
            // Debug.LogError(DialogueManager.Instance.lastConversationEnded);
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
            if (IsPhoneActive)
            {
                HidePhone();
                return;
            }
            ShowPhone();
        }

        public void ShowPhone()
        {
            if (IsPhoneActive)
                return;
            
            phoneController.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            firstPersonController.cameraCanMove = false;
            selector.enabled = false;

            var openPhoneQuestState = QuestLog.GetQuestState(LuaVariables.Quests.Day1OpenPhone.id);
            if (openPhoneQuestState == QuestState.Active)
            {
                QuestLog.SetQuestState(LuaVariables.Quests.Day1OpenPhone.id, QuestState.Success);
                GameEvents.OnQuestStateChanged?.Invoke(LuaVariables.Quests.Day1OpenPhone, QuestState.Success);
            }
        }

        public void HidePhone()
        {
            if (!IsPhoneActive)
                return;
            
            selector.enabled = true;
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
            if (!IsPhoneActive)
            {
                if (t.transform != phoneController.transform)
                {
                    firstPersonController.enabled = false;
                    Cursor.lockState = CursorLockMode.None;
                }
                return;
            }
            firstPersonController.cameraCanMove = false;
        }

        private void OnConversationEnded(Transform t)
        {
            phoneController.OnConversationEnded(t);
            if (!IsPhoneActive)
            {
                if (t.transform != phoneController.transform)
                {
                    firstPersonController.enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                return;
            }

            Cursor.lockState = CursorLockMode.Locked;
            firstPersonController.cameraCanMove = true;
        }
    }
}