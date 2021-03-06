﻿using UnityEngine;
using UnityEngine.SceneManagement;

public static class UIManager
{
    private static MenuCanvas _menuCanvas;
    
    private static MenuCanvas MenuCanvas
    {
        get
        {
            if (_menuCanvas == null)
            {
                _menuCanvas = GameObject.FindWithTag("MenuCanvas").GetComponent<MenuCanvas>();
            }

            return _menuCanvas;
        }
    }

    private static GameCanvas _gameCanvas;

    public static GameCanvas GameCanvas
    {
        get
        {
            if (_gameCanvas == null)
            {
                _gameCanvas = GameObject.FindWithTag("GameCanvas").GetComponent<GameCanvas>();
            }

            return _gameCanvas;
        }
    }

    public static void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        EventManager.OnAbilityLevelUp += OnAbilityLevelUp;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == "Menu")
        {
            UpdateMenu();
        }
        
        if (scene.name == "Gameplay")
        {
            UpdateGameplay();
        }
    }

    private static void OnAbilityLevelUp(AbilityType abilityType)
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            UpdateMenu();
        }
    }

    private static void UpdateMenu()
    {
        var playerData = PlayerManager.PlayerData;
        UpdateCoinPanel(playerData.CurrentCoins);

        foreach (var ability in playerData.Abilities)
        {
            UpdateAbilityPanel(ability);
        }
    }

    private static void UpdateCoinPanel(int coins)
    {
        MenuCanvas.SetCoinPanel(coins.ToString());
    }

    private static void UpdateAbilityPanel(Ability ability)
    {
        MenuCanvas.SetAbilityPanel(ability);
    }

    private static void UpdateGameplay()
    {
        
    }
}
