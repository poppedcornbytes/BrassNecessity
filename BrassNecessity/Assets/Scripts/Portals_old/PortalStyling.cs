using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PortalStyling : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem portalRays;
    [SerializeField]
    private ParticleSystem portalRunes;
    [SerializeField]
    private SpriteRenderer innerPortal;
    [SerializeField]
    private SpriteRenderer outerPortal;
    [SerializeField]
    private SpriteRenderer portalMarkers;
    [SerializeField]
    private int portalThemeIndex;
    public int PortalThemeIndex { get => portalThemeIndex; }
    private int lastThemeIndex;
    [SerializeField]
    private PortalThemes themeList;

    private void Awake()
    {
        if (themeList == null)
        {
            themeList = FindObjectOfType<PortalThemes>();
        }
        setColors();
    }

    // Update is called once per frame
    void Update()
    {
        if (portalThemeIndex != lastThemeIndex)
        {
            setColors();
            lastThemeIndex = portalThemeIndex;
        }
    }

    public void SetThemeIndex(int themeIndex)
    {
        portalThemeIndex = themeIndex;
    }

    public void SetSpecificTheme(PortalColorScheme schemeToSet)
    {
        SetAllSpriteColors(schemeToSet);
        SetRuneColor(schemeToSet);
        SetRayColors(schemeToSet);
    }

    private void setColors()
    {
        if (portalThemeIndex < 0 || portalThemeIndex > themeList.Count)
        {
            portalThemeIndex = 0;
        }
        PortalColorScheme scheme = themeList.GetColorSchemeAtIndex(portalThemeIndex);
        SetSpecificTheme(scheme);
        portalRunes.Play();
        portalRays.Play();
        lastThemeIndex = portalThemeIndex;
    }

    public void SetAllSpriteColors(PortalColorScheme schemeToSet)
    {
        setSpriteColor(innerPortal, schemeToSet.PrimaryColor);
        setSpriteColor(outerPortal, schemeToSet.SecondaryColor);
        setSpriteColor(portalMarkers, schemeToSet.AccentColor);
    }

    private void setSpriteColor(SpriteRenderer spriteToAdjust, Color colorToAdd)
    {
        spriteToAdjust.color = colorToAdd;
    }

    public void SetRuneColor(PortalColorScheme colorToAdd)
    {
        var runeMain = portalRunes.main;
        Gradient runeColor = runeMain.startColor.gradient;
        GradientColorKey[] runeColorKeys = getParticleGradient(runeColor.colorKeys, colorToAdd.AccentColor);
        runeColor.SetKeys(runeColorKeys, runeColor.alphaKeys);
        runeMain.startColor = runeColor;
        resetParticleSystem(portalRunes);
    }

    public void SetRayColors(PortalColorScheme colorToAdd)
    {
        Color maxColor = colorToAdd.SecondaryColor;
        Color minColor = colorToAdd.AccentColor;
        var rayMain = portalRays.main;
        rayMain.startColor = new ParticleSystem.MinMaxGradient(maxColor, minColor);
        resetParticleSystem(portalRays);
    }

    private GradientColorKey[] getParticleGradient(GradientColorKey[] oldColors, Color colorToAdd)
    {
        GradientColorKey[] newColors = new GradientColorKey[oldColors.Length];
        for (int i = 0; i < oldColors.Length; i++)
        {
            newColors[i].color = colorToAdd;
        }
        return newColors;
    }

    private void resetParticleSystem(ParticleSystem systemToReset)
    {
        systemToReset.Stop();
    }

}
