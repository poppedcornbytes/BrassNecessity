using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGateBehaviour : MonoBehaviour
{
    [SerializeField]
    private float timeToUnlockInSeconds = 4f;
    private float currentUnlockProgressInSeconds = 0f;
    protected bool isResetting = false;
    protected bool isBarrierActive = true;
    [SerializeField]
    private GameObject[] objectsToDisableOnComplete;
    [SerializeField]
    private GameObject unlockEffectPrefab;
    private GateUnlockPhyiscs lockBehaviour;
    private SoundEffectTrackHandler soundEffects;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        soundEffects = FindObjectOfType<SoundEffectTrackHandler>();
        lockBehaviour = GetComponentInChildren<GateUnlockPhyiscs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isResetting)
        {
            decrementProgress();
        }
    }

    private void decrementProgress()
    {
        currentUnlockProgressInSeconds -= Time.deltaTime;
        if (currentUnlockProgressInSeconds <= 0f)
        {
            isResetting = false;
            currentUnlockProgressInSeconds = 0f;
        }
    }

    public void IncreaseUnlockProgress()
    {
        isResetting = false;
        currentUnlockProgressInSeconds += Time.deltaTime;
        updateLockEffects();
        if (currentUnlockProgressInSeconds >= timeToUnlockInSeconds)
        {
            removeBarrier();
        }
    }

    private void updateLockEffects()
    {
        if (!isResetting)
        {
            lockBehaviour.SetLockHitEffect(currentUnlockProgressInSeconds / timeToUnlockInSeconds);
        }
        else
        {
            lockBehaviour.ResetLockHitEffect();
        }
    }

    protected virtual void removeBarrier()
    {
        GameObject unlockEffect = Instantiate(unlockEffectPrefab);
        unlockEffect.transform.position = lockBehaviour.transform.position;
        soundEffects.PlayOnce(SoundEffectKey.LaserGateUnlock);
        disableBarrierObjects();
        GetComponentInChildren<GateUnlockPhyiscs>().UnlockGate();
        isBarrierActive = false;
    }

    protected virtual void disableBarrierObjects()
    {
        GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i < objectsToDisableOnComplete.Length; i++)
        {
            objectsToDisableOnComplete[i].SetActive(false);
        }
    }

    public void StartRevertingUnlockProgress()
    {
        if (isBarrierActive)
        {
            isResetting = true;
            updateLockEffects();
        }
    }
}
