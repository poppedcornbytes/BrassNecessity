using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BatteryController : MonoBehaviour
{
    [SerializeField]
    private ElementComponent elementInfo;
    private Element.Type lastType;

    [SerializeField]
    private ElementData data;

    [SerializeField]
    private BatteryPiece[] batteryTanks;

    [SerializeField]
    private Light[] batteryLights;

    private void Awake()
    {
        if (Application.IsPlaying(gameObject))
        {
            lastType = elementInfo.ElementInfo.Primary;
            updateBatteryElement();
        }
        else
        {
            findElementData();
            elementInfo = data.TryGetElementPair();
            elementInfo.UpdateElement();
            batteryTanks = GetComponentsInChildren<BatteryPiece>();
            batteryLights = GetComponentsInChildren<Light>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            findElementData();
            updateBatteryElement();
        }
        else if (elementHasChanged())
        {
            updateBatteryElement();
        }
    }

    private void updateBatteryElement()
    {
        if (elementInfo == null)
        {
            Debug.Log("?????");
            Debug.Break();
        }
        ElementPair element = elementInfo.ElementInfo;
        Color lightColor = data.GetLight(element);
        Material batteryMaterial = data.GetBatteryMaterial(element);
        for (int i = 0; i < batteryTanks.Length; i++)
        {
            batteryTanks[i].SetMaterial(batteryMaterial);
        }
        for (int i = 0; i < batteryLights.Length; i++)
        {
            batteryLights[i].color = lightColor;
        }
        lastType = element.Primary;
    }

    private void findElementData()
    {
        data = GetComponentInParent<ElementData>();
        if (data == null)
        {
            data = FindObjectOfType<ElementData>();
        }
    }

    private bool elementHasChanged()
    {
        bool hasChanged = false;
        if (elementInfo != null && elementInfo.ElementInfo != null)
        {
            hasChanged = lastType != elementInfo.ElementInfo.Primary;
        }
        else
        {
            if (elementInfo == null)
            {
                Debug.LogError("The element component was null.");
            }
            else if (elementInfo.ElementInfo == null)
            {
                Debug.LogError("The element pair was null");
            }
        }
        return hasChanged;
    }
}
