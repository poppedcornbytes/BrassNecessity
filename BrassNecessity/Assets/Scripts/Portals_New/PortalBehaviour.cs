using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewPortals
{
    public class PortalBehaviour : MonoBehaviour, IPortal, IExitEventHandler
    {
        [SerializeField]
        protected float preTeleportTimeout = 2f;
        [SerializeField]
        protected float preEffectDuration = 1f;
        [SerializeField]
        protected AudioSource effectSource;
        [SerializeField]
        protected SoundEffectTrackHandler soundEffects;
        protected Dictionary<GameObject, FrameTimeoutHandler> teleportMap;
        [SerializeField]
        protected bool isHidden = false;
        [SerializeField]
        protected bool isDisabled = false;
        [SerializeField]
        protected PortalController _portalController;
        public bool IsHidden { get => isHidden; }
        public bool IsDisabled { get => isDisabled; }
        private event GameEvents.ExitEvent OnExitEvent;

        protected virtual void Awake()
        {
            if (soundEffects == null)
            {
                soundEffects = FindObjectOfType<SoundEffectTrackHandler>();
            }
            if (_portalController == null) 
            {
                _portalController = GetComponentInChildren<PortalController>();
            }
            if (effectSource == null)
            {
                effectSource = GetComponent<AudioSource>();
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
                _portalController.IsIrisStable = false;
                _portalController.SetIrisFollowObject(teleportingObject);
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
            if (other.gameObject.tag == "Player")
            {
                _portalController.IsIrisStable = true;
            }
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
            _portalController.SetIrisFollowObject(null);
            _portalController.IsIrisStable = true;
            teleportMap.Remove(objectToTeleport);
        }

        protected IEnumerator objectTeleportRoutine(GameObject objectToTeleport, Vector3 targetLocation)
        {
            Vector3 objectPosition = objectToTeleport.transform.position;
            Vector3 preEffectPosition = new Vector3(objectPosition.x, objectPosition.y, objectPosition.z);
            _portalController.StartTeleport(preEffectPosition);
            objectToTeleport.SetActive(false);
            yield return new WaitForSeconds(preEffectDuration);
            objectToTeleport.transform.position = new Vector3(targetLocation.x, targetLocation.y, targetLocation.z);
            objectPosition = objectToTeleport.transform.position;
            Vector3 postEffectPosition = new Vector3(objectPosition.x, objectPosition.y, objectPosition.z);
            _portalController.StartArrive(postEffectPosition);
            yield return new WaitForSeconds(0.25f);
            objectToTeleport.SetActive(true);
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
            _portalController.Reveal();
        }

        public void Hide()
        {
            isHidden = true; 
            _portalController.Hide();
        }

        public virtual void Disable()
        {
            isDisabled = true;
            _portalController.Deactivate();
            soundEffects.PlayOnce(SoundEffectKey.PortalDisable);
            this.enabled = false;
            GetComponent<Collider>().enabled = false;
        }

        public void Enable()
        {
            isDisabled = false;
            _portalController.Activate();
            soundEffects.PlayOnce(SoundEffectKey.PortalEnable);
            GetComponent<Collider>().enabled = true;
            this.enabled = true;
        }
    }
}
