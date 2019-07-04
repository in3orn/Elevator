using Krk.Common.Elements;
using UnityEngine;
using Zenject;

namespace Krk.Doors
{
    public class DoorAutoMediator : MonoBehaviour
    {
        [SerializeField] Trigger trigger;

        [Inject] DoorAutoController controller;

        void OnEnable()
        {
            trigger.OnActivated += HandleTriggerActivated;
            trigger.OnDeactivated += HandleTriggerDeactivated;
        }

        void OnDisable()
        {
            trigger.OnActivated -= HandleTriggerActivated;
            trigger.OnDeactivated -= HandleTriggerDeactivated;
        }

        void HandleTriggerActivated()
        {
            controller.OpenAndLock();
        }

        void HandleTriggerDeactivated()
        {
            controller.UnlockAndClose();
        }
    }
}