using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ElementComponent))]
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


    private void Awake()
    {
        if (Application.IsPlaying(gameObject))
        {
            findElementData();
            if (elementInfo != null)
            {
                lastType = elementInfo.ElementInfo.Primary;
            }
            updateBatteryElement();
        }
        else
        {
            if (elementInfo == null)
            {
                elementInfo = GetComponent<ElementComponent>();
            }
            findElementData();
            elementInfo.UpdateElement();
            batteryTanks = GetComponentsInChildren<BatteryPiece>();
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
            elementInfo = GetComponent<ElementComponent>();
        }
        ElementPair element = elementInfo.ElementInfo;
        Color lightColor = data?.GetLight(element) ?? Color.white;
        Material batteryMaterial = data?.GetBatteryMaterial(element);
        for (int i = 0; i < batteryTanks.Length; i++)
        {
            batteryTanks[i].SetMaterial(batteryMaterial);
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
