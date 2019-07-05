using UnityEngine;

namespace Krk.Elevators
{
    public class PanelMediator : MonoBehaviour
    {
        [SerializeField] PanelView view;

        void Start()
        {
            view.Init();
        }
    }
}