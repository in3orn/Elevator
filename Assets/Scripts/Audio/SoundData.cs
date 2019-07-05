using System;
using UnityEngine;

namespace Krk.Audio
{
    public enum SoundType
    {
        ElevatorStart = 0,
        ElevatorEnd,
        DoorOpen,
        DoorClose
    }
    
    [Serializable]
    public class SoundData
    {
        public SoundType type;
        public AudioClip clip;
        public float volume;
    }
}