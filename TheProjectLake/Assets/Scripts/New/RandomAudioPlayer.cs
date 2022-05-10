using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SoulsLike
{
    public class RandomAudioPlayer : MonoBehaviour
    {
        [System.Serializable]
        public class SoundBank
        {
            public string name;
            public AudioClip[] clips;
        }

        public bool canPlay;
        public bool isPlaying;
        public SoundBank soundBank = new SoundBank();
        private AudioSource mAudioSource;

        private void Awake()
        {
            mAudioSource = GetComponent<AudioSource>();
        }

        public void PlayRandomClip()
        {
            var clip = soundBank.clips[Random.Range(0,soundBank.clips.Length)];
        
            if(clip == null)
            {
                return;
            }

            mAudioSource.clip = clip;
            mAudioSource.Play();
        }

    }
}

