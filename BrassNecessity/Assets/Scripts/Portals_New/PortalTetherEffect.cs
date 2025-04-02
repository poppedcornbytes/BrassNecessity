using UnityEngine;
using UnityEngine.VFX.Utility;

public class PortalTetherEffect : PortalEffect
{
    [SerializeField]
    private ExposedProperty arcSizeParameter = "TetherArcSize";
    [SerializeField]
    private ExposedProperty colorParameter = "ParticleColour";

    public float ArcSize
    {
        set
        {
            baseEffect.SetFloat(arcSizeParameter, value);
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
