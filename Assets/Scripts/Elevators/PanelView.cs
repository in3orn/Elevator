using System.Collections.Generic;
using Krk.Common.Elements;
using Krk.Common.View;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Krk.Elevators
{
    public class PanelView : MonoBehaviour
    {
        public UnityAction<FloorData> OnFloorButtonClicked;

        public Button resetButton;

        [SerializeField] Trigger trigger;
        [SerializeField] ShowableElement panel;

        [SerializeField] RectTransform buttonsContent;
        [SerializeField] FloorButton buttonTemplate;

        [Inject] DynamicContent dynamicContent;

        readonly IList<FloorButton> buttons;

        public PanelView()
        {
            buttons = new List<FloorButton>();
        }

        public void Init(IList<FloorData> floors)
        {
            panel.Init(false);

            dynamicContent.Init(buttonsContent, buttonTemplate, buttons, floors);

            foreach (var button in buttons)
            {
                button.OnClicked += HandleFloorButtonClicked;
            }

            resetButton.transform.SetAsLastSibling();
        }

        void OnEnable()
        {
            trigger.OnActivated += HandleTriggerActivated;
            trigger.OnDeactivated += HandleTriggerDeactivated;

            foreach (var button in buttons)
            {
                button.OnClicked -= HandleFloorButtonClicked;
            }
        }

        void OnDisable()
        {
            trigger.OnActivated -= HandleTriggerActivated;
            trigger.OnDeactivated -= HandleTriggerDeactivated;

            foreach (var button in buttons)
            {
                button.OnClicked -= HandleFloorButtonClicked;
            }
        }

        void HandleTriggerActivated()
        {
            panel.Show();
        }

        void HandleTriggerDeactivated()
        {
            panel.Hide();
        }

        void HandleFloorButtonClicked(FloorData data)
        {
            OnFloorButtonClicked?.Invoke(data);
        }
    }
}