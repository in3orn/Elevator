using Krk.Common.Elements;
using UnityEngine;
using UnityEngine.Events;

namespace Krk.Doors
{
    public class DoorView : MonoBehaviour
    {
        public UnityAction OnOpenFinished
        {
            get => door.OnShowFinished;
            set => door.OnShowFinished = value;
        }
        
        public UnityAction OnCloseFinished
        {
            get => door.OnHideFinished;
            set => door.OnHideFinished = value;
        }
        
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