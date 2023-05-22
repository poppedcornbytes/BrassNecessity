using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSoundBehaviour : MonoBehaviour
{
    [SerializeField]
    private SoundEffectTrackHandler soundEffectPlayer;

    [SerializeField]
    private AudioSource firingSource;

    [SerializeField]
    private float impactSoundTimeoutInSeconds = 0.5f;
    private FrameTimeoutHandler soundTimeoutHandler;
    private bool isPlayingEffect = false;

    [SerializeField]
    private float strongElementPitchMultiplier = 1.2f;
    [SerializeField]
    private float strongElementVolume = 0.3f;

    [SerializeField]
    private float weakElementPitchMultiplier = 0.8f;

    [SerializeField]
    private float weakElementVolume = 1f;

    private float normalPitchMultiplier;
    private float normalVolume;

    private void Awake()
    {
        normalPitchMultiplier = firingSource.pitch;
        normalVolume = firingSource.volume;
        soundTimeoutHandler = new FrameTimeoutHandler(impactSoundTimeoutInSeconds);
        soundTimeoutHandler.UpdateTimePassed(impactSoundTimeoutInSeconds);//start at zero remaining
        if (soundEffectPlayer == null)
        {
            soundEffectPlayer = FindObjectOfType<SoundEffectTrackHandler>();
        }
    }

    private void OnEnable()
    {
        soundTimeoutHandler.UpdateTimePassed(impactSoundTimeoutInSeconds);
    }

    private void Update()
    {
        if (!soundTimeoutHandler.HasTimeoutEnded())
        {
            soundTimeoutHandler.UpdateTimePassed(Time.deltaTime);
        }
    }

    public void StartLoopLaserFiringEffect() 
    {
        if (!firingSource.isPlaying)
        {
            AudioClip clipToLoop = soundEffectPlayer.GetClipForLooping(SoundEffectKey.LaserFiringSound);
            firingSource.clip = clipToLoop;
            firingSource.Play();
        }
    }

    public void StopLoopLaserFiringEffect()
    {
        firingSource.Stop();
    }

    public void PlayLaserImpactSound(float attackMultiplier)
    {
        if (soundTimeoutHandler.HasTimeoutEnded())
        {
            soundTimeoutHandler.ResetTimeout();
            if (ElementMultiplierGrid.IsWeakMultiplier(attackMultiplier))
            {
                soundEffectPlayer.PlayOnce(SoundEffectKey.WeakLaserAttack);
            }
            else if (ElementMultiplierGrid.IsStrongMultiplier(attackMultiplier))
            {
                soundEffectPlayer.PlayOnce(SoundEffectKey.StrongLaserAttack);
            }
        }
    }

    public void ChangeLaserFiringSoundWithMultiplier(float attackMultiplier)
    {
        if (ElementMultiplierGrid.IsWeakMultiplier(attackMultiplier))
        {
            firingSource.pitch = normalPitchMultiplier * weakElementPitchMultiplier;
            firingSource.volume = weakElementVolume * SettingsHandler.GetEffectVolumeFraction();
        }
        else if (ElementMultiplierGrid.IsStrongMultiplier(attackMultiplier))
        {
            firingSource.pitch = normalPitchMultiplier * strongElementPitchMultiplier;
            firingSource.volume = strongElementVolume * SettingsHandler.GetEffectVolumeFraction();
        }
        else
        {
            NormaliseLaserFiringSoundPitch();
        }
    }

    public void NormaliseLaserFiringSoundPitch()
    {
        firingSource.pitch = normalPitchMultiplier;
        firingSource.volume = normalVolume * SettingsHandler.GetEffectVolumeFraction();
    }

}
