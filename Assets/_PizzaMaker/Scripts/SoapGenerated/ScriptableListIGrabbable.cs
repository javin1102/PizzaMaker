using UnityEngine;
using Obvious.Soap;

namespace PizzaMaker
{
    [CreateAssetMenu(fileName = "scriptable_list_" + nameof(IGrabbable), menuName = "Soap/ScriptableLists/"+ nameof(IGrabbable))]
    public class ScriptableListIGrabbable : ScriptableList<IGrabbable>
    {
        
    }
}
