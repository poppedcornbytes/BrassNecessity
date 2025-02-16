using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField]
    private VisualTreeAsset _overrideSceneDocument;
    public VisualTreeAsset OverrideSceneDocument { get => _overrideSceneDocument; }
    [SerializeField]
    private float _overrideDisplayTimeInSeconds = 2f;
    public float OverrideDisplayTimeInSeconds { get => _overrideDisplayTimeInSeconds; }

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
        managerToPopulate._overrideSceneDocument = this.OverrideSceneDocument;
    }
}
