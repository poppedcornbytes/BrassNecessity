using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PortalFader : MonoBehaviour
{
    [SerializeField]
    private PortalComponents components;
    [SerializeField]
    private float fadeDurationInSeconds = 3f;
    private bool lastHiddenStatus;
    private bool lastDisabledStatus;


    private void Awake()
    {
        initiateStatus();
    }

    private void Start()
    {
        initiateStatus(); 
    }

    private void initiateStatus()
    {
        if (components.Portal == null)
        {
            components.Portal = GetComponent<PortalBehaviour>();
        }
        if (components.Portal.IsHidden)
        {
            QuickFadeOut();
            lastHiddenStatus = components.Portal.IsHidden;
        }
        else if (components.Portal.IsDisabled)
        {
            QuickGrayFade();
            lastHiddenStatus = components.Portal.IsDisabled;
        }
    }

    private void Update()
    {
        if (!Application.IsPlaying(this))
        {
            if (components.Portal.IsHidden && !lastHiddenStatus) //the only combination that should hide.
            {
                QuickFadeOut();
                lastHiddenStatus = components.Portal.IsHidden;
            }
            else if (components.Portal.IsDisabled && !lastDisabledStatus)
            {
                QuickGrayFade();
                lastDisabledStatus = components.Portal.IsDisabled;
            }
        }
    }

    public void QuickFadeOut()
    {
        components.PortalSpecks.Stop();
        components.PortalRays.Stop();
        components.PortalRunes.Stop();
        components.PortalRipples.Stop();
        components.SpriteAnimator.enabled = false;
        components.InnerPortal.color = getTransparentColor(components.InnerPortal.color);
        components.OuterPortal.color = getTransparentColor(components.OuterPortal.color);
        components.PortalMarks.color = getTransparentColor(components.PortalMarks.color);
    }

    public void QuickGrayFade()
    {
        PortalColorScheme grayScheme = components.PortalThemes.GetDisabledScheme();
        components.PortalStyle.SetSpecificTheme(grayScheme);
        components.PortalSpecks.Stop();
        components.PortalRays.Stop();
        components.PortalRunes.Simulate(1f);
        components.PortalRipples.Stop();
        components.SpriteAnimator.speed = 0f;
    }

    public void FadeOut()
    {
        components.PortalSpecks.Stop();
        components.PortalRays.Stop();
        components.SpriteAnimator.enabled = false;
        StartCoroutine(fadeOutRoutine());
    }

    private IEnumerator fadeOutRoutine()
    {
        float effectTime = fadeDurationInSeconds / 3f;
        yield return new WaitForSeconds(effectTime / 2);
        components.PortalRunes.Stop();
        yield return new WaitForSeconds(effectTime / 2);
        components.PortalRipples.Stop();
        float remainingTime = fadeDurationInSeconds * 2 / 3;
        Color innerColor = components.InnerPortal.color;
        Color outerColor = components.OuterPortal.color;
        Color marksColor = components.PortalMarks.color;
        for (float i = 0; i < remainingTime; i += 0.1f)
        {
            float intervalStep = i / remainingTime;
            components.InnerPortal.color = Color.Lerp(innerColor, Color.clear, intervalStep);
            components.OuterPortal.color = Color.Lerp(outerColor, Color.clear, intervalStep);
            components.PortalMarks.color = Color.Lerp(marksColor, Color.clear, intervalStep);
            components.SpriteAnimator.speed = Mathf.Lerp(1f, 0f, intervalStep);
            yield return new WaitForSeconds(0.1f);
        }
        components.InnerPortal.color = getTransparentColor(innerColor);
        components.OuterPortal.color = getTransparentColor(outerColor);
        components.PortalMarks.color = getTransparentColor(marksColor);
    }

    public IEnumerator FadeIn()
    {
        yield return fadeInRoutein();
    }

    private IEnumerator fadeInRoutein()
    {
        float remainingTime = fadeDurationInSeconds * 2 / 3;
        Color innerColor = getSolidColor(components.InnerPortal.color);
        Color outerColor = getSolidColor(components.OuterPortal.color);
        Color marksColor = getSolidColor(components.PortalMarks.color);
        for (float i = 0; i < remainingTime; i += 0.1f)
        {
            float intervalStep = i / remainingTime;
            components.InnerPortal.color = Color.Lerp(Color.clear, innerColor, intervalStep);
            components.OuterPortal.color = Color.Lerp(Color.clear, outerColor, intervalStep);
            components.PortalMarks.color = Color.Lerp(Color.clear, marksColor, intervalStep);
            yield return new WaitForSeconds(0.1f);
        }
        components.InnerPortal.color = innerColor;
        components.OuterPortal.color = outerColor;
        components.PortalMarks.color = marksColor;
        components.PortalRunes.Play();
        float effectTime = fadeDurationInSeconds / 3;
        yield return new WaitForSeconds(effectTime);
        components.PortalRipples.Play();
        components.PortalSpecks.Play();
        components.PortalRays.Play();
        components.SpriteAnimator.enabled = true;
    }

    public void FadeToGray()
    {
        PortalColorScheme disabledScheme = components.PortalThemes.GetDisabledScheme();
        StartCoroutine(fadeToGrayRoutine(disabledScheme));
    }

    private IEnumerator fadeToGrayRoutine(PortalColorScheme colorToFade)
    {

        components.PortalRipples.Stop();
        components.PortalRays.Stop();
        components.PortalSpecks.Stop();
        Color innerColor = components.InnerPortal.color;
        Color outerColor = components.OuterPortal.color;
        Color marksColor = components.PortalMarks.color;
        float remainingTime = fadeDurationInSeconds * 2 / 3;
        components.PortalStyle.SetRuneColor(colorToFade);
        components.PortalRunes.Play();
        var runeMain = components.PortalRunes.main;
        for (float i = 0; i < fadeDurationInSeconds; i += 0.1f)
        {
            float intervalFraction = i / remainingTime;
            components.InnerPortal.color = Color.Lerp(innerColor, colorToFade.PrimaryColor, intervalFraction);
            components.OuterPortal.color = Color.Lerp(outerColor, colorToFade.SecondaryColor, intervalFraction);
            components.PortalMarks.color = Color.Lerp(marksColor, colorToFade.AccentColor, intervalFraction);
            runeMain.simulationSpeed = Mathf.Lerp(1f, 0f, intervalFraction);
            components.SpriteAnimator.speed = Mathf.Lerp(1f, 0f, intervalFraction);
            yield return new WaitForSeconds(0.1f);
        }
        components.PortalStyle.SetAllSpriteColors(colorToFade);
    }

    public IEnumerator FadeToScheme()
    {
        int currentSchemeIndex = components.PortalStyle.PortalThemeIndex;
        PortalColorScheme currentScheme = components.PortalThemes.GetColorSchemeAtIndex(currentSchemeIndex);
        yield return fadeToSchemeRoutine(currentScheme);
    }

    private IEnumerator fadeToSchemeRoutine(PortalColorScheme colorToReturn)
    {

        Color innerColor = components.InnerPortal.color;
        Color outerColor = components.OuterPortal.color;
        Color marksColor = components.PortalMarks.color;
        float fadeTime = fadeDurationInSeconds * 2 / 3;
        components.PortalStyle.SetRayColors(colorToReturn);
        components.PortalStyle.SetRuneColor(colorToReturn);
        components.PortalRunes.Play();
        var runeMain = components.PortalRunes.main;
        for (float i = 0; i < fadeDurationInSeconds; i += 0.1f)
        {
            float intervalFraction = i / fadeTime;
            components.InnerPortal.color = Color.Lerp(innerColor, colorToReturn.PrimaryColor, intervalFraction);
            components.OuterPortal.color = Color.Lerp(outerColor, colorToReturn.SecondaryColor, intervalFraction);
            components.PortalMarks.color = Color.Lerp(marksColor, colorToReturn.AccentColor, intervalFraction);
            runeMain.simulationSpeed = Mathf.Lerp(0f, 1f, intervalFraction);
            components.SpriteAnimator.speed = Mathf.Lerp(0f, 1f, intervalFraction);
            yield return new WaitForSeconds(0.1f);
        }
        components.PortalStyle.SetAllSpriteColors(colorToReturn);
        components.PortalRipples.Play();
        components.PortalRays.Play();
        components.PortalSpecks.Play();
    }

    private Color getTransparentColor(Color originalColor)
    {
        return new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private Color getSolidColor(Color transparentColor)
    {
        return new Color(transparentColor.r, transparentColor.g, transparentColor.b, 1f);
    }
}
