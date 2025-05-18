using UnityEngine;
using Obvious.Soap;

namespace PizzaMaker
{
    [CreateAssetMenu(fileName = "scriptable_event_" + nameof(IGrabbable), menuName = "Soap/ScriptableEvents/"+ nameof(IGrabbable))]
    public class ScriptableEventIGrabbable : ScriptableEvent<IGrabbable>
    {
        
    }
}
