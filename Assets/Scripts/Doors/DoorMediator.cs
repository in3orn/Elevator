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
            controller.OnOpened += HandleDoorOpened;
            controller.OnClosed += HandleDoorClosed;
        }

        void OnDisable()
        {
            controller.OnOpened -= HandleDoorOpened;
            controller.OnClosed -= HandleDoorClosed;
        }

        public void HandleDoorOpened()
        {
            view.Open();
        }

        public void HandleDoorClosed()
        {
            view.Close();
        }
    }
}