using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSeekBehaviour : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer laserRender;

    [SerializeField]
    private VolumetricLines.VolumetricLineBehavior laserBeam;

    [SerializeField]
    private GameObject laserSplash;

    [SerializeField]
    private LayerMask ignoreLayers;

    [SerializeField]
    private float baseDamagePerSecond = 15f;

    [SerializeField]
    private ElementComponent weaponElement;

    [SerializeField]
    private ImpactBehaviour impactEffect;

    [SerializeField]
    private LaserSoundBehaviour soundEffects;

    private EnemyHealthHandler hitEnemy;
    private LaserGateBehaviour hitGate;

    private void Start()
    {
        ignoreLayers = ~ignoreLayers;
        if (weaponElement == null)
        {
            weaponElement = GetComponentInParent<ElementComponent>();
        }
    }

    public void SeekTarget()
    {
        gameObject.SetActive(true);
        soundEffects.StartLoopLaserFiringEffect();
        RaycastHit target;
        Vector3 raycastStart = transform.position;
        Vector3 raycastDirection = transform.TransformDirection(Vector3.up);
        bool isHittingGate = false;
        bool isHittingEnemy = false;
        if (Physics.Raycast(raycastStart, raycastDirection, out target, Mathf.Infinity, ignoreLayers))
        {
            displayLaser(target);
            isHittingEnemy = target.collider.TryGetComponent(out hitEnemy);
            isHittingGate = !isHittingEnemy && target.collider.TryGetComponent(out hitGate);
        }
        if (isHittingEnemy)
        {
            handleEnemyCollision(target);
        }
        else if (isHittingGate)
        {
            handleGateCollision(target);
        }
        else
        {
            soundEffects.NormaliseLaserFiringSoundPitch();
            impactEffect.ResetEffects();
        }
        laserBeam.UpdateLineScale();
    }

    private void displayLaser(RaycastHit target)
    {
        laserRender.enabled = true;
        laserSplash.SetActive(true);
        float targetDistance = target.distance / transform.lossyScale.y;
        laserSplash.transform.localPosition = new Vector3(0, targetDistance, 0);
        Vector3 newEndPos = new Vector3(0, targetDistance, 0);
        laserBeam.EndPos = newEndPos;
    }

    private void handleEnemyCollision(RaycastHit target)
    {
        ElementPair enemyElement = target.collider.GetComponent<ElementComponent>().ElementInfo;
        ElementPair laserElement = weaponElement.ElementInfo;
        float multiplier = laserElement.GetAttackingMultiplier(enemyElement);
        impactEffect.SetImpactEffects(multiplier);
        soundEffects.ChangeLaserFiringSoundWithMultiplier(multiplier);
        float damage = baseDamagePerSecond * multiplier * Time.deltaTime;
        hitEnemy.DamageEnemy(damage);
    }

    private void handleGateCollision(RaycastHit target)
    {
        ElementPair laserElement = weaponElement.ElementInfo;
        ElementPair gateElement = target.collider.GetComponent<ElementComponent>().ElementInfo;
        if (laserElement.Primary == gateElement.Primary)
        {
            hitGate.IncreaseUnlockProgress();
        }
    }

    public void FinishSeeking()
    {
        Vector3 oldStart = laserBeam.StartPos;
        Vector3 targetStart = new Vector3(0, laserBeam.EndPos.y, 0);
        laserBeam.StartPos = targetStart;
        laserSplash?.SetActive(false);
        laserRender.enabled = false;
        gameObject.SetActive(false);
        laserBeam.StartPos = oldStart;
        impactEffect.ResetEffects();
        soundEffects.StopLoopLaserFiringEffect();
        hitEnemy = null;
        hitGate?.StartRevertingUnlockProgress();
        hitGate = null;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 raycastStart = transform.position;
        Vector3 raycastDirection = transform.TransformDirection(Vector3.up);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastStart, raycastDirection);
        if (Physics.Raycast(raycastStart, raycastDirection, out RaycastHit target, Mathf.Infinity, ignoreLayers))
        {
            float targetDistance = target.distance / transform.lossyScale.y;
            Vector3 newEndPos = transform.TransformVector(new Vector3(0, targetDistance, 0)); ;
            Gizmos.DrawSphere(newEndPos, .1f);
        }
    }
}
