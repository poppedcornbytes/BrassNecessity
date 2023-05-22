using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MusicVolumeSlider : GenericSlider
{
    public MusicVolumeSlider(SliderData sliderData, SliderInt sliderElement) : base(sliderData, sliderElement) 
    {
        SetValue(SettingsHandler.MusicVolumeSetting);
    }
    public override void SetValue(int newValue)
    {
        base.SetValue(newValue);
        Debug.Log("Updated music volume to " + newValue);
        SettingsHandler.MusicVolumeSetting = newValue;
    }
}
