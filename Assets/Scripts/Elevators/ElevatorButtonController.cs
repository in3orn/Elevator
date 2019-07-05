using Krk.Doors;
using UnityEngine.Events;

namespace Krk.Elevators
{
    public class ElevatorButtonController
    {
        public UnityAction<bool> OnCanSwitchChanged;
        public UnityAction OnSwitchedOn;
        public UnityAction OnSwitchedOff;

        readonly FloorConfig config;
        readonly ElevatorController elevatorController;
        readonly ElevatorDoorController elevatorDoorController;

        bool switchedOn;
        bool inRange;
        bool canUse;

        public bool CanUse => canUse;
        public bool SwitchedOn => switchedOn;

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
            OnSwitchedOn?.Invoke();
            
            //TODO should not be here - use mediator OnSwitchedOn
            elevatorController.AddTargetFloor(config.floorIndex);

            canUse = false;
            OnCanSwitchChanged?.Invoke(canUse);
        }

        public void TrySwitchOff(int floorIndex)
        {
            if (floorIndex == config.floorIndex)
            {
                switchedOn = false;
                OnSwitchedOff?.Invoke();
            }
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