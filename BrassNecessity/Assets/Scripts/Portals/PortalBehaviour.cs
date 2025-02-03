using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour, IPortal, IExitEventHandler
{
    [SerializeField]
    protected float preTeleportTimeout = 2f;
    [SerializeField]
    protected GameObject preTeleportEffectPrefab;
    [SerializeField]
    protected float preEffectDuration = 1f;
    [SerializeField]
    protected GameObject postTeleportEffectPrefab;
    [SerializeField]
    protected AudioSource effectSource;
    [SerializeField]
    protected SoundEffectTrackHandler soundEffects;
    protected Dictionary<GameObject, FrameTimeoutHandler> teleportMap;
    [SerializeField]
    protected bool isHidden = false;
    [SerializeField]
    protected bool isDisabled = false;
    public bool IsHidden { get => isHidden; }
    public bool IsDisabled { get => isDisabled; }
    private event GameEvents.ExitEvent OnExitEvent;

    protected virtual void Awake()
    {
        if (soundEffects == null)
        {
            soundEffects = FindObjectOfType<SoundEffectTrackHandler>();
        }
        teleportMap = new Dictionary<GameObject, FrameTimeoutHandler>();

    }

    // Start is called before the first frame update
    protected void Start()
    {
    }

    // Update is called once per frame
    protected void Update()
    {
        float secondsPassed = Time.deltaTime;
        List<GameObject> teleportCandidates = new List<GameObject>(teleportMap.Keys);
        for (int i = 0; i < teleportCandidates.Count; i++)
        {
            GameObject candidate = teleportCandidates[i];
            FrameTimeoutHandler candidateTimer = teleportMap[candidate];
            candidateTimer.UpdateTimePassed(secondsPassed);
            if (candidateTimer.HasTimeoutEnded())
            {
                TeleportObject(candidate);
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        GameObject teleportingObject = other.gameObject;
        bool canTryTeleporting = other.gameObject.tag == "Player";
        if (canTryTeleporting)
        {
            canTryTeleporting = !isHidden && !isDisabled;
        }
        if (canTryTeleporting)
        {
            if (!teleportMap.ContainsKey(teleportingObject))
            {
                teleportMap.Add(teleportingObject, new FrameTimeoutHandler(preTeleportTimeout));
                AudioClip clipToPlay = soundEffects.GetClipForLooping(SoundEffectKey.TeleportBegin);
                effectSource.clip = clipToPlay;
                effectSource.volume = SettingsHandler.GetEffectVolumeFraction();
                effectSource.Play();
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
       GameObject exitingObject = other.gameObject;
        if (teleportMap.ContainsKey(exitingObject))
        {
            teleportMap.Remove(exitingObject);
            if (effectSource.isPlaying)
            {
                StartCoroutine(fadeOutTeleportSound());
            }
            CallExitEvent();
        }
    }

    public void AddExitEvent(GameEvents.ExitEvent eventToAdd)
    {
        OnExitEvent += eventToAdd;
    }

    public void RemoveExitEvent(GameEvents.ExitEvent eventToRemove)
    {
        OnExitEvent -= eventToRemove;
    }

    public void CallExitEvent()
    {
        if (OnExitEvent != null)
        {
            OnExitEvent();
        }
    }

    public virtual void TeleportObject(GameObject objectToTeleport)
    {
        teleportMap.Remove(objectToTeleport);
    }

    protected IEnumerator objectTeleportRoutine(GameObject objectToTeleport, Vector3 targetLocation)
    {
        Vector3 objectPosition = objectToTeleport.transform.position;
        GameObject teleportEffect = Instantiate(preTeleportEffectPrefab);
        Vector3 preEffectPosition = new Vector3(objectPosition.x, objectPosition.y + preTeleportEffectPrefab.transform.position.y, objectPosition.z);
        teleportEffect.transform.position = preEffectPosition;
        objectToTeleport.SetActive(false);
        yield return new WaitForSeconds(preEffectDuration);
        objectToTeleport.transform.position = new Vector3(targetLocation.x, targetLocation.y, targetLocation.z);
        objectPosition = objectToTeleport.transform.position;
        objectToTeleport.SetActive(true);
        GameObject postTeleportEffect = Instantiate(postTeleportEffectPrefab);
        Vector3 postEffectPosition = new Vector3(objectPosition.x, objectPosition.y + postTeleportEffectPrefab.transform.position.y, objectPosition.z);
        postTeleportEffect.transform.position = postEffectPosition;
    }

    private IEnumerator fadeOutTeleportSound()
    {
        float initialVolume = effectSource.volume;
        float fadeTimeInSeconds = 0.5f;
        float fadeStep = initialVolume / fadeTimeInSeconds;
        int numberOfIntervals = 5;
        for (int i = 0; i < numberOfIntervals; i++)
        {
            effectSource.volume -= fadeStep;
            yield return new WaitForSeconds(fadeTimeInSeconds / numberOfIntervals);
        }
        effectSource.Stop();
        effectSource.volume = initialVolume;
    }

    public void Reveal()
    {
        isHidden = false;
        StartCoroutine(revealRoutine());
    }

    protected IEnumerator revealRoutine()
    {
        soundEffects.PlayOnce(SoundEffectKey.PortalReveal);

        yield return GetComponent<PortalFader>().FadeIn();
        this.enabled = true;
    }

    public void Hide()
    {

    }

    public void Disable()
    {
        isDisabled = true;
        soundEffects.PlayOnce(SoundEffectKey.PortalDisable);
        GetComponent<PortalFader>().FadeToGray();
        this.enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    public void Enable()
    {
        isDisabled = false;
        soundEffects.PlayOnce(SoundEffectKey.PortalEnable);
        StartCoroutine(enableRoutine());
    }

    private IEnumerator enableRoutine()
    {
        yield return GetComponent<PortalFader>().FadeToScheme();
        GetComponent<Collider>().enabled = true;
        this.enabled = true;
    }
}
