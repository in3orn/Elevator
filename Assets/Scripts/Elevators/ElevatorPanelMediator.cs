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
        }

        void OnDisable()
        {
            trigger.OnActivated -= HandlePanelTriggerActivated;
            trigger.OnDeactivated -= HandlePanelTriggerDeactivated;

            view.resetButton.onClick.RemoveListener(HandleResetButtonClicked);
        }

        void HandlePanelTriggerActivated()
        {
            view.Show(controller.Config.floors);
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
        }

        void HandleResetButtonClicked()
        {
            controller.Reset();
        }
    }
}