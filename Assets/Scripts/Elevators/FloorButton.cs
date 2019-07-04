using Krk.Common.View;
using TMPro;
using UnityEngine;

namespace Krk.Elevators
{
    public class FloorButton : DataButton<FloorData>
    {
        [SerializeField] TextMeshProUGUI label;

        public override void Init(FloorData data)
        {
            base.Init(data);

            label.text = $"{data.number} {data.name}";
        }
    }
}