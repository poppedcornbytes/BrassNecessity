using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelData
{
    [SerializeField]
    private string codename;
    public string Codename
    {
        get => codename;
    }
    [SerializeField]
    private string sceneName;
    public string SceneName
    {
        get => sceneName;
    }
}
