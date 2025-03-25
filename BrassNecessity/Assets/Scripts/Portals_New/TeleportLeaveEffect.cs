using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class TeleportLeaveEffect : PortalEffect
{
    [SerializeField]
    private ExposedProperty burstLocationParameter = "BurstLocation";
    [SerializeField]
    private ExposedProperty colorProperty = "ParticleColour";

    public Vector3 BurstLocation
    {
        set
        {
            baseEffect.SetVector3(burstLocationParameter, value);
        }
    }

    public Color EffectColor
    {
        set
        {
            baseEffect.SetVector4(colorProperty, value);
        }
    }
}
