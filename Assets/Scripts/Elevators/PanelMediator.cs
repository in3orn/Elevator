using UnityEngine;
using Zenject;

namespace Krk.Elevators
{
    public class PanelMediator : MonoBehaviour
    {
        [SerializeField] PanelView view;

        [Inject] ElevatorController controller;

        void Start()
        {
            view.Init(controller.Config.floors);
        }

        void OnEnable()
        {
            view.OnFloorButtonClicked += HandleFloorButtonClicked;
            view.resetButton.onClick.AddListener(HandleResetButtonClicked);
        }

        void OnDisable()
        {
            view.OnFloorButtonClicked -= HandleFloorButtonClicked;
            view.resetButton.onClick.RemoveListener(HandleResetButtonClicked);
        }

        void HandleFloorButtonClicked(FloorData data)
        {
            controller.AddTargetFloor(data);
        }

        void HandleResetButtonClicked()
        {
            controller.Reset();
        }
    }
}