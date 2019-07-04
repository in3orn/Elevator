using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace Krk.Elevators
{
    public class ElevatorController
    {
        public UnityAction<FloorData> OnMoved;

        readonly ElevatorConfig config;
        readonly IList<int> queue;

        int currentFloorIndex;
        bool running;

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

        public void AddTargetFloor(int floorIndex)
        {
            if (queue.Count <= 0)
            {
                if (floorIndex != currentFloorIndex)
                {
                    queue.Add(floorIndex);
                    TryMove();
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

                    OnMoved?.Invoke(config.floors[currentFloorIndex]);
                    return;
                }
            }
        }

        public void FinishMove()
        {
            running = false;
            TryMove();
        }
    }
}