using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerHealthHandler : EnemyHealthHandler
{
    EnemyController enemyController;


    protected override void Awake()
    {
        base.Awake();
        enemyController = GetComponent<EnemyController>();
    }

    public override void DamageEnemy(float damageAmount)
    {
        if (IsDead)
        {
            // Prevents the enemy still getting repeatedly hit while the death animation is playing.            
            return;
        }
        
        
        if (enemyController.currentState != enemyController.GotHitState)
        {
            // This is the first frame of the enemy being hit by the laser, so update the EnemyController
            enemyController.LaserContactBegins();
        }
        
        Health -= damageAmount;
        if (Health <= 0)
        {
            Health = 0;
            IsDead = true;
            soundEffects.PlayOnce(SoundEffectKey.EnemyDyingSound);
            DropItem();
            enemyController.EnemyHasDied();
        }
    }

}
