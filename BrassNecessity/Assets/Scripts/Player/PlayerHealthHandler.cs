using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField]
    private float baseHealth = 250f;
    public float Health { get; private set; }
    [SerializeField]
    private SoundEffectTrackHandler soundEffects;
    private bool isPorcessingDamageEffects = false;
    private float damageSoundEffectDelayInSeconds = 0.75f;
    private bool isGameOverTriggered = false;
    private float gameOverDelayInSeconds = 1f;

    private void Awake()
    {
        Health = baseHealth;
        if (soundEffects == null)
        {
            soundEffects = FindObjectOfType<SoundEffectTrackHandler>();
        }
    }
    public void DamagePlayer(float damageAmount)
    {
        if (!isGameOverTriggered)
        {
            reduceHealth(damageAmount);
        }
        if (!isPorcessingDamageEffects)
        {
            StartCoroutine(takeDamageRoutine());
        }
        if (shouldTriggerGameOver())
        {
            isGameOverTriggered = true;
            isPorcessingDamageEffects = true;
            StartCoroutine(deathRoutine());
        }
    }

    private void reduceHealth(float damageAmount)
    {
        Health -= damageAmount;
        if (Health < 0f)
        {
            Health = 0f;
        }
    }
    private IEnumerator takeDamageRoutine()
    {
        isPorcessingDamageEffects = true;
        soundEffects.PlayOnce(SoundEffectKey.PlayerInjuredSound);
        yield return new WaitForSeconds(damageSoundEffectDelayInSeconds);
        isPorcessingDamageEffects = false;
    }

    private bool shouldTriggerGameOver()
    {
        return Health <= 0f && !isGameOverTriggered;
    }

    private IEnumerator deathRoutine()
    {
        soundEffects.PlayOnce(SoundEffectKey.PlayerDyingSound);
        yield return new WaitForSeconds(gameOverDelayInSeconds);
        SceneNavigator.OpenScene(SceneKey.GameOver);
    }

    public void HealPlayer(float healAmount)
    {
        Health += healAmount;
        if (Health > baseHealth)
        {
            Health = baseHealth;
        }
    }

    public bool AtMaxHealth()
    {
        return Mathf.Approximately(Health, baseHealth);
    }

    public float GetHealthPercentage()
    {
        return Health / baseHealth;
    }
}
