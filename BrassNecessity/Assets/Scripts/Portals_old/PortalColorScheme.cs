using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PortalColorScheme
{
    [SerializeField]
    private Color primaryColor;
    public Color PrimaryColor { get => primaryColor; }
    [SerializeField]
    private Color secondaryColor;
    public Color SecondaryColor { get => secondaryColor; }
    [SerializeField]
    private Color accentColor;
    public Color AccentColor { get => accentColor; }
}
