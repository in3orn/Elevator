using System;
using UnityEngine;

namespace Krk.Audio
{
    public enum SoundType
    {
        ElevatorStart = 0,
        ElevatorEnd
    }
    
    [Serializable]
    public class SoundData
    {
        public SoundType type;
        public AudioClip clip;
    }
}