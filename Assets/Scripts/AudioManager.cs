using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] private AudioMixer _audioGameMixer;
    public AudioData _audioData;
    [SerializeField] private AudioSource _soundMusic;
    [SerializeField] private AudioSource _soundSFX;
    [SerializeField] Slider master;
    [SerializeField] Slider music;
    [SerializeField] Slider SFX;

    void Start()
    {
        PlayMusicIndex(0);
    }
    public void Setmaster(float f)
    {
        _audioData._master = f;
        _audioGameMixer.SetFloat("Master", Mathf.Log10(f) * 20f);
        Debug.Log("Master volume set to: " + f);
    }
    public void SetMusic(float f)
    {
        _audioData._music = f;
        _audioGameMixer.SetFloat("Music", Mathf.Log10(f) * 20f);
        Debug.Log("Music volume set to: " + f);
    }
    public void SetSFX(float f)
    {
        _audioData._SFX = f;
        _audioGameMixer.SetFloat("SFX", Mathf.Log10(f) * 20f);
        Debug.Log("SFX volume set to: " + f);
    }
    public void PlaySound()
    {
        _soundMusic.Play();
    }
    public void StopSound()
    {
        _soundMusic.Stop();
    }
    public void PlaysfxIndex(int index)
    {
        _soundSFX.clip = _audioData.sfxClip[index];
        _soundSFX.Play();
        Debug.Log("audio" + index.ToString(name));
    }
    public void PlayMusicIndex(int index)
    {
        _soundMusic.clip = _audioData.musicClip[index];
        _soundMusic.Play();
    }
    public void MuteSFX()
    {
        _audioGameMixer.SetFloat("SFX", -80f);
        _audioData.isMuteSFX = true;
    }
    public void MuteMusic()
    {
        _audioGameMixer.SetFloat("Music", -80f);
        _audioData.isMuteMusic = true;
    }
    public void UnMuteMusic()
    {
        _audioGameMixer.SetFloat("Music", 0f);
        _audioData.isMuteMusic = false;
    }
    public void UnMuteSFX()
    {
        _audioGameMixer.SetFloat("SFX", 0f);
        _audioData.isMuteSFX = false;
    }


}
