using Krk.Common.Elements;
using UnityEngine;
using Zenject;

namespace Krk.Elevators
{
    public class ElevatorButtonMediator : MonoBehaviour
    {
        [SerializeField] ShowableElement actionView;
        [SerializeField] ShowableElement buttonIndicator;
        [SerializeField] Trigger trigger;

        [Inject] ElevatorButtonController controller;
        [Inject] ElevatorController elevatorController;

        void Start()
        {
            buttonIndicator.Init(false);
        }

        void OnEnable()
        {
            trigger.OnActivated += HandleTriggerActivated;
            trigger.OnDeactivated += HandleTriggerDeactivated;

            controller.OnCanSwitchChanged += HandleCanSwitchChanged;
            controller.OnSwitchedOn += HandleButtonSwitchedOn;
            controller.OnSwitchedOff += HandleButtonSwitchedOff;

            elevatorController.OnMoveFinished += HandleElevatorMoveFinished;
        }

        void OnDisable()
        {
            trigger.OnActivated -= HandleTriggerActivated;
            trigger.OnDeactivated -= HandleTriggerDeactivated;

            controller.OnCanSwitchChanged -= HandleCanSwitchChanged;
            controller.OnSwitchedOn -= HandleButtonSwitchedOn;
            controller.OnSwitchedOff -= HandleButtonSwitchedOff;

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
                actionView.Show();
            else
                actionView.Hide();
        }

        void HandleButtonSwitchedOn()
        {
            buttonIndicator.Show();
        }

        void HandleButtonSwitchedOff()
        {
            buttonIndicator.Hide();
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