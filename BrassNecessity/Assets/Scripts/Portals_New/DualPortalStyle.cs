using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewPortals
{
    [ExecuteAlways]
    public class DualPortalStyle : MonoBehaviour
    {
        [SerializeField]
        private PortalStyler portalA;

        [SerializeField]
        private PortalStyler portalB;

        [Range(0,10)]
        [SerializeField]
        private int themeIndex;
        private int lastThemeIndex;

        void Awake()
        {
            setTheme();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (themeIndex != lastThemeIndex)
            {
                setTheme();
            }
            if (portalA.PortalThemeIndex != themeIndex)
            {
                int newIndex = portalA.PortalThemeIndex;
                portalB.SetThemeIndex(newIndex);
                themeIndex = newIndex;
                lastThemeIndex = newIndex;
            }
            if (portalB.PortalThemeIndex != themeIndex)
            {
                int newIndex = portalB.PortalThemeIndex;
                portalA.SetThemeIndex(newIndex);
                themeIndex = newIndex;
                lastThemeIndex = newIndex;
            }
        }

        private void setTheme()
        {
            portalA.SetThemeIndex(themeIndex);
            portalB.SetThemeIndex(themeIndex);
            lastThemeIndex = themeIndex;
        }
    }
}
