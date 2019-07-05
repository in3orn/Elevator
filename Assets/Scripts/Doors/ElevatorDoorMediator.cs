using Krk.Common.Elements;
using Krk.Elevators;
using UnityEngine;
using Zenject;

namespace Krk.Doors
{
    public class ElevatorDoorMediator : MonoBehaviour
    {
        [SerializeField] Trigger trigger;

        [Inject] ElevatorDoorController doorController;
        [Inject] ElevatorController elevatorController;

        void OnEnable()
        {
            elevatorController.OnMoveFinished += HandleElevatorMoveFinished;
            elevatorController.OnWaitFinished += HandleElevatorWaitFinished;

            doorController.OnOpenFinished += HandleDoorOpenFinished;
            doorController.OnCloseFinished += HandleDoorCloseFinished;

            trigger.OnActivated += HandleTriggerActivated;
            trigger.OnDeactivated += HandleTriggerDeactivated;
        }

        void OnDisable()
        {
            elevatorController.OnMoveFinished -= HandleElevatorMoveFinished;
            elevatorController.OnWaitFinished -= HandleElevatorWaitFinished;

            doorController.OnOpenFinished -= HandleDoorOpenFinished;
            doorController.OnCloseFinished -= HandleDoorCloseFinished;

            trigger.OnActivated -= HandleTriggerActivated;
            trigger.OnDeactivated -= HandleTriggerDeactivated;
        }

        void HandleElevatorMoveFinished(int floorIndex)
        {
            doorController.TryOpenAndLock();
        }

        void HandleElevatorWaitFinished()
        {
            if (!trigger.IsActivated)
                doorController.UnlockAndClose();
        }

        void HandleDoorOpenFinished()
        {
            elevatorController.WaitStart();
        }

        void HandleDoorCloseFinished()
        {
            elevatorController.TryMove();
        }

        void HandleTriggerActivated()
        {
            if (doorController.State.running)
                doorController.TryOpenAndLock();
        }

        void HandleTriggerDeactivated()
        {
            if (!elevatorController.Waiting && !doorController.State.running)
                doorController.UnlockAndClose();
        }
    }
}