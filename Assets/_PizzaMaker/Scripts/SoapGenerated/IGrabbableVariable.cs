using UnityEngine;
using Obvious.Soap;

namespace PizzaMaker
{
    [CreateAssetMenu(fileName = "scriptable_variable_" + nameof(IGrabbable), menuName = "Soap/ScriptableVariables/"+ nameof(IGrabbable))]
    public class IGrabbableVariable : ScriptableVariable<IGrabbable>
    {
            
    }
}
