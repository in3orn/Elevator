using UnityEngine;

namespace Krk.Doors
{
    public class DoorView : MonoBehaviour
    {
        [SerializeField] private Transform door;

        Vector3 closedScale;
        Vector3 openScale;

        public void Init(DoorState state)
        {
            closedScale = door.localScale;
            openScale = door.localScale;
            openScale.x = 0.1f;
            door.localScale = state.open ? openScale : closedScale;
        }

        public void Open()
        {
            door.localScale = openScale;
        }

        public void Close()
        {
            door.localScale = closedScale;
        }
    }
}