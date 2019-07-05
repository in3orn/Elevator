using UnityEngine;
using Zenject;

namespace Krk.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] AudioSource source;

        [Inject] MusicController controller;

        void Awake()
        {
            source.Play();
            source.Pause();
        }

        void OnEnable()
        {
            controller.OnMusicPlayed += HandleMusicPlayed;
            controller.OnMusicStopped += HandleMusicStopped;
        }

        void OnDisable()
        {
            controller.OnMusicPlayed -= HandleMusicPlayed;
            controller.OnMusicStopped -= HandleMusicStopped;
        }

        void HandleMusicPlayed()
        {
            source.UnPause();
        }

        void HandleMusicStopped()
        {
            source.Pause();
        }
    }
}