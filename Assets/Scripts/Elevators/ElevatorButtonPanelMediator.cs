using Krk.Common.Elements;
using UnityEngine;

namespace Krk.Elevators
{
    public class ElevatorButtonPanelMediator : MonoBehaviour
    {
        [SerializeField] ShowableElement view;
        
        void Start()
        {
            view.Init(false);
        }
    }
}