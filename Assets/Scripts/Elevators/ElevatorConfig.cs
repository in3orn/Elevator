using UnityEngine;

namespace Krk.Elevators
{
    [CreateAssetMenu(menuName = "Krk/Elevators/Elevator")]
    public class ElevatorConfig : ScriptableObject
    {
        public float speed;
        public int defaultFloorIndex;
        public FloorData[] floors;
    }
}