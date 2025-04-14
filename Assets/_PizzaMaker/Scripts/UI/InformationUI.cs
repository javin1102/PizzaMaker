using System;
using PixelCrushers.DialogueSystem;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PizzaMaker
{
    public class InformationUI : MonoBehaviour
    {
        [SerializeField] private Image imageBackground;
        [SerializeField] private TMP_Text text;
        private QuestId currentQuestId;

        private void OnEnable()
        {
            GameEvents.OnQuestStateChanged += OnQuestStateChange;
        }

        private void OnDisable()
        {
            GameEvents.OnQuestStateChanged -= OnQuestStateChange;
        }

        public void FadeIn()
        {
            gameObject.SetActive(true);
            Tween.Alpha(imageBackground, 0f, 0.8f, 0.25f);
        }

        public void FadeOut()
        {
            Tween.Alpha(imageBackground, 0f, 0.25f).OnComplete(() => { gameObject.SetActive(false); });
        }

        public void SetText(string text)
        {
            this.text.text = text;
        }
        
        public void SetQuest(QuestId questId)
        {
            currentQuestId = questId;
        }

        public void OnQuestStateChange(QuestId quest, QuestState state)
        {
            if(string.IsNullOrEmpty(currentQuestId))
                return;
            
            if (quest != currentQuestId)
                return;

            if (state == QuestState.Success)
            {
                Tween.Delay(2, FadeOut);
                currentQuestId = "";
            }
        }
    }
    
}
