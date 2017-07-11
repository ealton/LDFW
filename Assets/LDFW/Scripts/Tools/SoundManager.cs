using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using LDFW.Extensions;

namespace LDFW.Tools
{
    
    public class SoundManager : MonoBehaviour
    {
        private AudioSource                                 _backgroundAudioSource;

        private GameObject                                  _sound;
        private GameObject                                  _voice;
        private GameObject                                  _background;
        private GameObject                                  _cachedSound;
        private GameObject                                  _cachedVoice;

        private List<AudioSource>                           _soundSourceList;
        private List<AudioSource>                           _voiceSourceList;
        private List<AudioSource>                           _backgroundSourceList;
        private Dictionary<string, AudioSource>             _cachedSoundSourceList;
        private Dictionary<string, AudioSource>             _cachedVoiceSourceList;

        private string                                      _soundSourceParentPath = "Sound/";
        private string                                      _soundSourceResourcePath = "sound/";
        private string                                      _voiceSourceResourcePath = "voice/";

        private AudioListener                               _audioListener;

        

        /// <summary>
        /// Reset
        /// </summary>
        public SoundManager Reset(string[] cacheSoundIDList, string[] cacheVoiceIDList)
        {
            _soundSourceResourcePath = _soundSourceParentPath + "sound/";
            _voiceSourceResourcePath = _soundSourceParentPath + "voice/";

            transform.DestroyAllChildren();

            _sound = new GameObject("SoundSources");
            _sound.transform.SetParent(transform);

            _voice = new GameObject("VoiceSources");
            _voice.transform.parent = transform;

            _background = new GameObject("BackgrondSources");
            _background.transform.parent = transform;

            _cachedSound = new GameObject("CachedSoundSources");
            _cachedSound.transform.parent = transform;

            _cachedVoice = new GameObject("CachedVoiceSources");
            _cachedVoice.transform.parent = transform;
            
            _soundSourceList = new List<AudioSource>();
            _voiceSourceList = new List<AudioSource>();
            _backgroundSourceList = new List<AudioSource>();

            LoadCacheList(cacheSoundIDList, cacheVoiceIDList);

            _audioListener = gameObject.AddComponent<AudioListener>();

            return this;
        }

        /// <summary>
        /// Load cache list
        /// </summary>
        /// <param name="cacheSoundIDList"></param>
        public SoundManager LoadCacheList(string[] cacheSoundIDList, string[] cacheVoiceIDList)
        {
            _cachedSoundSourceList = new Dictionary<string, AudioSource>();

            if (cacheSoundIDList != null)
            {
                foreach (var id in cacheSoundIDList)
                {
                    AudioSource newAudio = _cachedSound.AddComponent<AudioSource>();
                    newAudio.clip = LoadSound(id);
                    newAudio.playOnAwake = false;
                    _cachedSoundSourceList.Add(id, newAudio);
                }
            }

            _cachedVoiceSourceList = new Dictionary<string, AudioSource>();
            if (cacheVoiceIDList != null)
            {
                foreach (var id in cacheVoiceIDList)
                {
                    AudioSource newAudio = _cachedVoice.AddComponent<AudioSource>();
                    newAudio.clip = LoadSound(id);
                    newAudio.playOnAwake = false;
                    _cachedVoiceSourceList.Add(id, newAudio);
                }
            }

            return this;
        }
        
        /// <summary>
        /// Play sound file
        /// </summary>
        /// <param name="str"></param>
        /// <param name="delay"></param>
        /// <param name="isConcurrent"></param>
        /// <param name="isRecursive"></param>
        /// <param name="forceNewAudioSource"></param>
        /// <returns></returns>
        public AudioSource PlaySound(string str, float delay = 0f, bool isConcurrent = true, bool isRecursive = false, bool forceNewAudioSource = false)
        {
            AudioSource cached = FetchCachedSound(str);
            if (cached == null)
            {
                return PlayAudioSource(_soundSourceResourcePath + str, _sound, _soundSourceList, isConcurrent, delay, isRecursive, forceNewAudioSource);
            }
            else
            {
                if (!cached.isPlaying)
                {
                    cached.PlayDelayed(0);
                }
                return cached;
            }
        }

        /// <summary>
        /// Play voice file
        /// </summary>
        /// <param name="str"></param>
        /// <param name="delay"></param>
        /// <param name="isConcurrent"></param>
        public AudioSource PlayVoice(string str, float delay = 0f, bool isConcurrent = false, bool isRecursive = false)
        {
            AudioSource cached = FetchCachedVoice(str);
            if (cached == null)
            {
                return PlayAudioSource(_voiceSourceResourcePath + str, _voice, _voiceSourceList, isConcurrent, delay, isRecursive, true);
            }
            else
            {
                if (!cached.isPlaying)
                {
                    cached.PlayDelayed(0);
                }
                return cached;
            }
        }

        /// <summary>
        /// Fetch cached sound audio source
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private AudioSource FetchCachedSound(string str)
        {
            if (_cachedSoundSourceList.ContainsKey(str))
                return _cachedSoundSourceList[str];
            else
                return null;
        }

        /// <summary>
        /// Fetch cached voice audio source
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private AudioSource FetchCachedVoice(string str)
        {
            if (_cachedVoiceSourceList.ContainsKey(str))
                return _cachedVoiceSourceList[str];
            else
                return null;
        }

        /// <summary>
        /// Load audio clip from resources directory
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public AudioClip LoadSound(string str)
        {
            return LoadAudioClip(_soundSourceResourcePath + str);
        }

        /// <summary>
        /// Stop all sound
        /// </summary>
        public void StopAllSounds()
        {
            foreach (var source in _soundSourceList)
            {
                source.Stop();
            }
            foreach (var source in _voiceSourceList)
            {
                source.Stop();
            }
            foreach (var source in _backgroundSourceList)
            {
                source.Stop();
            }
        }

        /// <summary>
        /// Load audio clip from resources directory
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public void StopAudioClip(AudioSource audio)
        {
            audio.Stop();
            Destroy(audio);
        }

        /// <summary>
        /// Pause all sound
        /// </summary>
        public void PauseAllSounds()
        {
            foreach (var source in _soundSourceList)
            {
                source.Pause();
            }
            foreach (var source in _voiceSourceList)
            {
                source.Pause();
            }
            foreach (var source in _backgroundSourceList)
            {
                source.Pause();
            }
            if (_backgroundAudioSource != null)
                _backgroundAudioSource.Pause();
        }

        /// <summary>
        /// Resume all sound
        /// </summary>
        public void ResumeAllSounds()
        {
            foreach (var source in _soundSourceList)
            {
                source.UnPause();
            }
            foreach (var source in _voiceSourceList)
            {
                source.UnPause();
            }
            foreach (var source in _backgroundSourceList)
            {
                source.UnPause();
            }
            if (_backgroundAudioSource != null)
                _backgroundAudioSource.UnPause();
        }

        /// <summary>
        /// Destroy audioSource
        /// </summary>
        /// <param name="audioSource"></param>
        public void DestroyAudioSource(AudioSource audioSource)
        {
            _soundSourceList.Remove(audioSource);
            _voiceSourceList.Remove(audioSource);
            _backgroundSourceList.Remove(audioSource);

            Destroy(audioSource);
        }


        #region HelperFUnctions

        private AudioSource PlayAudioSource(string soundString, GameObject targetGO, List<AudioSource> targetSourceList, bool isConcurrentSource, float delay, bool isRecursive, bool forceNewAudioSource)
        {
            AudioClip targetAudioClip = LoadAudioClip(soundString);

            if (targetAudioClip != null)
            {
                AudioSource targetAudioSource = GetAvailableAudioSource(targetGO, targetSourceList, isConcurrentSource, forceNewAudioSource);
                if (targetAudioSource != null)
                {
                    targetAudioSource.loop = isRecursive;
                    targetAudioSource.clip = targetAudioClip;
                    targetAudioSource.PlayDelayed(delay);
                    return targetAudioSource;
                }
                else
                {
                    Debug.LogError("No available target audio source: " + soundString);
                    return null;
                }
            }
            else
            {
                Debug.LogError("targetAudioClip is null");
                return null;
            }

        }

        private AudioSource GetAvailableAudioSource(GameObject go, List<AudioSource> list, bool isConcurrentSource, bool forceNewAudioSource)
        {
            AudioSource targetSource = null;
            int playingSourceCount = 0;
            foreach (var source in list)
            {
                if (source.isPlaying)
                {
                    playingSourceCount++;
                }
                else
                {
                    targetSource = source;
                    break;
                }
            }

            if (isConcurrentSource == false)
            {
                // doesn't allow concurrent audios
                if (playingSourceCount == 0)
                {
                    // if no source is playing, return targetSource
                    if (targetSource == null)
                    {
                        targetSource = go.AddComponent<AudioSource>();
                        list.Add(targetSource);
                    }
                    return targetSource;
                }
                else
                {
                    // if at least one source is playing, return null
                    return null;
                }
            }
            else
            {
                // allow concurrent audios
                if (targetSource == null || forceNewAudioSource)
                {
                    targetSource = go.AddComponent<AudioSource>();
                    list.Add(targetSource);
                }
                return targetSource;
            }
        }

        /// <summary>
        /// Loads an audio clip from resources dir
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private AudioClip LoadAudioClip(string str)
        {
            AudioClip audio = Resources.Load<AudioClip>(str);
            if (audio == null)
                Debug.LogError("Audio source not found: " + str);


            return audio;
        }

        #endregion

    }


}