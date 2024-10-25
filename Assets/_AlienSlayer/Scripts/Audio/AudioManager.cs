using System.Collections.Generic;
using Drland.Common.Utils;
using UnityEngine;

namespace drland.AlienSlayer
{
    public enum SFXName
    {
        RifleShoot,
        GrenadeLauncherShoot,
        GrenadeExplosion,
        EnemyHit,
        PlayerHit,
        EnemyDie
    }
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource _bgmSource;  
        [SerializeField] private AudioSource _sfxSource; 

        [Header("BGM Clips")]
        [SerializeField] private AudioClip[] _bgmClips;  

        [Header("SFX Clips")]
        [SerializeField] private AudioClip[] _sfxClips;  

        private Dictionary<string, AudioClip> _sfxDict;

        protected override void Awake()
        {
            base.Awake();
            _sfxDict = new Dictionary<string, AudioClip>();
            foreach (var clip in _sfxClips)
            {
                _sfxDict.Add(clip.name, clip);
            }
        }

        public void PlayBGM(int index)
        {
            if (index < 0 || index >= _bgmClips.Length)
            {
                return;
            }
            _bgmSource.clip = _bgmClips[index];
            _bgmSource.Play();
        }

        public void PlayBGMByName(string name)
        {
            AudioClip clip = FindClipByName(name, _bgmClips);
            if (clip != null)
            {
                _bgmSource.clip = clip;
                _bgmSource.Play();
            }
        }

        public void StopBGM()
        {
            _bgmSource.Stop();
        }

        public void SetBGMVolume(float volume)
        {
            _bgmSource.volume = volume;
        }

        public void PlaySFXAtPosition(string name, Vector3 position)
        {
            if (_sfxDict.TryGetValue(name, out var clip))
            {
                AudioSource.PlayClipAtPoint(clip, position);
            }
        }

        private void PlaySFX(AudioClip audioClip)
        {
        }

        public void StopSFX()
        {
            _sfxSource.Stop();
        }

        public void SetSFXVolume(float volume)
        {
            _sfxSource.volume = volume;
        }

        private AudioClip FindClipByName(string name, AudioClip[] clips)
        {
            foreach (var clip in clips)
            {
                if (clip.name == name)
                {
                    return clip;
                }
            }
            return null;
        }
    }
}
