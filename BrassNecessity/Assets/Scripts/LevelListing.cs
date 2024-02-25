using UnityEngine;

public class LevelListing : MonoBehaviour
{
    [SerializeField]
    private LevelData[] gameLevels;
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
        int rawLevelNumber = currentLevel;
        string levelId;
        if (rawLevelNumber <= lastTutorialLevel)
        {
            levelId = convertTutorialIdToLetter(rawLevelNumber);
        }
        else
        {
            levelId = (rawLevelNumber - lastTutorialLevel).ToString();

        }
        return levelId;
    }

    private string convertTutorialIdToLetter(int rawLevelNumber)
    {
        return ((char)('A' + rawLevelNumber)).ToString();
    }

    public string GetLevelName()
    {
        return gameLevels[currentLevel].Codename;
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
