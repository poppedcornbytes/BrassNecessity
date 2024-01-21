using UnityEngine;

public class LevelListing : MonoBehaviour
{
    [SerializeField]
    private LevelData[] gameLevels;
    [SerializeReference]
    private int currentLevel;

    private void Awake()
    {
        //currentLevel = 0;
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
}
