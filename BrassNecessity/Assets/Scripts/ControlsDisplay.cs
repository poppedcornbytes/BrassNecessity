using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsDisplay : MonoBehaviour
{
    [SerializeField]
    private float inputDelayInSeconds = 1f;
    [SerializeField]
    private GameObject playerInput;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(inputDelay());
    }

    private IEnumerator inputDelay()
    {
        yield return new WaitForSeconds(inputDelayInSeconds);
        playerInput.SetActive(true);
    }
    
}
