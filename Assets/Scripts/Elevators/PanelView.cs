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

        [SerializeField] ShowableElement panel;

        [SerializeField] RectTransform buttonsContent;
        [SerializeField] FloorButton buttonTemplate;

        [Inject] DynamicContent dynamicContent;

        readonly IList<FloorButton> buttons;

        public PanelView()
        {
            buttons = new List<FloorButton>();
        }

        public void Init()
        {
            panel.Init(false);
        }

        public void Show(IList<FloorData> floors, IList<int> queued, int selected)
        {
            Unsubscribe();
            dynamicContent.Init(buttonsContent, buttonTemplate, buttons, floors);
            resetButton.transform.SetAsLastSibling();
            Subscribe();

            Refresh(queued, selected);
            
            panel.Show();
        }

        public void Hide()
        {
            panel.Hide();
        }

        void Subscribe()
        {
            foreach (var button in buttons)
            {
                button.OnClicked += HandleFloorButtonClicked;
            }
        }

        void Unsubscribe()
        {
            foreach (var button in buttons)
            {
                button.OnClicked -= HandleFloorButtonClicked;
            }
        }

        void HandleFloorButtonClicked(FloorData data)
        {
            OnFloorButtonClicked?.Invoke(data);
        }

        public void Refresh(IList<int> queued, int selected)
        {
            for (var i = 0; i < buttons.Count; i++)
            {
                var button = buttons[i];
                if (i == selected)
                    button.State = FloorButtonState.Selected;
                else if (queued.Contains(i))
                    button.State = FloorButtonState.Queued;
                else
                    button.State = FloorButtonState.Default;
            }
        }
    }
}