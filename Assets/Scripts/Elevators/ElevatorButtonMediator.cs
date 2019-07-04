using Krk.Common.Elements;
using UnityEngine;
using Zenject;

namespace Krk.Elevators
{
    public class ElevatorButtonMediator : MonoBehaviour
    {
        [SerializeField] ShowableElement view;
        [SerializeField] Trigger trigger;

        [Inject] ElevatorButtonController controller;
        [Inject] ElevatorController elevatorController;

        void Start()
        {
            view.Init(false);
        }

        void OnEnable()
        {
            trigger.OnActivated += HandleTriggerActivated;
            trigger.OnDeactivated += HandleTriggerDeactivated;

            controller.OnCanSwitchChanged += HandleCanSwitchChanged;

            elevatorController.OnMoveFinished += HandleElevatorMoveFinished;
        }

        void OnDisable()
        {
            trigger.OnActivated -= HandleTriggerActivated;
            trigger.OnDeactivated -= HandleTriggerDeactivated;

            controller.OnCanSwitchChanged -= HandleCanSwitchChanged;

            elevatorController.OnMoveFinished -= HandleElevatorMoveFinished;
        }

        void HandleTriggerActivated()
        {
            controller.InRange = true;
        }

        void HandleTriggerDeactivated()
        {
            controller.InRange = false;
        }

        void HandleCanSwitchChanged(bool canSwitch)
        {
            if (canSwitch)
                view.Show();
            else
                view.Hide();
        }

        void HandleElevatorMoveFinished(int floorIndex)
        {
            controller.TrySwitchOff(floorIndex);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && controller.CanUse)
            {
                controller.SwitchOn();
            }
        }
    }
}