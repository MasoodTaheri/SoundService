using System;
using UnityEngine;

namespace AudioSystem
{
    [Serializable]
    public class SoundConfig
    {
       public string name;
       public AudioClip clip;
       public float volume = 1f;
    }
}