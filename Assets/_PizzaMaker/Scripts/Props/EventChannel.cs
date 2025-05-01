using System;
using UnityEngine;

namespace PizzaMaker
{

    [CreateAssetMenu(menuName = "Create/Event Channel", fileName = "New Event Channel")]
    public class EventChannel : ScriptableObject
    {
        public Action<IGrabbable> GrabAction;
    }
}