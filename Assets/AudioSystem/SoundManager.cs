using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace AudioSystem
{
    [CreateAssetMenu(fileName = "SfxPlayer", menuName = "AudioSystem/SfxPlayer")]
    public partial class SoundManager : ScriptableObject
    {
        [Range(-80, 20), SerializeField] private float audioMinValue;
        [Range(-80, 20), SerializeField] private float audioMaxValue;

        [SerializeField] private string musicVolumeExposedName;
        [SerializeField] private string sfxGVolumeExposedName;
        [SerializeField] private AudioMixer audioMixer;

        [Space, Header("SFx List")] [SerializeField]
        private List<SoundConfig> sfxConfigs = new List<SoundConfig>();

        [Space, Header("Music List")] [SerializeField]
        private List<MusicConfig> musicConfigs = new List<MusicConfig>();
        private SoundPlayer _soundPlayer;

        private string _currentMusicName;

        public void Setup()
        {
            _soundPlayer = new SoundPlayer(2);
        }

        public void Play(string sound, Action onEnd = null)
        {
            SoundConfig clipConfig = sfxConfigs.Find(x => x.name == sound);
            if (clipConfig != null)
            {
                AudioClip cip = clipConfig.clip;
                _soundPlayer.Play(cip,clipConfig.volume,onEnd);
            }
        }

        public void PlayRandomLoopMusic()
        {
            _currentMusicName = musicConfigs[Random.Range(0, musicConfigs.Count)].name;
            Play(_currentMusicName, PlayRandomLoopMusic);
        }

        public void StopMusic()
        {
            _soundPlayer.Stop(musicConfigs.Find(x => x.name == _currentMusicName).clip);
        }


        public void MuteMusic()
        {
            MusicChangeVolume(audioMinValue);
        }

        public void MusicChangeVolume(float volume)
        {
            audioMixer.SetFloat(musicVolumeExposedName, (volume < audioMaxValue) ? volume : audioMaxValue);
        }

        public void MuteSfx()
        {
            SfxChangeVolume(audioMinValue);
        }

        public void SfxChangeVolume(float volume)
        {
            audioMixer.SetFloat(sfxGVolumeExposedName, (volume < audioMaxValue) ? volume : audioMaxValue);
        }
    }
}