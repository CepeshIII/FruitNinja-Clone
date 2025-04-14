using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static RequiredAudioMethods;

// Singleton class that manages and plays audio resources in the game
public class SoundManager : Singleton<SoundManager>
{
    // Serialized audio resources assigned via the Inspector
    [SerializeField] private AudioResource fruitSliceAudioResource;
    [SerializeField] private AudioResource bombExplosionAudioResource;
    [SerializeField] private AudioResource WhooshAudioResource;
    [SerializeField] private AudioResource TossAudioResource;

    // Dictionary to store lists of AudioSources associated with each AudioResource
    private Dictionary<AudioResource, List<AudioSource>> _initializedAudioSources = new();

    public void PlayFruitSound()
    {
        Play(fruitSliceAudioResource, Vector3.zero);
    }

    public void PlayBombExplosionSound()
    {
        Play(bombExplosionAudioResource, Vector3.zero);
    }

    public void PlayWhooshSound()
    {
        Play(WhooshAudioResource, Vector3.zero, false);
    }

    public void PlayTossSound()
    {
        Play(TossAudioResource, Vector3.zero, false);
    }

    /// <summary>
    /// Core function to handle playing an audio resource.
    /// </summary>
    /// <param name="resource">The audio resource to play.</param>
    /// <param name="position">World position to play the sound.</param>
    /// <param name="withAllowedParallelPlay">Whether multiple instances of the sound can play simultaneously.</param>
    private void Play(AudioResource resource, Vector3 position, bool withAllowedParallelPlay = true)
    {
        // Get or create the list of audio sources for the given resource
        if (!_initializedAudioSources.TryGetValue(resource, out List<AudioSource> audioSources))
        {
            audioSources = new List<AudioSource>();
            _initializedAudioSources.Add(resource, audioSources);
        }

        // Try to get an existing audio source that is not currently playing
        if (!TryGetEnactiveAudioSource(audioSources, out var audioSource))
        {
            // Here we check whether parallel playback of the same resource is allowed.
            // If it's not, we check if there's at least one non-null audio source 
            // (to ensure that at least one player has been created).
            if (withAllowedParallelPlay || (audioSources.Count == 0 ))
            {
                // Create and play a new audio source
                audioSource = PlayResourcesAtPoint(resource, position, transform, null, 1, false);
                audioSources.Add(audioSource);
            }
        }
        else
        {
            // Reactivate and play the available audio source
            audioSource.gameObject.SetActive(true);
            audioSource.Play();
        }
    }

    /// <summary>
    /// Searches through the list of audio sources and returns the first one that is not currently playing.
    /// </summary>
    /// <param name="audioSources">List of available audio sources.</param>
    /// <param name="foundAudioSource">Output: found available audio source or null.</param>
    /// <returns>True if a non-playing audio source was found; false otherwise.</returns>
    private bool TryGetEnactiveAudioSource(List<AudioSource> audioSources, out AudioSource foundAudioSource)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource == null) continue;

            if (!audioSource.isPlaying)
            {
                foundAudioSource = audioSource;
                return true;
            }
        }

        foundAudioSource = null;
        return false;
    }

    private void OnDisable()
    {
        foreach (var key in _initializedAudioSources.Keys) 
        { 
            foreach(var audioSource in _initializedAudioSources[key])
            {
                Destroy(audioSource);
            }
        }
        _initializedAudioSources.Clear();
    }
}
