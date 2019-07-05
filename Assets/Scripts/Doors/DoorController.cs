using UnityEngine.Events;

namespace Krk.Doors
{
    public class DoorController
    {
        public UnityAction OnOpenStarted;
        public UnityAction OnOpenFinished;
        public UnityAction OnCloseStarted;
        public UnityAction OnCloseFinished;
        
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
            state.running = false;
        }

        public void OpenStart()
        {
            if (state.locked) return;
            if (state.open) return;

            state.open = true;
            state.running = true;
            
            OnOpenStarted?.Invoke();
        }

        public void OpenFinish()
        {
            state.running = false;
            
            OnOpenFinished?.Invoke();
        }

        public void CloseStart()
        {
            if (state.locked) return;
            if (!state.open) return;

            state.open = false;
            state.running = true;
            
            OnCloseStarted?.Invoke();
        }

        public void CloseFinish()
        {
            state.running = false;
            
            OnCloseFinished?.Invoke();
        }

        public void SetLocked(bool value)
        {
            state.locked = value;
        }
    }
}