using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MusicTrackHandler : MonoBehaviour
{
    [SerializeField]
    private MusicKeyValue[] trackListing;
    private Dictionary<MusicKey, MusicKeyValue> trackMap;
    private MusicKey currentTrack;
    [SerializeField]
    private float trackFadeTimeInSeconds = 0.5f;
    private int fadeSteps = 10;
    private int lastVolumeSetting;
    private Queue<MusicKey> _trackRequests = new Queue<MusicKey>();
    private MusicKeyValue _inProgressRequest = null;
    private MusicKeyValue _currentTrack;

    private void Awake()
    {
        trackMap = new Dictionary<MusicKey, MusicKeyValue>();
        for (int i = 0; i < trackListing.Length; i++)
        {
            trackMap.Add(trackListing[i].Key, trackListing[i]);
        }
        DefaultTrack defaultTrack = GetComponent<DefaultTrack>();
        lastVolumeSetting = SettingsHandler.MusicVolumeSetting;
        currentTrack = defaultTrack.Track;
        PlayTrack(currentTrack);
    }

    private void Update()
    {
        if (lastVolumeSetting != SettingsHandler.MusicVolumeSetting)
        {
            UpdateVolume();
            lastVolumeSetting = SettingsHandler.MusicVolumeSetting;
        }
        if (ShouldLoadTrack())
        {
            LoadTrack();
        }
    }
    
    private bool ShouldLoadTrack()
    {
        return _trackRequests.Count > 0 && _inProgressRequest == null;
    }

    private void LoadTrack()
    {
        currentTrack = _trackRequests.Dequeue();
        _inProgressRequest = trackMap[currentTrack];
        MusicTrackData[] clipsToPlay = _inProgressRequest.Value;
        for (int i = 0; i < clipsToPlay.Length; i++)
        {
            StartCoroutine(fadeInTrackRoutine(clipsToPlay[i]));
        }
    }

    public void UpdateVolume()
    {
        MusicTrackData[] currentTracks = trackMap[currentTrack].Value;
        for (int i = 0; i < currentTracks.Length; i++)
        {
            AudioSource playingSource = currentTracks[i].Source;
            if (playingSource != null)
            {
                playingSource.volume = SettingsHandler.GetMusicVolumeFraction() * currentTracks[i].Volume;
            }
        }
    }

    public void PlayTrack(MusicKey key)
    {
        _trackRequests.Enqueue(key);
    }

    private IEnumerator fadeInTrackRoutine(MusicTrackData trackData)
    {
        if (_currentTrack != null)
        {
            yield return StopTrack();
        }
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = trackData.Clip;
        newSource.volume = 0f;
        newSource.pitch = trackData.Pitch;
        newSource.playOnAwake = true;
        newSource.loop = true;
        newSource.Play();
        trackData.Source = newSource;
        float targetVolume = trackData.Volume * SettingsHandler.GetMusicVolumeFraction();
        float fadeWaitTime = targetVolume / fadeSteps;
        float volumeStepIncrease = targetVolume / fadeSteps;
        for (int i = 0; i < fadeSteps; i++)
        {
            newSource.volume += volumeStepIncrease;
            yield return new WaitForSeconds(fadeWaitTime);
        }
        _currentTrack = _inProgressRequest;
        _inProgressRequest = null;
    }

    public void StopAll()
    {
        StartCoroutine(StopTrack());
        _trackRequests.Clear();
    }

    private IEnumerator StopTrack()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
            if (sources[i] != null)
            {
                StartCoroutine(fadeOutAudioSource(sources[i]));
            }
        }
        yield return new WaitForSeconds(trackFadeTimeInSeconds);
        _currentTrack = null;
    }

    private IEnumerator fadeOutAudioSource(AudioSource sourceToFade)
    {
        float stepVolumeChange = sourceToFade.volume / fadeSteps;
        float fadeWaitTime = trackFadeTimeInSeconds / fadeSteps;
        for (int i = 0; i < fadeSteps; i++)
        {
            if (sourceToFade != null)
            {
                sourceToFade.volume -= stepVolumeChange;
                yield return new WaitForSeconds(fadeWaitTime);
            }
        }
        Destroy(sourceToFade);
    }
}
