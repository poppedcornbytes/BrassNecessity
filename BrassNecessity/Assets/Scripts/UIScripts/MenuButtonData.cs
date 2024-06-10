using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MenuButtonData : UIElementData
{

    [SerializeField]
    private MenuButtonType type;
    public MenuButtonType Type
    {
        get => type;
    }

    [SerializeField]
    private SceneKey sceneDestination;
    public SceneKey SceneDestination
    {
        get => sceneDestination;
        set => sceneDestination = value;
    }

    [SerializeField]
    private GameObject activatedMenu;
    public GameObject ActivatedMenu
    {
        get => activatedMenu;
    }

    [SerializeField]
    private GameObject hidMenu;
    public GameObject HidMenu
    {
        get => hidMenu;
    }

    [SerializeField]
    private GameObject deactivatedMenu;
    public GameObject DeactivatedMenu
    {
        get => deactivatedMenu;
    }

    [SerializeField]
    private GameObject unhidMenu;
    public GameObject UnhidMenu
    {
        get => unhidMenu;
    }

}
