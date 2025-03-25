using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewPortals
{
    [ExecuteAlways]
    public class PortalFader : MonoBehaviour
    {
        [SerializeField]
        private PortalComponents components;
        [SerializeField]
        private PortalStyler styler;
        [SerializeField]
        private PortalThemes themes;
        [SerializeField]
        private float fadeDurationInSeconds = 3f;
        private bool lastHiddenStatus;
        private bool lastDisabledStatus;


        private void Awake()
        {
            initiateStatus();
        }

        private void Start()
        {
            initiateStatus();
        }

        private void initiateStatus()
        {
            if (components.Portal == null)
            {
                components.Portal = GetComponent<PortalBehaviour>();
            }
            if (components.Portal.IsHidden)
            {
                QuickFadeOut();
                lastHiddenStatus = components.Portal.IsHidden;
            }
            else if (components.Portal.IsDisabled)
            {
                QuickGrayFade();
                lastHiddenStatus = components.Portal.IsDisabled;
            }
        }

        private void Update()
        {
            if (!Application.IsPlaying(this))
            {
                if (components.Portal.IsHidden && !lastHiddenStatus) //the only combination that should hide.
                {
                    QuickFadeOut();
                    lastHiddenStatus = components.Portal.IsHidden;
                }
                else if (components.Portal.IsDisabled && !lastDisabledStatus)
                {
                    QuickGrayFade();
                    lastDisabledStatus = components.Portal.IsDisabled;
                }
            }
        }

        public void QuickFadeOut()
        {

        }

        public void QuickGrayFade()
        {
            Color grayScheme = themes.GetDisabledColor();
        }

        public void FadeOut()
        {
            StartCoroutine(fadeOutRoutine());
        }

        private IEnumerator fadeOutRoutine()
        {
            yield return null;
        }

        public IEnumerator FadeIn()
        {
            yield return fadeInRoutein();
        }

        private IEnumerator fadeInRoutein()
        {
            yield return null;
        }

        public void FadeToGray()
        {
            Color colorToFade = themes.GetDisabledColor();
            StartCoroutine(fadeToGrayRoutine(colorToFade));
        }

        private IEnumerator fadeToGrayRoutine(Color colorToFade)
        {
            yield return null;
        }

        public IEnumerator FadeToScheme()
        {
            int currentColorIndex = styler.PortalThemeIndex;
            Color currentColor = themes.GetColorAtIndex(currentColorIndex);
            yield return fadeToSchemeRoutine(currentColor);
        }

        private IEnumerator fadeToSchemeRoutine(Color colorToReturn)
        {
            yield return null;
        }
    }
}
