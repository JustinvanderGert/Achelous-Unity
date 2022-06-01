using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource mySounds;
    public AudioClip hoverSound;

    public AudioSource clickSound;

    public void HoverSound()
    {
        mySounds.PlayOneShot(hoverSound);
    }
    public void ClickSound()
    {
        clickSound.Play();
    }
}
