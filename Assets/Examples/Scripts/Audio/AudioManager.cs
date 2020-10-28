using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

//test
public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        // Loop the background music
        musicSource.loop = true;
    }

    public void FadeOutMusic()
    {
        StartCoroutine(BeginFadeOut(2.0f));
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
