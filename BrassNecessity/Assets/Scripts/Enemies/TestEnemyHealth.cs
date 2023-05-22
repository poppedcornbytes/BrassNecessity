using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyHealth : EnemyHealthHandler
{
    [SerializeField]
    private float respawnTimeInSeconds = 3f;
    public override void DamageEnemy(float damageAmount)
    {
        takeDamage(damageAmount);
        if (IsDead)
        {
            StartCoroutine(hideEnemy());
        }
    }

    private IEnumerator hideEnemy()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Collider collider = GetComponent<Collider>();
        mesh.enabled = false;
        collider.enabled = false;
        setActiveOnAllChildren(false);
        yield return new WaitForSeconds(respawnTimeInSeconds);
        mesh.enabled = true;
        collider.enabled = true;
        Health = baseHealth;
        IsDead = false;
        setActiveOnAllChildren(true);
    }

    private void setActiveOnAllChildren(bool isActive)
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] != transform)
            {
                children[i].gameObject.SetActive(isActive);
            }
        }
    }
}
