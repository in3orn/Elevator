using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Krk.Common.Elements
{
    public class Trigger : MonoBehaviour
    {
        public UnityAction OnActivated;
        public UnityAction OnDeactivated;
        
        [SerializeField] TriggerConfig config;

        int count;

        public bool IsActivated => count > 0;
        
        void OnTriggerEnter(Collider other)
        {
            if (config.layers.Contains(other.gameObject.layer))
            {
                count++;
                
                if(count == 1) 
                    OnActivated?.Invoke();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (config.layers.Contains(other.gameObject.layer))
            {
                count--;
                
                if(count == 0) 
                    OnDeactivated?.Invoke();
            }
        }
    }
}