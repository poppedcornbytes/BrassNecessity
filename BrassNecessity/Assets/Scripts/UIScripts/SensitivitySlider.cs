using UnityEngine;
using UnityEngine.UIElements;

public class SensitivitySlider : GenericSlider
{
    public SensitivitySlider(SliderData sliderData, SliderInt sliderElement) : base(sliderData, sliderElement) { }

    public override void SetValue(int newValue)
    {
        base.SetValue(newValue);
        Debug.Log("Set sensivity to " + newValue);
        SettingsHandler.SensitivitySetting = newValue;
    }
}
