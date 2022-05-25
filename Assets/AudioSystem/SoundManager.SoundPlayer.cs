using System;
using System.Collections.Generic;
using System.Threading.Tasks;
// using DG.Tweening;
using UnityEngine;

namespace AudioSystem
{
    public partial class SoundManager
    {
        private class SoundPlayer
        {
            private List<AudioSource> _audioSources;
            private readonly GameObject _audioSourceHolder;

            public SoundPlayer(int initSoundChannel)
            {
                _audioSourceHolder = new GameObject();
                _audioSources = new List<AudioSource>();
                for (int i = 0; i < initSoundChannel; i++)
                {
                    CreateNewAudioSource();
                }
            }

            public async void Play(AudioClip clip, float volume, Action OnFinished)
            {
                var freeSource = GetFreeAudioSource();
                freeSource.clip = clip;
                freeSource.volume = volume;
                freeSource.PlayOneShot(clip);
                while (freeSource.isPlaying)
                {
                    await Task.Yield();
                }
                OnFinished?.Invoke();
            }

            private AudioSource GetFreeAudioSource()
            {
                var freeSource = _audioSources.Find(x => x.isPlaying == false);
                if (freeSource == null)
                    freeSource = CreateNewAudioSource();
                return freeSource;
            }

            private AudioSource CreateNewAudioSource()
            {
                var audioSource = _audioSourceHolder.AddComponent<AudioSource>();
                _audioSources.Add(audioSource);
                return audioSource;
            }

            public void Stop(AudioClip clip)
            {
                var currentAudioSource = _audioSources.Find(x => x.clip == clip);
                if (currentAudioSource != null)
                    currentAudioSource.Stop();
            }
        }
    }
}