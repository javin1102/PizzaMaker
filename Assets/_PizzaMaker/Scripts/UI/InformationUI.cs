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
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image imageBackground;
        [SerializeField] private TMP_Text text;
        public QuestId CurrentQuestId { get; set; }



        public void FadeIn()
        {
            FadeIn(0.25f);
        }

        public void FadeIn(float duration)
        {
            gameObject.SetActive(true);
            Tween.Alpha(canvasGroup, 0f, 0.8f, duration);
        }
        
        public void FadeOut()
        {
            FadeOut(0.25f);
        }

        public void FadeOut(float duration)
        {
            Tween.Alpha(canvasGroup, 0f, duration).OnComplete(() => { gameObject.SetActive(false); });
        }

        public void SetText(string text)
        {
            this.text.text = text;
        }
        
        public void SetQuest(QuestId questId)
        {
            CurrentQuestId = questId;
        }

       
    }
    
}
