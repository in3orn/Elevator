using UnityEngine.Events;

namespace Krk.Elevators
{
    public class ElevatorButtonController
    {
        public UnityAction<bool> OnCanSwitchChanged;

        readonly FloorConfig config;
        readonly ElevatorController elevatorController;

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

        public ElevatorButtonController(FloorConfig config, ElevatorController elevatorController)
        {
            this.config = config;
            this.elevatorController = elevatorController;
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
            var newCanUse = inRange && !switchedOn && !elevatorController.IsOnFloor(config.floorIndex);
            if (canUse != newCanUse)
            {
                canUse = newCanUse;
                OnCanSwitchChanged?.Invoke(canUse);
            }
        }
    }
}