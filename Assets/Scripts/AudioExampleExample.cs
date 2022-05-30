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
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                _mute = !_mute;
                soundManager.Play("CompleteGame");
            }
        }
    }
}