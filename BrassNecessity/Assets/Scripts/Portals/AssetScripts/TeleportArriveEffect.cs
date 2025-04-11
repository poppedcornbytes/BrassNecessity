using UnityEngine;
using UnityEngine.VFX.Utility;

public class TeleportArriveEffect : PortalEffect
{
    [SerializeField]
    private ExposedProperty burstLocationParameter = "BurstLocation";
    [SerializeField]
    private ExposedProperty colorParameter = "ParticleColour";
    
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
            baseEffect.SetVector4(colorParameter, value);
        }
    }
}
