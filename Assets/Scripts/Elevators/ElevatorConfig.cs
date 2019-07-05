using UnityEngine;
using UnityEngine.Serialization;

namespace Krk.Elevators
{
    [CreateAssetMenu(menuName = "Krk/Elevators/Elevator")]
    public class ElevatorConfig : ScriptableObject
    {
        public float speed;
        [FormerlySerializedAs("waitDuration")] public float waitForDoorDuration;
        public float waitIdleDuration;
        public int defaultFloorIndex;
        public FloorData[] floors;
    }
}