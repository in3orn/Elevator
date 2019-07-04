using Krk.Elevators;

namespace Krk.Doors
{
    public class ElevatorDoorController
    {
        readonly FloorConfig config;
        readonly DoorController doorController;
        readonly ElevatorController elevatorController;

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

            doorController.Open();
            doorController.SetLocked(true);
        }

        public void UnlockAndClose()
        {
            doorController.SetLocked(false);
            doorController.Close();
        }
    }
}