using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NewPortals
{
    [ExecuteAlways]
    public class PortalStyler : MonoBehaviour
    {
        [SerializeField]
        private int portalThemeIndex;
        public int PortalThemeIndex { get => portalThemeIndex; }
        private int lastThemeIndex;
        [SerializeField]
        private PortalThemes themeList;
        [SerializeField]
        private PortalComponents components;

        private void OnEnable()
        {
            if (themeList == null)
            {
                themeList = FindObjectOfType<PortalThemes>();
            }
            setColors();
        }

        // Update is called once per frame
        void Update()
        {
            if (portalThemeIndex != lastThemeIndex)
            {
                setColors();
                lastThemeIndex = portalThemeIndex;
            }
        }

        public void SetThemeIndex(int themeIndex)
        {
            portalThemeIndex = themeIndex;
        }

        public void SetSpecificTheme(Color colorToApply)
        {
            components.BasePortal.EffectColor = colorToApply;
            components.LeaveEffect.EffectColor = colorToApply;
            components.ArriveEffect.EffectColor = colorToApply;
        }

        private void setColors()
        {
            if (portalThemeIndex < 0 || portalThemeIndex > themeList.Count)
            {
                portalThemeIndex = 0;
            }
            Color scheme = themeList.GetColorAtIndex(portalThemeIndex);
            SetSpecificTheme(scheme);
            lastThemeIndex = portalThemeIndex;
        }

        private void OnDrawGizmosSelected()
        {
            if (components.BasePortal != null)
            {
                components.BasePortal.Play();
            }
        }

        private void OnDrawGizmos()
        {
            if (!Selection.gameObjects.Any(x => x.transform.IsChildOf(this.transform)))
            {
                if (components.BasePortal != null)
                {
                    components.BasePortal.Stop();
                }
            }
            else
            {
                if (components.BasePortal != null)
                {
                    components.BasePortal.Play();
                }
            }
        }
    }
}
