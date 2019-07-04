using UnityEngine.Events;

namespace Krk.Doors
{
    public class DoorController
    {
        public UnityAction OnOpened;
        public UnityAction OnClosed;
        
        readonly DoorState state;

        public DoorState State => state;

        public DoorController()
        {
            state = new DoorState();
        }

        public void Init()
        {
            state.open = false;
            state.locked = false;
        }

        public void Open()
        {
            if (state.locked) return;
            if (state.open) return;

            state.open = true;
            
            OnOpened?.Invoke();
        }

        public void Close()
        {
            if (state.locked) return;
            if (!state.open) return;

            state.open = false;
            
            OnClosed?.Invoke();
        }

        public void SetLocked(bool value)
        {
            state.locked = value;
        }
    }
}