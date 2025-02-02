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
    private int currentPart = 0;

    public bool CurrentSceneIsLevel()
    {
        return currentLevel >= 0;
    }

    public string GetLevelId()
    {
        return levelData.Codename;
    }

    public string GetLevelName()
    {
        return levelParts[currentPart].Codename;
    }

    public LevelData SetNextLevel()
    {
        LevelData nextLevel = null;
        currentPart++;
        if (currentPart < levelParts.Length)
        {
            nextLevel = levelParts[currentPart];
        }
        return nextLevel;
    }

    public void ResetLevelCounter()
    {
        currentLevel = -1;
    }

    public void CreateCopy(LevelManager managerToPopulate)
    {
        managerToPopulate.levelData = this.levelData;
        managerToPopulate.levelParts = this.levelParts;
        managerToPopulate.currentLevel = this.currentLevel;
        managerToPopulate.currentPart = this.currentPart;
    }
}
