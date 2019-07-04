using Krk.Common.Elements;
using Krk.Elevators;
using UnityEngine;
using Zenject;

namespace Krk.Doors
{
    public class ElevatorDoorMediator : MonoBehaviour
    {
        [SerializeField] Trigger trigger;

        [Inject] ElevatorDoorController controller;
        [Inject] ElevatorController elevatorController;

        void OnEnable()
        {
            elevatorController.OnMoveFinished += HandleElevatorMoveFinished;
            trigger.OnActivated += HandleTriggerActivated;
            trigger.OnDeactivated += HandleTriggerDeactivated;
        }

        void OnDisable()
        {
            elevatorController.OnMoveFinished -= HandleElevatorMoveFinished;
            
            trigger.OnActivated -= HandleTriggerActivated;
            trigger.OnDeactivated -= HandleTriggerDeactivated;
        }

        void HandleElevatorMoveFinished(int floorIndex)
        {
            controller.TryOpenAndLock();
        }

        void HandleTriggerActivated()
        {
        }

        void HandleTriggerDeactivated()
        {
        }
    }
}