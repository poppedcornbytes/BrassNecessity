using UnityEngine;
using UnityEngine.VFX.Utility;

public class PortalTetherEffect : PortalEffect
{
    [SerializeField]
    private ExposedProperty arcSizeParameter = "TetherArcSize";
    [SerializeField]
    private ExposedProperty arcCenterParameter = "TetherArcCenter";
    [SerializeField]
    private ExposedProperty colorParameter = "ParticleColour";

    public float ArcSize
    {
        set
        {
            baseEffect.SetFloat(arcSizeParameter, value);
        }
    }

    public Vector2 ArcCenter
    {
        set
        {
            baseEffect.SetVector2(arcCenterParameter, value);
        }
    }

    public Color EffectColor
    {
        set
        {
            baseEffect.SetVector4(colorParameter, value);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 arcCenter = transform.TransformPoint(baseEffect.GetVector2(arcCenterParameter));
        Gizmos.DrawRay(arcCenter, transform.up);
        Gizmos.DrawRay(arcCenter, transform.right);
    }
}
