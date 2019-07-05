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
            elevatorController.OnStateChanged += HandleElevatorStateChanged;

            doorController.OnOpenFinished += HandleDoorOpenFinished;
            doorController.OnCloseFinished += HandleDoorCloseFinished;

            trigger.OnActivated += HandleTriggerActivated;
            trigger.OnDeactivated += HandleTriggerDeactivated;
        }

        void OnDisable()
        {
            elevatorController.OnStateChanged -= HandleElevatorStateChanged;
            
            doorController.OnOpenFinished -= HandleDoorOpenFinished;
            doorController.OnCloseFinished -= HandleDoorCloseFinished;

            trigger.OnActivated -= HandleTriggerActivated;
            trigger.OnDeactivated -= HandleTriggerDeactivated;
        }

        void HandleElevatorStateChanged(ElevatorState state)
        {
            if (state == ElevatorState.WaitingForDoorOpen)
                doorController.TryOpenAndLock();
            else if (state == ElevatorState.WaitingForDoorClose && !trigger.IsActivated)
                doorController.UnlockAndClose();
        }

        void HandleDoorOpenFinished()
        {
            elevatorController.State = ElevatorState.WaitingForPassengers;
        }

        void HandleDoorCloseFinished()
        {
            elevatorController.State = ElevatorState.WaitingForGoingIdle;
            elevatorController.TryMove();
        }

        void HandleTriggerActivated()
        {
            if (doorController.State.running)
                doorController.TryOpenAndLock();
        }

        void HandleTriggerDeactivated()
        {
            if (elevatorController.State == ElevatorState.WaitingForDoorClose && !doorController.State.running)
                doorController.UnlockAndClose();
        }
    }
}