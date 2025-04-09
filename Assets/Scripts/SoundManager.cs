using System;
using UnityEngine;
using UnityEngine.Audio;
using static RequiredAudioMethods;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioResource fruitSliceAudioResource;
    [SerializeField] private AudioResource bombExplosionAudioResource;

    public void PlayFruitSound()
    {
        PlayResourcesAtPoint(fruitSliceAudioResource, Vector3.zero);
    }

    public void PlayBombExplosionSound()
    {
        PlayResourcesAtPoint(bombExplosionAudioResource, Vector3.zero);
    }
}
