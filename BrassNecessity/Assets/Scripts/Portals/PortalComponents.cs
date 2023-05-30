using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalComponents : MonoBehaviour
{
    public PortalBehaviour Portal;
    public PortalThemes PortalThemes;
    public PortalStyling PortalStyle;
    public ParticleSystem PortalSpecks;
    public ParticleSystem PortalRays;
    public ParticleSystem PortalRunes;
    public ParticleSystem PortalRipples;
    public SpriteRenderer InnerPortal;
    public SpriteRenderer OuterPortal;
    public SpriteRenderer PortalMarks;
    public Animator SpriteAnimator;

    private void Awake()
    {
        if (PortalThemes == null)
        {
            PortalThemes = FindObjectOfType<PortalThemes>();
        }
    }
}
