using UnityEngine;
using Zenject;

namespace Krk.Elevators
{
    public class ElevatorMediator : MonoBehaviour
    {
        [SerializeField] ElevatorView view;

        [Inject] ElevatorController controller;

        public ElevatorController Controller => controller;

        void Start()
        {
            controller.Init();
            view.Init(controller.CurrentFloor.y);
        }

        void OnEnable()
        {
            controller.OnMoveStarted += HandleElevatorMoved;

            view.OnMoveFinished += HandleElevatorMoveFinished;
        }

        void OnDisable()
        {
            controller.OnMoveStarted -= HandleElevatorMoved;

            view.OnMoveFinished -= HandleElevatorMoveFinished;
        }

        void HandleElevatorMoved(FloorData data)
        {
            view.Move(data.y);
        }

        void HandleElevatorMoveFinished()
        {
            controller.FinishMove();
        }
    }
}