using PixelCrushers.DialogueSystem;
using PizzaMaker;
using UnityEngine;
using Zenject;

public class ZenInstaller : MonoInstaller
{
    [SerializeField] private DialogueDatabase chatDialogueDatabase;
    public override void InstallBindings()
    {
        Container.Bind<DialogueDatabase>().WithId(Constants.ZenDialogueMainDatabaseId).FromInstance(chatDialogueDatabase).AsSingle().NonLazy();
        Container.Bind<PhoneController>().FromComponentInParents().AsSingle();
    }
}