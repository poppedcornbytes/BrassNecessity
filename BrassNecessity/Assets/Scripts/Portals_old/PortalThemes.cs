using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalThemes : MonoBehaviour
{
    [SerializeField]
    private PortalColorScheme[] colorSchemes;

    [SerializeField]
    private PortalColorScheme disabledScheme;
    public int Count 
    {
        get => colorSchemes.Length;
    }
   
    
    public PortalColorScheme GetColorSchemeAtIndex(int index)
    {
        return colorSchemes[index];
    }

    public PortalColorScheme GetDisabledScheme()
    {
        return disabledScheme;
    }
}
