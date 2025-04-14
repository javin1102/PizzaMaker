using System;
using PixelCrushers.DialogueSystem;
using PrimeTween;
using UnityEngine;
using Zenject;

namespace PizzaMaker.Events
{
    [AddComponentMenu("Pizza Maker/Events/Day One/Introduction Event Trigger")]
    public class IntroductionEventTrigger : EventTrigger
    {
        private DialogueSystemTrigger dialogueSystemTrigger;
        [Inject] private PlayerController playerController;

        private void Awake()
        {
           dialogueSystemTrigger = GetComponent<DialogueSystemTrigger>();
           dialogueSystemTrigger.overrideDialogueUI = playerController.PhoneController.DialogueUIGO;
           dialogueSystemTrigger.conversationActor = playerController.PhoneController.transform;
           dialogueSystemTrigger.conversationConversant = playerController.PhoneController.transform;
        }

        private void Start()
        {
           if(DialogueLua.GetVariable(LuaVariables.Conversatons.Day1BossIntro).asBool)
               gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isTriggered || DialogueLua.GetVariable(LuaVariables.Conversatons.Day1BossIntro).asBool)
            {
                gameObject.SetActive(false);
                return;
            }
            
            if (other.TryGetComponent(out PlayerController player))
            {
                isTriggered = true;
                DialogueManager.Instance.DialogueUI.ShowAlert("You got a message", 2f);
                QuestLog.SetQuestState(LuaVariables.Quests.Day1OpenPhone.id, QuestState.Active);
                dialogueSystemTrigger.enabled = true;
                Tween.Delay(3f, () =>
                {
                    if(QuestLog.GetQuestState(LuaVariables.Quests.Day1OpenPhone.id) == QuestState.Active)
                        InGameUIController.Instance.ShowQuestInformationUI("Press <b>Tab</b> to Show/Hide your phone", LuaVariables.Quests.Day1OpenPhone);
                });
            }
        }
    }
}