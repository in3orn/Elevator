using System.Collections;
using UnityEngine;
using Zenject;

namespace Krk.Elevators
{
    public class ElevatorMediator : MonoBehaviour
    {
        [SerializeField] ElevatorView view;
        
        [Inject] ElevatorController controller;

        public ElevatorController Controller => controller;

        Coroutine waitIdleCoroutine;

        void Start()
        {
            controller.Init();
            view.Init(controller.CurrentFloor.y);
        }

        void OnEnable()
        {
            controller.OnMoveStarted += HandleElevatorMoved;
            controller.OnWaitForDoorStarted += HandleElevatorWaitForDoorStarted;
            controller.OnWaitIdleStarted += HandleElevatorWaitIdleStarted;
            controller.OnWaitIdleStopped += HandleElevatorWaitIdleStopped;

            view.OnMoveFinished += HandleElevatorMoveFinished;
        }

        void OnDisable()
        {
            controller.OnMoveStarted -= HandleElevatorMoved;
            controller.OnWaitForDoorStarted -= HandleElevatorWaitForDoorStarted;
            controller.OnWaitIdleStarted -= HandleElevatorWaitIdleStarted;
            controller.OnWaitIdleStopped -= HandleElevatorWaitIdleStopped;

            view.OnMoveFinished -= HandleElevatorMoveFinished;  
        }

        void HandleElevatorMoved(FloorData data)
        {
            view.Move(data.y);
        }

        void HandleElevatorWaitForDoorStarted()
        {
            StartCoroutine(WaitForDoor());
        }

        void HandleElevatorWaitIdleStarted()
        {
            waitIdleCoroutine = StartCoroutine(WaitIdle());
        }

        void HandleElevatorWaitIdleStopped()
        {
            StopCoroutine(waitIdleCoroutine);
        }

        void HandleElevatorMoveFinished()
        {
            controller.FinishMove();
        }

        IEnumerator WaitForDoor()
        {
            yield return new WaitForSeconds(controller.Config.waitForDoorDuration);
            controller.WaitForDoorFinish();
        }

        IEnumerator WaitIdle()
        {
            yield return new WaitForSeconds(controller.Config.waitIdleDuration);
            controller.WaitIdleFinish();
        }
    }
}