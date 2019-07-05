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

            elevatorController.OnStateChanged += HandleElevatorStateChanged;
        }

        void OnDisable()
        {
            trigger.OnActivated -= HandleTriggerActivated;
            trigger.OnDeactivated -= HandleTriggerDeactivated;

            controller.OnCanSwitchChanged -= HandleCanSwitchChanged;
            controller.OnSwitchedOn -= HandleButtonSwitchedOn;
            controller.OnSwitchedOff -= HandleButtonSwitchedOff;

            elevatorController.OnStateChanged -= HandleElevatorStateChanged;
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

        void HandleElevatorStateChanged(ElevatorState state)
        {
            if(state == ElevatorState.WaitingForDoorOpen)
                controller.TrySwitchOff(elevatorController.CurrentFloorIndex);    
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