using PixelCrushers.DialogueSystem.Wrappers;
using Reflex.Core;
using UnityEngine;

namespace PizzaMaker
{
    public class ReflexInGameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private DialogueDatabase dialogueMainDatabase;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PhoneController phoneController;

        public void InstallBindings(ContainerBuilder cb)
        {
            cb.AddSingleton(playerController);
            cb.AddSingleton(phoneController);
            cb.AddSingleton(dialogueMainDatabase);
        }
    }
}
