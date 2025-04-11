using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public static class RequiredAudioMethods
{
    private static AudioSource PlayResourcesAtPoint(AudioResource audioResource, Vector3 position, Transform parent, 
        AudioMixerGroup audioMixerGroup = null, float volume = 1f)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = position;
        gameObject.transform.parent = parent;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.resource = audioResource;
        audioSource.spatialBlend = 1f;
        audioSource.volume = volume;
        audioSource.Play();

        return audioSource;
    }

    public static AudioSource PlayResourcesAtPoint(AudioResource audioResource, Vector3 position, Transform parent = null,
    AudioMixerGroup audioMixerGroup = null, float volume = 1f, bool destroyAfter = true)
    {
        var audioSource = PlayResourcesAtPoint(audioResource, position, parent, audioMixerGroup, volume);
        if (destroyAfter)
        {
            audioSource.gameObject.name = "One shot audio";
            var destroyer = (ConditionalDestroyer)audioSource.gameObject.AddComponent(typeof(ConditionalDestroyer));
            destroyer.Initialize(x => x.isPlaying, audioSource);
        }
        else
        {
            audioSource.gameObject.name = $"{audioResource.name}_Audio";
        }

        return audioSource;
    }
}
