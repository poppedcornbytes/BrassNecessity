using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField]
    private PlayerControllerInputs input;
    [SerializeField]
    private SoundEffectTrackHandler soundEffects;
    private bool creditsExited = false;

    private void Awake()
    {
        if (soundEffects == null)
        {
            soundEffects = FindObjectOfType<SoundEffectTrackHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!creditsExited)
        {
            if (input.shoot || input.strafe)
            {
                creditsExited = true;
                soundEffects.PlayOnce(SoundEffectKey.ButtonSelect);
                SceneNavigator.OpenScene(SceneKey.StartMenu);
            }
        }

    }
}
