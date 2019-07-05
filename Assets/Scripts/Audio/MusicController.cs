using UnityEngine.Events;

namespace Krk.Audio
{
    public class MusicController
    {
        public UnityAction OnMusicPlayed;
        public UnityAction OnMusicStopped;
        
        public void Play()
        {
            OnMusicPlayed?.Invoke();
        }

        public void Pause()
        {
            OnMusicStopped?.Invoke();
        }
    }
}