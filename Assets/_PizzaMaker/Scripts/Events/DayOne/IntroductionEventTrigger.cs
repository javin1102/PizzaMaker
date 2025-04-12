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
           if(DialogueLua.GetVariable("day1_boss_intro").asBool)
               gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isTriggered || DialogueLua.GetVariable("day1_boss_intro").asBool)
            {
                gameObject.SetActive(false);
                return;
            }
            
            if (other.TryGetComponent(out PlayerController player))
            {
                isTriggered = true;
                DialogueManager.Instance.DialogueUI.ShowAlert("You got a message", 2f);
                Tween.Delay(3f, () => InGameUIController.Instance.ShowInformationUI("Press <b>Tab</b> to Show/Hide your phone"));
                Tween.Delay(4f, () =>
                {
                    // DialogueManager.Instance.activeConversation.
                    DialogueManager.Instance.StartConversation("day1/boss/intro", player.PhoneController.transform, player.PhoneController.transform, -1, player.PhoneController.DialogueUI);
                });
            }
        }
    }
}