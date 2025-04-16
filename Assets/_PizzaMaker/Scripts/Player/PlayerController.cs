using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace PizzaMaker
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject GrabbedGameObject => grabbedGameObject;
        public bool IsPhoneActive => phoneController.gameObject.activeInHierarchy;
        public FirstPersonController FirstPersonController => firstPersonController;
        public PhoneController PhoneController => phoneController;
        [field: SerializeField] public StandardDialogueUI StandardDialogueUI { get; private set; }

        [SerializeField] private FirstPersonController firstPersonController;
        [SerializeField] private PhoneController phoneController;
        [SerializeField] private Transform grabAttachPoint;

        private Focusable currentFocusable;
        private Selector selector;
        private IInteractable currentInteractable;
        private IGrabbable currentIGrabbable;
        private GameObject grabbedGameObject;

        protected void Awake()
        {
            //Note: Uses PlayerPrefs for temp save data (testing purposes)
            PersistentDataManager.ApplySaveData(PlayerPrefs.GetString(GlobalVars.SaveData));
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
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (currentFocusable != null)
                {
                    currentFocusable.OutFocus();
                    currentFocusable = null;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else if (currentIGrabbable != null)
                {
                    UnGrab();
                }
            }

            if (currentFocusable != null || currentIGrabbable != null)
            {
                selector.maxSelectionDistance = 0;
            }

            if (currentFocusable == null && currentIGrabbable == null)
                selector.maxSelectionDistance = 10f;

            //Temp input for testing
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                TogglePhone();
            }

            var isHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 5f);
            if (isHit && hit.collider.TryGetComponent(out IInteractable interactable))
            {
                if (!IsPhoneActive && currentFocusable == null)
                {
                    if (Input.GetMouseButtonDown(0))
                        currentInteractable?.OnClick(this);
                    else
                    {
                        currentInteractable = interactable;
                        currentInteractable?.OnHover(this);
                    }
                }
            }
            else if (currentInteractable != null)
            {
                currentInteractable?.OnUnhover(this);
                currentInteractable = null;
            }

            if (currentFocusable && Input.GetMouseButton(0))
            {
                currentFocusable.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 200f);
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
            if (IsPhoneActive || currentFocusable != null)
                return;

            //Do not show phone if direct conversation is ongoing
            if (DialogueManager.Instance.IsConversationActive && DialogueManager.Instance.currentConversant != phoneController.transform)
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

            //Do not hide phone if chat conversation is ongoing
            // if (DialogueManager.Instance.IsConversationActive && DialogueManager.Instance.currentConversant == phoneController.transform)
            //     return;

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
                    SetMovement(false);
                    Cursor.lockState = CursorLockMode.None;
                }

                return;
            }

            firstPersonController.cameraCanMove = false;
        }

        private void OnConversationEnded(Transform t)
        {
            phoneController.OnConversationEnded(t);
            if (IsPhoneActive)
            {
                if (t.transform != phoneController.transform)
                {
                    SetMovement(true);
                    Cursor.lockState = CursorLockMode.Locked;
                }

                return;
            }

            Cursor.lockState = CursorLockMode.Locked;
            SetMovement(true);
        }

        public void SetMovement(bool isEnable)
        {
            firstPersonController.playerCanMove = isEnable;
            firstPersonController.cameraCanMove = isEnable;
        }

        public void Grab(IGrabbable grabbable)
        {
            if (currentIGrabbable != null)
                return;

            var iGrabbable = grabbable.GetGrabbableObject(out GameObject objectToGrab);
            objectToGrab.transform.SetParent(grabAttachPoint);
            objectToGrab.transform.localPosition = Vector3.zero;
            objectToGrab.SetGameLayerRecursive(GlobalVars.LayerFocus);
            grabbedGameObject = objectToGrab;
            currentIGrabbable = iGrabbable;
            currentIGrabbable.OnGrab(this);
        }

        private void UnGrab()
        {
            if (currentIGrabbable == null)
                return;

            currentIGrabbable.OnRelease(this);
            currentIGrabbable = null;
            grabbedGameObject = null;
        }
    }
}