using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BrightnessSlider : GenericSlider
{
    public BrightnessSlider(SliderData sliderData, SliderInt sliderElement) : base(sliderData, sliderElement) { } 
    public override void SetValue(int newValue)
    {
        base.SetValue(newValue);
        Debug.Log("Set brightness to " + newValue);
        SettingsHandler.BrightnessSetting = newValue;
    }
}
