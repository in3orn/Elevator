using Krk.Common.Elements;
using UnityEngine;
using Zenject;

namespace Krk.Elevators
{
    public class ElevatorPanelMediator : MonoBehaviour
    {
        [SerializeField] PanelView view;
        [SerializeField] Trigger trigger;

        [Inject] ElevatorController controller;

        void OnEnable()
        {
            trigger.OnActivated += HandlePanelTriggerActivated;
            trigger.OnDeactivated += HandlePanelTriggerDeactivated;

            view.resetButton.onClick.AddListener(HandleResetButtonClicked);

            controller.OnStateChanged += HandleElevatorStateChanged;
        }

        void OnDisable()
        {
            trigger.OnActivated -= HandlePanelTriggerActivated;
            trigger.OnDeactivated -= HandlePanelTriggerDeactivated;

            view.resetButton.onClick.RemoveListener(HandleResetButtonClicked);
        }

        void HandlePanelTriggerActivated()
        {
            view.Show(controller.Config.floors, controller.Queue, controller.CurrentFloorIndex);
            view.OnFloorButtonClicked += HandleFloorButtonClicked;
        }

        void HandlePanelTriggerDeactivated()
        {
            view.Hide();
            view.OnFloorButtonClicked -= HandleFloorButtonClicked;
        }

        void HandleFloorButtonClicked(FloorData data)
        {
            controller.AddTargetFloor(data);
            view.Refresh(controller.Queue, controller.CurrentFloorIndex);
        }

        void HandleResetButtonClicked()
        {
            controller.Reset();
            view.Refresh(controller.Queue, controller.CurrentFloorIndex);
        }

        void HandleElevatorStateChanged(ElevatorState state)
        {
            view.Refresh(controller.Queue, controller.CurrentFloorIndex);
        }
    }
}