using System;
using UnityEngine;

namespace AudioSystem
{
    public class AudioExampleExample : MonoBehaviour
    {
        [SerializeField] private SoundManager soundManager;
        
        private bool _mute;

        private void Awake()
        {
            soundManager.Setup();
//#if AUDIO_PLAYER
            soundManager.Play("");
//#endif
        }

        private void Update()
        {
//#if AUDIO_PLAYER
            if (Input.GetKeyDown(KeyCode.M))
            {
                _mute = !_mute;
                //sfxPlayer.SetMute(Channels.MusicChannel, _mute);
            }

            if (Input.GetMouseButtonDown(0))
            {
                //sfxPlayer.Play(Sounds.CardFlick_1, () => { Debug.Log($"Played : {Sounds.CardFlick_1}"); });
            }
//#endif
        }
    }
}