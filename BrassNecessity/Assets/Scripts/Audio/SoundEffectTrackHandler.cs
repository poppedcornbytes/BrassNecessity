using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectTrackHandler : MonoBehaviour
{
    [SerializeField]
    private AudioSource clipSource;
    [SerializeField]
    private SoundEffectKeyValue[] clipListing;
    private Dictionary<SoundEffectKey, SoundEffectKeyValue> listingMap;
    [SerializeField]
    private AudioListener playerCameraListener;

    private void Awake()
    {
        if (playerCameraListener == null)
        {
            playerCameraListener = Camera.main.GetComponent<AudioListener>();
        }
        listingMap = new Dictionary<SoundEffectKey, SoundEffectKeyValue>();
        for (int i = 0; i < clipListing.Length; i++)
        {
            listingMap.Add(clipListing[i].Key, clipListing[i]);
        }
        transform.position = playerCameraListener.transform.position;
    }

    public void PlayOnce(SoundEffectKey listingKey)
    {
        AudioClip clipToPlay = listingMap[listingKey].Value;
        if (clipSource != null)
        {
            clipSource.PlayOneShot(clipToPlay);
        }
    }

    public AudioClip GetClipForLooping(SoundEffectKey listingKey)
    {
        return listingMap[listingKey].Value;
    }
}
