using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceController : MonoBehaviour
{
    [SerializeField]
    private Animator animatorController;
    private SoundEffectTrackHandler soundEffectManager;
    
    private void Awake()
    {
        animatorController = GetComponent<Animator>();
        soundEffectManager = FindObjectOfType<SoundEffectTrackHandler>();
    }

    public void RaiseFence()
    {
        animatorController.SetTrigger("Rise");
    }

    public void LowerFence()
    {
        animatorController.SetTrigger("Lower");
    }

    private void playActivationSound(AnimationEvent animationEvent)
    {
        soundEffectManager.PlayOnce(SoundEffectKey.ObstacleFence);
    }

    private void OnLowerComplete(AnimationEvent animationEvent)
    {
        soundEffectManager.PlayOnce(SoundEffectKey.ObstacleFence);
    }
}
