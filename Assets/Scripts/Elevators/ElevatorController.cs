using System.Collections.Generic;
using UnityEngine.Events;

namespace Krk.Elevators
{
    public enum ElevatorState
    {
        Idle = 0,
        Running,
        WaitingForDoorOpen,
        WaitingForPassengers,
        WaitingForDoorClose,
        WaitingForGoingIdle,
        GoingIdle
    }

    public class ElevatorController
    {
        public UnityAction<ElevatorState> OnStateChanged;

        readonly ElevatorConfig config;
        readonly IList<int> queue;

        int currentFloorIndex;
        ElevatorState state;

        public int CurrentFloorIndex => currentFloorIndex;
        public FloorData CurrentFloor => config.floors[currentFloorIndex];
        public ElevatorConfig Config => config;

        public ElevatorState State
        {
            get => state;
            set
            {
                if (state == value) return;

                state = value;
                OnStateChanged?.Invoke(state);
            }
        }

        public ElevatorController(ElevatorConfig config)
        {
            this.config = config;

            queue = new List<int>();
        }

        public void Init()
        {
            currentFloorIndex = config.defaultFloorIndex;
        }

        public void AddTargetFloor(FloorData data)
        {
            AddTargetFloor(GetIndex(data));
        }

        int GetIndex(FloorData data)
        {
            for (var i = 0; i < config.floors.Length; i++)
            {
                if (config.floors[i].number.Equals(data.number))
                    return i;
            }

            return -1;
        }

        public void AddTargetFloor(int floorIndex)
        {
            if (queue.Count > 0)
            {
                if (!queue.Contains(floorIndex))
                {
                    queue.Add(floorIndex);
                }
            }
            else
            {
                if (floorIndex != currentFloorIndex)
                {
                    queue.Add(floorIndex);
                    TryMove();
                }
                else
                {
                    State = ElevatorState.WaitingForDoorOpen;
                }
            }
        }

        public void Reset()
        {
            queue.Clear();
        }

        public void TryMove()
        {
            if (!CanMove()) return;

            while (queue.Count > 0)
            {
                var nextFloorIndex = queue[0];
                queue.RemoveAt(0);
                if (currentFloorIndex != nextFloorIndex)
                {
                    currentFloorIndex = nextFloorIndex;
                    State = ElevatorState.Running;
                    return;
                }
            }

            if (currentFloorIndex != config.defaultFloorIndex)
            {
                State = ElevatorState.WaitingForGoingIdle;
            }
            else
            {
                State = ElevatorState.Idle;
            }
        }

        bool CanMove()
        {
            return State == ElevatorState.Idle || State == ElevatorState.WaitingForGoingIdle;
        }

        public void TryGoIdle()
        {
            if (currentFloorIndex != config.defaultFloorIndex)
            {
                currentFloorIndex = config.defaultFloorIndex;
                State = ElevatorState.GoingIdle;
            }
        }

        public bool IsOnFloor(int floorIndex)
        {
            return State != ElevatorState.Running && currentFloorIndex == floorIndex;
        }

        public void FinishMove()
        {
            State = State == ElevatorState.Running ? ElevatorState.WaitingForDoorOpen : ElevatorState.Idle;
            TryMove();
        }
    }
}