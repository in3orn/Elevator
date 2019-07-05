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

        public void Show(IList<FloorData> floors)
        {
            Unsubscribe();
            dynamicContent.Init(buttonsContent, buttonTemplate, buttons, floors);
            resetButton.transform.SetAsLastSibling();
            Subscribe();
            
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
    }
}