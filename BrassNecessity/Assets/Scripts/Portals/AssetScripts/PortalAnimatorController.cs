using UnityEngine;
using UnityEngine.VFX;

public class PortalAnimatorController : MonoBehaviour
{
    [SerializeField]
    private string revealTrigger = "Reveal";
    [SerializeField]
    private string hideTrigger = "Hide";
    [SerializeField]
    private string hiddenTrigger = "Hidden";
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private VisualEffect moveEffect;


    public void PlayReveal()
    {
        animator.SetTrigger(revealTrigger);
    }

    public void AutoHide() 
    {
        animator.SetTrigger(hiddenTrigger);
    } 

    public void PlayHide()
    {
        animator.SetTrigger(hideTrigger);
    }

    private void OnMoveStartEvent()
    {
        moveEffect.Play();
    }

    private void OnMoveEndEvent()
    {
        moveEffect.Stop();
    }
}
