using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private LevelData levelData; 
    [SerializeField]
    private LevelData[] levelParts;
    [SerializeReference]
    private int currentLevel = -1;
    [SerializeField]
    private int lastTutorialLevel = 1;

    public bool CurrentSceneIsLevel()
    {
        return currentLevel >= 0;
    }

    public string GetLevelId()
    {
        return currentLevel.ToString();
    }

    public string GetLevelName()
    {
        return levelParts[currentLevel].Codename;
    }

    public LevelData SetNextLevel()
    {
        LevelData nextLevel = null;
        if (currentLevel < 0 && SettingsHandler.GetHasReadControls())
        {
            currentLevel = lastTutorialLevel;
        }
        currentLevel++;
        if (currentLevel > lastTutorialLevel)
        {
            SettingsHandler.SetHasReadControls(true);
        }
        if (currentLevel < levelParts.Length)
        {
            nextLevel = levelParts[currentLevel];
        }
        return nextLevel;
    }

    public void ResetLevelCounter()
    {
        currentLevel = -1;
    }
}
