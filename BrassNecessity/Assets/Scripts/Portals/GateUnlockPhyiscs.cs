using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateUnlockPhyiscs : MonoBehaviour
{
    [SerializeField]
    private GameObject parentObject;
    [SerializeField]
    private float timeUntilDestroyInSeconds = 2f;
    [SerializeField]
    private GameObject destructionPrefab;
    private Animator lockAnimator;
    private SoundEffectTrackHandler soundEffects;
    // Start is called before the first frame update
    void Start()
    {
        soundEffects = FindObjectOfType<SoundEffectTrackHandler>();
        lockAnimator = GetComponent<Animator>();
    }

    public void UnlockGate()
    {
        parentObject.GetComponent<Animator>().enabled = false;
        this.gameObject.GetComponent<Animator>().enabled = false;
        SphereCollider lockCollider = this.gameObject.AddComponent<SphereCollider>();
        Rigidbody lockBody = this.gameObject.AddComponent<Rigidbody>();
        lockBody.isKinematic = true;
        lockBody.isKinematic = false;
        StartCoroutine(waitUntilDestruction());
    }

    public void SetLockHitEffect(float effectPercentage)
    {
        lockAnimator.SetFloat("Shake", effectPercentage);
    }

    public void ResetLockHitEffect()
    {
        lockAnimator.SetFloat("Shake", 0f);
        lockAnimator.SetTrigger("Default");
    }

    private IEnumerator waitUntilDestruction()
    {
        yield return new WaitForSeconds(2f);
        GameObject destroyEffect = Instantiate(destructionPrefab);
        destroyEffect.transform.position = this.transform.position;
        soundEffects.PlayOnce(SoundEffectKey.LaserGateLockDestroy);
        Destroy(parentObject);
    }
}
