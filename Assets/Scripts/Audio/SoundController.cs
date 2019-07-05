using UnityEngine.Events;

namespace Krk.Audio
{
    public class SoundController
    {
        public UnityAction<SoundType> OnSoundPlayed;

        public void PlaySound(SoundType type)
        {
            OnSoundPlayed?.Invoke(type);
        }
    }
}