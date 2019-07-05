using System.Collections.Generic;
using ModestTree;
using UnityEngine.Events;

namespace Krk.Elevators
{
    public class ElevatorController
    {
        public UnityAction<FloorData> OnMoveStarted;
        public UnityAction<int> OnMoveFinished;
        public UnityAction OnWaitStarted;
        public UnityAction OnWaitFinished;

        readonly ElevatorConfig config;
        readonly IList<int> queue;

        int currentFloorIndex;
        bool running;
        bool waiting;

        public bool Running => running;
        public bool Waiting => waiting;

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
            AddTargetFloor(config.floors.IndexOf(data));
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
            if (running) return;

            while (queue.Count > 0)
            {
                var nextFloorIndex = queue[0];
                queue.RemoveAt(0);
                if (currentFloorIndex != nextFloorIndex)
                {
                    currentFloorIndex = nextFloorIndex;
                    running = true;

                    OnMoveStarted?.Invoke(config.floors[currentFloorIndex]);
                    return;
                }
            }
        }

        public void FinishMove()
        {
            running = false;
            OnMoveFinished?.Invoke(currentFloorIndex);
        }

        public void WaitStart()
        {
            waiting = true;
            OnWaitStarted?.Invoke();
        }

        public void WaitFinish()
        {
            waiting = false;
            OnWaitFinished?.Invoke();
        }

        public bool IsOnFloor(int floorIndex)
        {
            return !Running && currentFloorIndex == floorIndex;
        }
    }
}