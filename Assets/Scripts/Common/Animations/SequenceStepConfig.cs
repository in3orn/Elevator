using DG.Tweening;
using UnityEngine;

namespace Krk.Common.Animations
{
    [CreateAssetMenu(menuName = "Krk/View/Common/Animations/Sequence Step Config")]
    public class SequenceStepConfig : ScriptableObject
    {
        public SequenceStepType type;
        public Ease ease = Ease.Linear;
        public float delay;
        public float duration;
        public Vector3 value;
        public Color color;
        public bool from;
        public bool relative;

        public SequenceStepConfig CopyDeeply()
        {
            return Instantiate(this);
        }
    }

    public enum SequenceStepType
    {
        Scale,
        Move,
        Rotate,
        Fade,
        FlexibleSize,
        PreferredSize,
        Color
    }
}