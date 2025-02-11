using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUIHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerHealthHandler playerHealth;
    private VisualElement healthBarParentContainer;
    private VisualElement healthBarFill;
    private Label healthLabel;

    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        healthBarParentContainer = root.Q<VisualElement>("HealthBar");
        healthBarFill = healthBarParentContainer.Q<VisualElement>("FillContainer");
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealthHandler>();
        }
    }

    private void Update()
    {
        float healthPercentage = playerHealth.GetHealthPercentage();
        if (healthPercentage > 1)
        {
            healthPercentage = 1;
        }
        else if (healthPercentage < 0)
        {
            healthPercentage = 0;
        }
        healthBarFill.style.width = new Length(healthPercentage * 100, LengthUnit.Percent);
    }
}
