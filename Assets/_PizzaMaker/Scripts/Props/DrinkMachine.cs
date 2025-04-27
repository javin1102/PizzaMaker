using System.Collections.Generic;
using Reflex.Extensions;
using Reflex.Injectors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PizzaMaker
{
    [DefaultExecutionOrder(-1000)]
    public class DrinkMachine : SerializedMonoBehaviour
    {
        [field: SerializeField] public Dictionary<DrinkMachinePresser, DrinkMachineAttachment> DrinkPairs;
        private void Awake()
        {
            var parentContainer = gameObject.scene.GetSceneContainer();
            using var objectContainer = parentContainer.Scope(builder =>
            {
                builder.SetName("DrinkMachine");
                builder.AddSingleton(this);
                GameObjectInjector.InjectRecursive(gameObject, builder.Build());
            });
        }
    }
}