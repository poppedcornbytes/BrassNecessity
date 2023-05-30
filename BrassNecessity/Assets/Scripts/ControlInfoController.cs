using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInfoController : MonoBehaviour
{
    [SerializeField]
    private PlayerControllerInputs input;
    [SerializeField]
    private SoundEffectTrackHandler soundEffects;
    private bool overviewExited = false;

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
        if (!overviewExited)
        {
            if (input.applyElement)
            {
                overviewExited = true;
                soundEffects.PlayOnce(SoundEffectKey.ButtonSelect);
                SceneNavigator.OpenScene(SceneKey.MainGame);
            }
        }
    }
}
