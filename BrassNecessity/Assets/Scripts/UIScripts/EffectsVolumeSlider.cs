using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EffectsVolumeSlider : GenericSlider
{
    public EffectsVolumeSlider(SliderData sliderData, SliderInt sliderElement) : base(sliderData, sliderElement) 
    {
        SetValue(SettingsHandler.EffectsVolumeSetting);
    }
    public override void SetValue(int newValue)
    {
        base.SetValue(newValue);
        Debug.Log("Set FX volume to " + newValue);
        SettingsHandler.EffectsVolumeSetting = newValue;
    }
}
