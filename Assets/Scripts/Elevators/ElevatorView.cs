using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Krk.Elevators
{
    public class ElevatorView : MonoBehaviour
    {
        public UnityAction OnMoveFinished;
        
        [SerializeField] Transform platform;

        [Inject] ElevatorConfig config;

        public void Init(float y)
        {
            var position = platform.position;
            position.y = y;
            platform.position = position;
        }
        
        public void Move(float y)
        {
            var distance = Mathf.Abs(platform.position.y - y);
            var duration = distance / config.speed;
            platform.DOMoveY(y, duration).SetEase(Ease.Linear).OnComplete(FinishMove);
        }

        void FinishMove()
        {
            OnMoveFinished?.Invoke();
        }
    }
}