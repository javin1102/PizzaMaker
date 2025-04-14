using PixelCrushers.DialogueSystem;
using PizzaMaker;
using UnityEngine;
using Zenject;

public class InGameInstaller : MonoInstaller
{
    [SerializeField] private DialogueDatabase chatDialogueDatabase;
    [SerializeField] private PlayerController playerController;
    public override void InstallBindings()
    {
        Container.Bind<DialogueDatabase>().WithId(GlobalVars.ZenDialogueMainDatabaseId).FromInstance(chatDialogueDatabase).AsSingle().NonLazy();
        Container.Bind<PlayerController>().FromInstance(playerController).AsSingle().NonLazy();
        Container.Bind<PhoneController>().FromComponentInParents().AsSingle();
    }
}
