using System;

/// <summary>
/// To expand list, ElementMultiplierGrid must also be expanded.
/// </summary>

public static class Element
{
    public enum Type
    {
        Normal,
        Carbon,
        Electric,
        Gravity,
        Nuclear
    }

    public static Type GenerateRandomType()
    {
        int numberOfElements = Enum.GetNames(typeof(Type)).Length;
        int diceRoll = UnityEngine.Random.Range(0, numberOfElements);
        return (Type)diceRoll;
    }

    public static int TypeToInt(Type type)
    {
        int elementValue = (int)type;
        int numberOfElements = Enum.GetNames(typeof(Type)).Length;
        if (elementValue >= numberOfElements)
        {
            elementValue = 0;
        }
        return elementValue;
    }
}
