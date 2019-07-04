using UnityEngine;

namespace Krk.Common.Elements
{
    [CreateAssetMenu(menuName = "Krk/Common/Elements/Trigger")]
    public class TriggerConfig : ScriptableObject
    {
        public int[] layers;
    }
}