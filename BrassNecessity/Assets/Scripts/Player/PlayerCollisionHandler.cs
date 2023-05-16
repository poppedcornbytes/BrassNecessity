using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private CharacterController controller;
    private ElementApplyState applyState;
    private PlayerHealthHandler healthHandler;
    [SerializeField]
    private MusicTrackHandler musicTracks;
    [SerializeField]
    private LayerMask enemeyMask;
    [SerializeField]
    private float enemyWarningRadius = 20f;
    private bool enemyWarningOn = false;
    
    [SerializeField]
    private SoundEffectTrackHandler soundEffects;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        applyState = GetComponentInChildren<ElementApplyState>();
        healthHandler = GetComponent<PlayerHealthHandler>();
        if (soundEffects == null)
        {
            soundEffects = FindObjectOfType<SoundEffectTrackHandler>();
        }
        if (musicTracks == null)
        {
            musicTracks = FindObjectOfType<MusicTrackHandler>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        detectPickupCollision();
        detectEnemyNearbyCollision();
    }

    private void detectPickupCollision()
    {
        RaycastHit collision;
        Vector3 boxCenter = new Vector3(transform.position.x, transform.position.y + controller.height / 2, transform.position.z);
        Vector3 extents = new Vector3(controller.radius / 2, controller.height / 4, controller.radius / 2);
        if (Physics.BoxCast(boxCenter, extents, transform.forward, out collision, Quaternion.identity, .25f))
        {
            ElementPickup elementItem;
            if (collision.collider.TryGetComponent<ElementPickup>(out elementItem))
            {
                if (!applyState.AtMaximumElements())
                {
                    soundEffects.PlayOnce(SoundEffectKey.ElementPickup);
                    ElementComponent element = elementItem.Element;
                    applyState.AddElement(element);
                    elementItem.PickupItem();
                }
            }
            HealthPickup healthItem;
            if (collision.collider.TryGetComponent<HealthPickup>(out healthItem))
            {
                if (!healthHandler.AtMaxHealth())
                {
                    healthHandler.HealPlayer(healthItem.HealthValue);
                    healthItem.PickupItem();
                }
            }
        }
    }

    private void detectEnemyNearbyCollision()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, enemyWarningRadius, enemeyMask);
        if (hitObjects.Length > 0)
        {
            if (!enemyWarningOn)
            {
                enemyWarningOn = true;
                StartCoroutine(switchMusicRoutine(MusicKey.Adventure));
            }
        }
        else
        {
            if (enemyWarningOn)
            {

                enemyWarningOn = false;
                StartCoroutine(switchMusicRoutine(MusicKey.Peaceful));
            }
        }
    }

    private IEnumerator switchMusicRoutine(MusicKey keyToPlay)
    {
        yield return musicTracks.StopTrack();
        musicTracks.PlayTrack(keyToPlay);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyWarningRadius);
    }
}
