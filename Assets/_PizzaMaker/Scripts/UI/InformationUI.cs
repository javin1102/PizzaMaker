using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationUI : MonoBehaviour
{
    [SerializeField] private Image imageBackground;
    [SerializeField] private TMP_Text text;

    public void FadeIn()
    {
        gameObject.SetActive(true);
        Tween.Alpha(imageBackground, 0f, 0.8f, 0.25f);
    }

    public void FadeOut()
    {
        Tween.Alpha(imageBackground, 0f, 0.25f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    
    public void SetText(string text)
    {
        this.text.text = text;
    }
}