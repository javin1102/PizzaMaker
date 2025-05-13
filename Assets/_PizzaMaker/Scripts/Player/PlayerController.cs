using PixelCrushers.DialogueSystem;
using Reflex.Core;
using UnityEngine;
using DialogueDatabase = PixelCrushers.DialogueSystem.Wrappers.DialogueDatabase;

namespace PizzaMaker
{
    public class PlayerController : MonoBehaviour
    {
        public IGrabbable CurrentIGrabbable { get; set; }
        public bool IsPhoneActive => phoneController.gameObject.activeInHierarchy;
        public FirstPersonController FirstPersonController => firstPersonController;
        public PhoneController PhoneController => phoneController;
        [field: SerializeField] public StandardDialogueUI StandardDialogueUI { get; private set; }

        [SerializeField] private FirstPersonController firstPersonController;
        [SerializeField] private PhoneController phoneController;
        [SerializeField] private Transform grabAttachPoint;
        [SerializeField] private DialogueDatabase demoDatabase;
        [SerializeField] private EventChannel grabEventChannel;

        private Focusable currentFocusable;
        private Selector selector;
        private IInteractable currentInteractable;
        private ContainerBuilder _cb;
        private Camera mainCamera;

        protected void Awake()
        {
            mainCamera = Camera.main;
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
                else if (CurrentIGrabbable != null)
                {
                    CurrentIGrabbable.OnRelease(this);
                    UnGrab();
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                CurrentIGrabbable?.OnGrabUsed(this);
            }

            if (currentFocusable != null || CurrentIGrabbable != null)
            {
                // selector.maxSelectionDistance = 0;
            }

            // if (currentFocusable == null && CurrentIGrabbable == null)
            //     selector.maxSelectionDistance = 10f;

            //Temp input for testing
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                TogglePhone();
            }

            var isHit = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, 5f);
            if (isHit && hit.collider.TryGetComponent(out IInteractable interactable))
            {
                // var interactable = hit.collider.TryGetComponent(out IInteractable interactableComponent) ? interactableComponent : hit.collider.GetComponentInParent<IInteractable>();
                if (currentInteractable != null && interactable != currentInteractable)
                {
                    currentInteractable.OnUnhover(this);
                    currentInteractable = null;
                }
                
                if (!IsPhoneActive && currentFocusable == null)
                {
                    if (Input.GetMouseButtonDown(0))
                        currentInteractable?.OnClick(this, ref hit);
                    else
                    {
                        currentInteractable = interactable;
                        currentInteractable?.OnHover(this, ref hit);
                    }
                }

            }
            else if (currentInteractable != null)
            {
                currentInteractable.OnUnhover(this);
                currentInteractable = null;
            }

            if (currentFocusable && Input.GetMouseButton(0))
            {
                currentFocusable.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * (Time.deltaTime * 200f));
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

            var openPhoneQuestState = QuestLog.GetQuestState(CustomLua.Quests.Day1OpenPhone.id);
            if (openPhoneQuestState == QuestState.Active)
            {
                QuestLog.SetQuestState(CustomLua.Quests.Day1OpenPhone.id, QuestState.Success);
                GameEvents.OnQuestStateChanged?.Invoke(CustomLua.Quests.Day1OpenPhone, QuestState.Success);
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

        public void Grab<T>(IGrabbable grabbable) where T : MonoBehaviour, IGrabbable
        {
            if (CurrentIGrabbable != null)
                return;

            var objectToGrab = grabbable.GetGrabbableObject<T>();
            objectToGrab.transform.SetParent(grabAttachPoint);
            objectToGrab.transform.localPosition = Vector3.zero;
            objectToGrab.gameObject.SetGameLayerRecursive(GlobalVars.LayerFocus);
            CurrentIGrabbable = objectToGrab.GetComponent<IGrabbable>();
            CurrentIGrabbable.OnGrab(this);
            grabEventChannel.GrabAction?.Invoke(grabbable);
        }

        public void UnGrab()
        {
            if (CurrentIGrabbable == null)
                return;

            CurrentIGrabbable = null;
        }
    }
}