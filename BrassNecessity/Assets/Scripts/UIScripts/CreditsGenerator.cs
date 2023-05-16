using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditsGenerator : MonoBehaviour
{
    [SerializeField]
    private string creditsFilepath;
    [SerializeField]
    private GameObject continueMessage;
    private StreamReader creditsFile;

    private VisualElement creditsElement;

    void OnEnable()
    {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        creditsElement = rootVisualElement.Q<VisualElement>("CreditElement");
        creditsElement.Clear();
        creditsFile = new StreamReader(creditsFilepath);
        StartCoroutine(creditsCrawlRoutine());
    }

    private IEnumerator creditsCrawlRoutine()
    {
        while (!creditsFile.EndOfStream)
        {
            string creditInfo = creditsFile.ReadLine();
            CreditLine lineToCrawl = new CreditLine(creditInfo, creditsElement);
            StartCoroutine(creditLineDestroy(lineToCrawl));
            yield return new WaitForSeconds(0.2f);
        }
        creditsFile.Close();
    }

    private IEnumerator creditLineDestroy(CreditLine lineToDestroy)
    {
        yield return new WaitForSeconds(7.5f);
        lineToDestroy.DestroyLabel();
        if (creditsElement.childCount == 0)
        {
            continueMessage.SetActive(true);
        }
    }
}
