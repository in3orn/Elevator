using UnityEngine;

namespace Krk.Audio
{
    [CreateAssetMenu(menuName = "Krk/Audio/Sound")]
    public class SoundConfig : ScriptableObject
    {
        public SoundData[] sounds;
    }
}