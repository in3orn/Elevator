using System.Collections.Generic;
using UnityEngine;

namespace Krk.Common.Animations
{
    [CreateAssetMenu(menuName = "Krk/View/Common/Animations/Sequence Steps Config")]
    public class SequenceItemConfig : ScriptableObject
    {
        public List<SequenceStepConfig> steps;

        public SequenceItemConfig()
        {
            steps = new List<SequenceStepConfig>();
        }

        public SequenceItemConfig CopyDeeply()
        {
            var result = Instantiate(this);
            result.steps.Clear();
            foreach (var step in steps)
            {
                result.steps.Add(step.CopyDeeply());
            }

            return result;
        }
    }
}