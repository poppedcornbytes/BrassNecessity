using UnityEngine;

public class PortalThemes : MonoBehaviour
{
    [SerializeField]
    private Color[] availableColors;

    [SerializeField]
    private Color disabledColor;
    public int Count
    {
        get => availableColors.Length;
    }


    public Color GetColorAtIndex(int index)
    {
        return availableColors[index];
    }

    public Color GetDisabledColor()
    {
        return disabledColor;
    }
}
