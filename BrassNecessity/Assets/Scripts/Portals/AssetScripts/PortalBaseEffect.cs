using UnityEngine;
using UnityEngine.VFX.Utility;

public class PortalBaseEffect : PortalEffect
{
    [SerializeField]
    private ExposedProperty colorParameter = "ParticleColor";
    [SerializeField]
    private ExposedProperty aliveParameter = "Alive";
    [SerializeField]
    private ExposedProperty irisPositionParameter = "IrisPosition";

    public Color EffectColor
    {
        set
        {
            baseEffect.SetVector4(colorParameter, value);
        }
    }
    
    public bool Alive
    {
        set
        {
            baseEffect.SetBool(aliveParameter, value);
        }
    }
    
    public Vector3 IrisPosition 
    { 
        set
        {
            baseEffect.SetVector3(irisPositionParameter, value);
        } 
    }
}
