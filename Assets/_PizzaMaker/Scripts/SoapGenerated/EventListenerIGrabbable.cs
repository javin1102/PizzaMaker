using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace PizzaMaker
{
    [AddComponentMenu("Soap/EventListeners/EventListener"+nameof(IGrabbable))]
    public class EventListenerIGrabbable : EventListenerGeneric<IGrabbable>
    {
        [SerializeField] private EventResponse[] _eventResponses = null;
        protected override EventResponse<IGrabbable>[] EventResponses => _eventResponses;
        [System.Serializable]
        public class EventResponse : EventResponse<IGrabbable>
        {
            [SerializeField] private ScriptableEventIGrabbable _scriptableEvent = null;
            public override ScriptableEvent<IGrabbable> ScriptableEvent => _scriptableEvent;
            [SerializeField] private IGrabbableUnityEvent _response = null;
            public override UnityEvent<IGrabbable> Response => _response;
        }
        [System.Serializable]
        public class IGrabbableUnityEvent : UnityEvent<IGrabbable>
        {
            
        }
    }
}
