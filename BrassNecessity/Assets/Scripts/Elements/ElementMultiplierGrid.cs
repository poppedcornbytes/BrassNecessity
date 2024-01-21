using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ElementMultiplierGrid
{
    /// <summary>
    /// Each row represents an ElementType and the damage multiplier
    /// for it attacking each type. Each row and column matches the index
    /// for the corresponding ElementType value. The row index attacks the column
    /// index. I.e, elementMultipliers[attackingIndex, defendingIndex]. 
    /// To add a new type, a new row and column must be added.
    /// </summary>
    private  static float STRONG_MULTIPLIER = 2f;
    private static float WEAK_MULTIPLIER = 0.5f;
    private static float NORMAL_MULTIPLIER = 1f;
    private static float[,] elementMultipliers = new float[,]
    {
      {NORMAL_MULTIPLIER, WEAK_MULTIPLIER, NORMAL_MULTIPLIER, STRONG_MULTIPLIER, NORMAL_MULTIPLIER},
      {WEAK_MULTIPLIER, STRONG_MULTIPLIER, NORMAL_MULTIPLIER, NORMAL_MULTIPLIER, STRONG_MULTIPLIER },
      {NORMAL_MULTIPLIER, STRONG_MULTIPLIER, STRONG_MULTIPLIER, WEAK_MULTIPLIER, NORMAL_MULTIPLIER },
      {NORMAL_MULTIPLIER, NORMAL_MULTIPLIER, NORMAL_MULTIPLIER, STRONG_MULTIPLIER, NORMAL_MULTIPLIER},
      {STRONG_MULTIPLIER, NORMAL_MULTIPLIER, STRONG_MULTIPLIER, NORMAL_MULTIPLIER, WEAK_MULTIPLIER}
    };

    public static float GetAttackMultiplier(Element.Type attackingType, Element.Type defendingType)
    {
        return elementMultipliers[(int)attackingType, (int)defendingType];
    }

    public static bool IsStrongMultiplier(float value)
    {
        return Mathf.Approximately(value, STRONG_MULTIPLIER) || Mathf.Approximately(value, STRONG_MULTIPLIER * STRONG_MULTIPLIER);
    }

    public static bool IsWeakMultiplier(float value)
    {
        return Mathf.Approximately(value, WEAK_MULTIPLIER) || Mathf.Approximately(value, WEAK_MULTIPLIER * WEAK_MULTIPLIER);
    }
}
