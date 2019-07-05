using System.Collections;
using Krk.Audio;
using UnityEngine;
using Zenject;

namespace Krk.Elevators
{
    public class ElevatorMediator : MonoBehaviour
    {
        [SerializeField] ElevatorView view;

        [Inject] ElevatorController controller;
        [Inject] SoundController soundController;
        [Inject] MusicController musicController;

        public ElevatorController Controller => controller;

        Coroutine waitIdleCoroutine;

        void Start()
        {
            controller.Init();
            view.Init(controller.CurrentFloor.y);
        }

        void OnEnable()
        {
            controller.OnStateChanged += HandleElevatorStateChanged;

            view.OnMoveFinished += HandleElevatorMoveFinished;
        }

        void OnDisable()
        {
            controller.OnStateChanged -= HandleElevatorStateChanged;

            view.OnMoveFinished -= HandleElevatorMoveFinished;
        }

        void HandleElevatorStateChanged(ElevatorState state)
        {
            if (state == ElevatorState.WaitingForGoingIdle)
            {
                waitIdleCoroutine = StartCoroutine(WaitIdle());
            }
            else
            {
                if (waitIdleCoroutine != null)
                {
                    StopCoroutine(waitIdleCoroutine);
                    waitIdleCoroutine = null;
                }

                if (state == ElevatorState.Running)
                {
                    view.Move(controller.CurrentFloor.y);
                    soundController.PlaySound(SoundType.ElevatorStart);
                    musicController.Play();
                }
                else if (state == ElevatorState.GoingIdle)
                {
                    view.Move(controller.CurrentFloor.y);
                }
                else if (state == ElevatorState.WaitingForPassengers)
                {
                    StartCoroutine(WaitForPassengers());
                }
                else if (state == ElevatorState.WaitingForGoingIdle)
                {
                    waitIdleCoroutine = StartCoroutine(WaitIdle());
                    soundController.PlaySound(SoundType.ElevatorEnd);
                    musicController.Pause();
                }
                else if (state == ElevatorState.WaitingForDoorOpen)
                {
                    soundController.PlaySound(SoundType.ElevatorEnd);
                    musicController.Pause();
                }
            }
        }

        void HandleElevatorMoveFinished()
        {
            controller.FinishMove();
        }

        IEnumerator WaitForPassengers()
        {
            yield return new WaitForSeconds(controller.Config.waitForPassengersDuration);
            controller.State = ElevatorState.WaitingForDoorClose;
        }

        IEnumerator WaitIdle()
        {
            yield return new WaitForSeconds(controller.Config.waitIdleDuration);
            controller.TryGoIdle();
        }
    }
}