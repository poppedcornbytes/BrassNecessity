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
    [SerializeField]
    private SceneKey sceneToOpen = SceneKey.GameLevel;

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
            if (input.shoot)
            {
                overviewExited = true;
                soundEffects.PlayOnce(SoundEffectKey.ButtonSelect);
                SceneNavigator.OpenScene(sceneToOpen);
            }
        }
    }
}
