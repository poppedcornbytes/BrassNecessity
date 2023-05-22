using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class BrightnessHandler : MonoBehaviour
{
    [SerializeField]
    private float minimumValue = -2;

    [SerializeField]
    private float maximumValue = 2;

    [SerializeField]
    private Volume renderVolume;
    private ColorAdjustments postExposureProfile;

    private int lastSettingValue;

    // Start is called before the first frame update
    void Start()
    {
        lastSettingValue = SettingsHandler.BrightnessSetting;
        renderVolume.profile.TryGet<ColorAdjustments>(out postExposureProfile);
        postExposureProfile.postExposure.value = convertSettingToRange();
    }

    // Update is called once per frame
    void Update()
    {
        if (settingChanged())
        {
            postExposureProfile.postExposure.value = convertSettingToRange();
        }
    }

    private bool settingChanged()
    {
        return lastSettingValue != SettingsHandler.BrightnessSetting;
    }

    private float convertSettingToRange()
    {
        float valueRange = maximumValue - minimumValue;
        return (SettingsHandler.GetBrightnessFraction() * valueRange) - maximumValue;
    }
}
