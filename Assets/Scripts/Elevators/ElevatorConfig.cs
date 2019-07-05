using UnityEngine;
using UnityEngine.Serialization;

namespace Krk.Elevators
{
    [CreateAssetMenu(menuName = "Krk/Elevators/Elevator")]
    public class ElevatorConfig : ScriptableObject
    {
        public float speed;
        public float waitForPassengersDuration;
        public float waitIdleDuration;
        public int defaultFloorIndex;
        public FloorData[] floors;
    }
}