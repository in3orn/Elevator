using Krk.Elevators;
using UnityEngine.Events;

namespace Krk.Doors
{
    public class ElevatorDoorController
    {
        public UnityAction OnOpenFinished
        {
            get => doorController.OnOpenFinished;
            set => doorController.OnOpenFinished = value;
        }

        public UnityAction OnCloseFinished
        {
            get => doorController.OnCloseFinished;
            set => doorController.OnCloseFinished = value;
        }

        readonly FloorConfig config;
        readonly DoorController doorController;
        readonly ElevatorController elevatorController;

        public DoorState State => doorController.State;

        public ElevatorDoorController(FloorConfig config, DoorController doorController,
            ElevatorController elevatorController)
        {
            this.config = config;
            this.doorController = doorController;
            this.elevatorController = elevatorController;
        }

        public void TryOpenAndLock()
        {
            if (!elevatorController.IsOnFloor(config.floorIndex)) return;

            doorController.OpenStart();
            doorController.SetLocked(true);
        }

        public void UnlockAndClose()
        {
            doorController.SetLocked(false);
            doorController.CloseStart();
        }
    }
}