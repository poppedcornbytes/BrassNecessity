using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrackHandler : MonoBehaviour
{
    [SerializeField]
    private MusicKeyValue[] trackListing;
    private Dictionary<MusicKey, MusicKeyValue> trackMap;
    [SerializeField]
    private float trackFadeTimeInSeconds = 0.5f;
    private int fadeSteps = 10;

    private void Awake()
    {
        trackMap = new Dictionary<MusicKey, MusicKeyValue>();
        for (int i = 0; i < trackListing.Length; i++)
        {
            trackMap.Add(trackListing[i].Key, trackListing[i]);
        }
        DefaultTrack defaultTrack = GetComponent<DefaultTrack>();
        PlayTrack(defaultTrack.Track);
    }

    public void PlayTrack(MusicKey key)
    {
        MusicTrackData[] clipsToPlay = trackMap[key].Value;
        for (int i = 0; i < clipsToPlay.Length; i++)
        {
            StartCoroutine(fadeInTrackRoutine(clipsToPlay[i]));
        }
    }

    private IEnumerator fadeInTrackRoutine(MusicTrackData trackData)
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = trackData.Clip;
        newSource.volume = 0f;
        newSource.pitch = trackData.Pitch;
        newSource.playOnAwake = true;
        newSource.loop = true;
        newSource.Play();
        float fadeWaitTime = trackFadeTimeInSeconds / fadeSteps;
        float volumeStepIncrease = trackData.Volume / fadeSteps;
        for (int i = 0; i < fadeSteps; i++)
        {
            newSource.volume += volumeStepIncrease;
            yield return new WaitForSeconds(fadeWaitTime);
        }
    }

    public IEnumerator StopTrack()
    {
        int fadeSteps = 10;
        float fadeWaitTime = trackFadeTimeInSeconds / fadeSteps;
        AudioSource[] sources = GetComponents<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
            StartCoroutine(fadeOutAudioSource(sources[i]));
        }
        yield return new WaitForSeconds(trackFadeTimeInSeconds);
    }

    private IEnumerator fadeOutAudioSource(AudioSource sourceToFade)
    {
        float stepVolumeChange = sourceToFade.volume / fadeSteps;
        float fadeWaitTime = trackFadeTimeInSeconds / fadeSteps;
        for (int i = 0; i < fadeSteps; i++)
        {
            sourceToFade.volume -= stepVolumeChange;
            yield return new WaitForSeconds(fadeWaitTime);
        }
        Destroy(sourceToFade);
    }
}
