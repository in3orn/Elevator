using System.Linq;
using UnityEngine;
using Zenject;

namespace Krk.Audio
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] AudioSource source;
        [SerializeField] SoundConfig config;

        [Inject] SoundController controller;

        void OnEnable()
        {
            controller.OnSoundPlayed += HandleSoundPlayed;
        }

        void OnDisable()
        {
            controller.OnSoundPlayed -= HandleSoundPlayed;
        }

        void HandleSoundPlayed(SoundType type)
        {
            var sound = GetSound(type);
            source.clip = sound.clip;
            source.Play();
        }

        SoundData GetSound(SoundType type)
        {
            return config.sounds.FirstOrDefault(sound => sound.type == type);
        }
    }
}