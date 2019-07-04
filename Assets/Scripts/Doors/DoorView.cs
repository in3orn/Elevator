using Krk.Common.Elements;
using UnityEngine;

namespace Krk.Doors
{
    public class DoorView : MonoBehaviour
    {
        [SerializeField] ShowableElement door;

        public void Init(DoorState state)
        {
            door.Init(state.open);
        }

        public void Open()
        {
            door.Show();
        }

        public void Close()
        {
            door.Hide();
        }
    }
}