using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyHealthUIHandler : MonoBehaviour
{
    [SerializeField]
    private EnemyHealthHandler enemyHealth;
    [SerializeField]
    private Image healthBarImage;
    [SerializeField]
    private Image healthBarIcon;
    [SerializeField]
    private ElementComponent enemyElement;
    [SerializeField]
    private Color defaultColour = Color.red;
    private ElementData elementDataReference;
    [SerializeField]
    private Vector3 lookAtVectorEuler = Vector3.zero;


    private void Start()
    {
        elementDataReference = FindObjectOfType<ElementData>();
        setHealthBarIcon();
        setHealthBarColour();
        setHealthBarFill();
    }

    private void Update()
    {
        rotateHealth();
        setHealthBarFill();
        if (enemyHealth.Health <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void setHealthBarIcon()
    {
        if (enemyElement != null)
        {
            healthBarIcon.sprite = elementDataReference.GetIcon(enemyElement.ElementInfo);
        }
    }

    private void setHealthBarColour()
    {
        Color colourToSet;
        if (enemyElement != null)
        {
            colourToSet = elementDataReference.GetLight(enemyElement.ElementInfo);
        }
        else
        {
            colourToSet = defaultColour;
        }
        healthBarIcon.color = colourToSet;
        healthBarImage.color = colourToSet;
    }

    private void setHealthBarFill()
    {
        healthBarImage.fillAmount = enemyHealth.GetRemainingHealthPercentage();
    }

    private void rotateHealth()
    {
        transform.rotation = Quaternion.Euler(lookAtVectorEuler);
    }
}
