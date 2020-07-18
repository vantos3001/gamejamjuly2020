using UnityEngine;
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

    public static void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == "Menu")
        {
            UpdateMenu();
        }
    }

    private static void UpdateMenu()
    {
        var playerData = PlayerManager.PlayerData;
        UpdateCoinPanel(playerData.CurrentCoin);

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
}
