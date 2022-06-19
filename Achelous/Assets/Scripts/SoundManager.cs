using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource source;

    [SerializeField]
    List<AudioClip> clips = new List<AudioClip>();

    public float minTimeBetweenSounds;
    public float maxTimeBetweenSounds;


    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
    {
        int newClip = Random.Range(0, clips.Count - 1);

        source.clip = clips[newClip];
        source.Play();

        yield return new WaitForSeconds(Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds));
        StartCoroutine(playSound());
    }
}