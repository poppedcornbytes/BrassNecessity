using UnityEngine;
using UnityEngine.VFX.Utility;

public class PortalTetherEffect : PortalEffect
{
    [SerializeField]
    private ExposedProperty arcRadiusParameter = "TetherArcSize";
    [SerializeField]
    private ExposedProperty arcCenterParameter = "TetherArcCenter";
    [SerializeField]
    private ExposedProperty colorParameter = "ParticleColour";

    public float ArcRadius
    {
        set
        {
            baseEffect.SetFloat(arcRadiusParameter, value);
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
        Vector2 arcCenterValue = baseEffect.GetVector2(arcCenterParameter);
        Vector3 arcCenter = transform.TransformPoint(arcCenterValue);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.right * 10);
        Gizmos.color = Color.red;
        float arcRadius = baseEffect.GetFloat(arcRadiusParameter);
        Vector3 direcitonUnit = (arcCenter - transform.position).normalized;
        Vector3 arcDiameterEnd = transform.position + (direcitonUnit * arcRadius * 2); 
        Gizmos.DrawLine(transform.position, arcDiameterEnd);
        Gizmos.DrawSphere(arcCenter, 0.5f);
    }
}
