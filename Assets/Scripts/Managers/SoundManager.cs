using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip FireSound;
    public AudioClip HitDroneSound;
    public AudioClip EmptyGunSound;

    public void Play(AudioClip sound)
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
    }
}
