using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NewPortals
{
    [ExecuteAlways]
#if UNITY_EDITOR
    [SelectionBase]
#endif
    public class PortalStyler : MonoBehaviour
    {
        [Range(1, 10)]
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
    }
}
