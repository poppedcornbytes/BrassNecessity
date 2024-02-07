using UnityEngine;

public class LevelListing : MonoBehaviour
{
    [SerializeField]
    private LevelData[] gameLevels;
    [SerializeReference]
    private int currentLevel = -1;

    public bool CurrentSceneIsLevel()
    {
        return currentLevel >= 0;
    }

    public int GetLevelNumber()
    {
        return currentLevel + 1;
    }

    public string GetLevelName()
    {
        return gameLevels[currentLevel].Codename;
    }

    public LevelData SetNextLevel()
    {
        LevelData nextLevel = null;
        currentLevel++;
        if (currentLevel < gameLevels.Length)
        {
            nextLevel = gameLevels[currentLevel];
        }
        return nextLevel;
    }

    public void ResetLevelCounter()
    {
        currentLevel = -1;
    }
}
