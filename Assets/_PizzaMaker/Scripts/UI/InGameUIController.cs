using PizzaMaker.Tools;
using UnityEngine;

public class InGameUIController : SceneSingleton<InGameUIController>
{
    [SerializeField] private InformationUI informationUI;    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected override void Awake()
    {
        base.Awake();
        informationUI.gameObject.SetActive(false);
    }

    public void ShowInformationUI(string text)
    {
        informationUI.FadeIn();
        informationUI.SetText(text);
    }
    
    public void HideInformationUI()
    {
        informationUI.FadeOut();
    }
}