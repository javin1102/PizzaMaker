using System;
using PixelCrushers.DialogueSystem;
using PrimeTween;
using UnityEngine;

namespace PizzaMaker.Events
{
    [AddComponentMenu("Pizza Maker/Events/Day One/Introduction Event Trigger")]
    public class IntroductionEventTrigger : EventTrigger
    {
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
                DialogueManager.Instance.StartConversation("day1/boss/intro", player.PhoneController.transform, player.PhoneController.transform, -1, player.PhoneController.DialogueUI);
                Tween.Delay(3f, () =>
                {
                    if(QuestLog.GetQuestState(LuaVariables.Quests.Day1OpenPhone.id) == QuestState.Active)
                        InGameUIController.Instance.ShowQuestInformationUI("Press <b>Tab</b> to Show/Hide your phone", LuaVariables.Quests.Day1OpenPhone);
                });
            }
        }
    }
}