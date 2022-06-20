using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource source;
    public AudioSource musicSource;
    public AudioClip mainMusic;

    [SerializeField]
    List<AudioClip> clips = new List<AudioClip>();

    public float minTimeBetweenSounds;
    public float maxTimeBetweenSounds;
    Coroutine randomSFX;


    void Start()
    {
        source = GetComponent<AudioSource>();
        randomSFX = StartCoroutine(playSound());
        //ResetMusic();
    }

    IEnumerator playSound(bool shrineDone = false)
    {
        if (shrineDone)
        {
            yield return new WaitForSeconds(6);
            ResetMusic();
        }

        int newClip = Random.Range(0, clips.Count - 1);

        source.clip = clips[newClip];
        source.Play();

        yield return new WaitForSeconds(Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds));
        randomSFX = StartCoroutine(playSound());
    }

    public void PlayShrine(AudioClip shrineMusic)
    {
        StopMusic();
        StopCoroutine(randomSFX);

        source.clip = shrineMusic;
        source.Play();

        randomSFX = StartCoroutine(playSound(true));
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void ChangeMusic(AudioClip newClip)
    {
        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();
    }

    public void ResetMusic()
    {
        musicSource.Stop();
        musicSource.clip = mainMusic;
        musicSource.Play();
    }
}