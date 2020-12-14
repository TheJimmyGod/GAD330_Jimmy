using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

//test
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        // Loop the background music
        musicSource.loop = true;
    }

    public void FadeOutMusic(float time)
    {
        StartCoroutine(BeginFadeOut(time));
    }

    public void FadeInMusic(float time)
    {
        StartCoroutine(BeginFadeIn(time));
    }

    public void PlaySfx(AudioClip clip, float volume = 1.0f)
    {
        sfxSource.clip = clip;
        sfxSource.volume = volume;
        sfxSource.Play();
    }

    private IEnumerator BeginFadeOut(float duration)
    {
        var muteMusicSS = audioMixer.FindSnapshot("MuteMusic");
        audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[] { muteMusicSS }, 
                                            new float[] { 1.0f }, 
                                            duration);

        yield return new WaitForSeconds(duration);
        // at this point the transition is done.

        var defaultSS = audioMixer.FindSnapshot("Default");
        audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[] { defaultSS },
                                     new float[] { 1.0f },
                                     duration);
    }

    private IEnumerator BeginFadeIn(float duration)
    {
        yield return new WaitForSeconds(duration);

        var muteMusicSS = audioMixer.FindSnapshot("MuteMusic");
        audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[] { muteMusicSS },
                                            new float[] { 1.0f },
                                            duration);

    }

    public void SetMasterVolume(float vol)
    {
        if (vol <= -50.0f)
            vol = -80.0f;
        audioMixer.SetFloat("Master_Volume", vol);
    }

    public void SetMusicVolume(float vol)
    {
        if (vol <= -50.0f)
            vol = -80.0f;
        audioMixer.SetFloat("Music_Volume", vol);
    }

    public void SetSFXVolume(float vol)
    {
        if (vol <= -50.0f)
            vol = -80.0f;
        audioMixer.SetFloat("SFX_Volume", vol);
    }
}
