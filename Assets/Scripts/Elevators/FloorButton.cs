using Krk.Common.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Elevators
{
    public enum FloorButtonState
    {
        Default = 0,
        Selected,
        Queued
    }

    public class FloorButton : DataButton<FloorData>
    {
        [SerializeField] TextMeshProUGUI label;
        [SerializeField] Image border;

        FloorButtonState state;

        public FloorButtonState State
        {
            set
            {
                if (state == value) return;

                state = value;
                switch (value)
                {
                    case FloorButtonState.Default:
                        label.color = Color.white;
                        border.color = Color.white;
                        break;
                    case FloorButtonState.Queued:
                        label.color = Color.yellow;
                        border.color = Color.yellow;
                        break;
                    case FloorButtonState.Selected:
                        label.color = Color.green;
                        border.color = Color.green;
                        break;
                }
            }
        }

        public override void Init(FloorData data)
        {
            base.Init(data);

            label.text = $"{data.number} {data.name}";
        }
    }
}