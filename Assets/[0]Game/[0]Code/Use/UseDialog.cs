using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class UseDialog : UseObject
    {
        [SerializeField]
        private Replica[] _replicas;
        
        [SerializeField] 
        private UnityEvent _startEvent;
        
        [SerializeField] 
        private UnityEvent _endEvent;

        public override void Use()
        {
            _startEvent.Invoke();
            GameData.Dialog.SetReplicas(_replicas);
            SignalBus.OnCloseDialog += _endEvent.Invoke;
        }
    }
}