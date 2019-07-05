using System.Collections;
using DG.Tweening;
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
            controller.OnWaitStarted += HandleElevatorWaitStarted;

            view.OnMoveFinished += HandleElevatorMoveFinished;
        }

        void OnDisable()
        {
            controller.OnMoveStarted -= HandleElevatorMoved;
            controller.OnWaitStarted -= HandleElevatorWaitStarted;

            view.OnMoveFinished -= HandleElevatorMoveFinished;
        }

        void HandleElevatorMoved(FloorData data)
        {
            view.Move(data.y);
        }
        
        void HandleElevatorWaitStarted()
        {
//            DOVirtual.DelayedCall(controller.Config.waitDuration, Aaa);
            StartCoroutine(Wait());
        }

        void HandleElevatorMoveFinished()
        {
            controller.FinishMove();
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(controller.Config.waitDuration);
            controller.WaitFinish();
        }
    }
}