using UnityEngine;
using UnityEngine.VFX;

public class PortalEffect : MonoBehaviour
{
    [SerializeField]
    protected VisualEffect baseEffect;

    private void OnEnable()
    {
        if (baseEffect == null)
        {
            baseEffect = GetComponent<VisualEffect>();
        }
    }

    public void Play()
    {
        baseEffect.Play();
    }

    public void Stop()
    {
        baseEffect.Stop();
    }
}
