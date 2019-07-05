using System.Collections.Generic;
using UnityEngine.Events;

namespace Krk.Elevators
{
    public class ElevatorController
    {
        public UnityAction<FloorData> OnMoveStarted;
        public UnityAction<int> OnMoveFinished;
        public UnityAction OnWaitForDoorStarted;
        public UnityAction OnWaitForDoorFinished;
        public UnityAction OnWaitIdleStarted;
        public UnityAction OnWaitIdleStopped;
        public UnityAction OnWaitIdleFinished;

        readonly ElevatorConfig config;
        readonly IList<int> queue;

        int currentFloorIndex;
        bool running;
        bool waitingForDoor;
        bool waitingIdle;

        public bool Running => running;
        public bool WaitingForDoor => waitingForDoor;

        public int CurrentFloorIndex => currentFloorIndex;
        public FloorData CurrentFloor => config.floors[currentFloorIndex];
        public ElevatorConfig Config => config;

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
            if (queue.Count <= 0)
            {
                if (floorIndex != currentFloorIndex)
                {
                    queue.Add(floorIndex);
                    TryMove();
                }
                else
                {
                    FinishMove();
                }
            }
            else
            {
                if (!queue.Contains(floorIndex))
                {
                    queue.Add(floorIndex);
                }
            }
        }

        public void Reset()
        {
            queue.Clear();
        }

        public void TryMove()
        {
            if (running || waitingForDoor) return;

            while (queue.Count > 0)
            {
                var nextFloorIndex = queue[0];
                queue.RemoveAt(0);
                if (currentFloorIndex != nextFloorIndex)
                {
                    WaitIdleStop();

                    currentFloorIndex = nextFloorIndex;
                    running = true;

                    OnMoveStarted?.Invoke(config.floors[currentFloorIndex]);
                    return;
                }
            }

            if (currentFloorIndex != config.defaultFloorIndex)
                WaitIdleStart();
        }

        public void FinishMove()
        {
            running = false;
            OnMoveFinished?.Invoke(currentFloorIndex);
        }

        public void WaitForDoorStart()
        {
            waitingForDoor = true;
            OnWaitForDoorStarted?.Invoke();
        }

        public void WaitForDoorFinish()
        {
            waitingForDoor = false;
            OnWaitForDoorFinished?.Invoke();
        }

        public void WaitIdleStart()
        {
            waitingIdle = true;
            OnWaitIdleStarted?.Invoke();
        }

        public void WaitIdleStop()
        {
            if (!waitingIdle) return;

            waitingIdle = false;
            OnWaitIdleStopped?.Invoke();
        }

        public void WaitIdleFinish()
        {
            waitingIdle = false;
            if (currentFloorIndex != config.defaultFloorIndex)
            {
                queue.Add(config.defaultFloorIndex);
                TryMove();
            }

            OnWaitIdleFinished?.Invoke();
        }

        public bool IsOnFloor(int floorIndex)
        {
            return !Running && currentFloorIndex == floorIndex;
        }
    }
}