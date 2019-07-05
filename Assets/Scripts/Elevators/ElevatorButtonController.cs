using Krk.Doors;
using UnityEngine.Events;

namespace Krk.Elevators
{
    public class ElevatorButtonController
    {
        public UnityAction<bool> OnCanSwitchChanged;

        readonly FloorConfig config;
        readonly ElevatorController elevatorController;
        readonly ElevatorDoorController elevatorDoorController;

        bool switchedOn;
        bool inRange;
        bool canUse;

        public bool CanUse => canUse;

        public bool InRange
        {
            set
            {
                if (inRange != value)
                {
                    inRange = value;
                    UpdateCanUse();
                }
            }
        }

        public ElevatorButtonController(FloorConfig config, ElevatorController elevatorController,
            ElevatorDoorController elevatorDoorController)
        {
            this.config = config;
            this.elevatorController = elevatorController;
            this.elevatorDoorController = elevatorDoorController;
        }

        public void SwitchOn()
        {
            if (switchedOn) return;

            switchedOn = true;
            elevatorController.AddTargetFloor(config.floorIndex);

            canUse = false;
            OnCanSwitchChanged?.Invoke(canUse);
        }

        public void TrySwitchOff(int floorIndex)
        {
            if (floorIndex == config.floorIndex)
                switchedOn = false;
        }

        void UpdateCanUse()
        {
            var newCanUse = inRange && !switchedOn && !elevatorDoorController.State.open;
            if (canUse != newCanUse)
            {
                canUse = newCanUse;
                OnCanSwitchChanged?.Invoke(canUse);
            }
        }
    }
}