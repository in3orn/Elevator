using Krk.Common.Elements;
using UnityEngine;

namespace Krk.Paintings
{
    public class PaintingMediator : MonoBehaviour
    {
        [SerializeField] Trigger trigger;
        [SerializeField] ShowableElement panel;

        void Start()
        {
            panel.Init(false);
        }

        void OnEnable()
        {
            trigger.OnActivated += HandleTriggerActivated;
            trigger.OnDeactivated += HandleTriggerDeactivated;
        }

        void OnDisable()
        {
            trigger.OnActivated -= HandleTriggerActivated;
            trigger.OnDeactivated -= HandleTriggerDeactivated;
        }

        void HandleTriggerActivated()
        {
            panel.Show();
        }

        void HandleTriggerDeactivated()
        {
            panel.Hide();
        }
    }
}