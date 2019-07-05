using UnityEngine;
using Zenject;

namespace Krk.Doors
{
    public class DoorMediator : MonoBehaviour
    {
        [SerializeField] DoorView view;

        [Inject] DoorController controller;

        void Start()
        {
            view.Init(controller.State);
        }

        void OnEnable()
        {
            controller.OnOpenStarted += HandleDoorOpened;
            controller.OnCloseStarted += HandleDoorClosed;

            view.OnOpenFinished += HandleDoorOpenFinished;
            view.OnCloseFinished += HandleDoorCloseFinished;
        }

        void OnDisable()
        {
            controller.OnOpenStarted -= HandleDoorOpened;
            controller.OnCloseStarted -= HandleDoorClosed;

            view.OnOpenFinished -= HandleDoorOpenFinished;
            view.OnCloseFinished -= HandleDoorCloseFinished;
        }

        public void HandleDoorOpened()
        {
            view.Open();
        }

        public void HandleDoorClosed()
        {
            view.Close();
        }

        void HandleDoorOpenFinished()
        {
            controller.OpenFinish();
        }

        void HandleDoorCloseFinished()
        {
            controller.CloseFinish();
        }
    }
}