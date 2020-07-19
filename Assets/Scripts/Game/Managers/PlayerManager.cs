using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerManager
{
    private static PlayerData _playerData;
    public static PlayerData PlayerData => _playerData;

    private static GameObject _player;
    
    public static void Init()
    {
        CreatePlayer();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void CreatePlayer()
    {
        _playerData = new PlayerData();
        
        var clickTimeAbility = new Ability(AbilityType.ClickTime);
        var maxHealthAbility = new Ability(AbilityType.MaxHealth);
        
        _playerData.Abilities.Add(clickTimeAbility);
        _playerData.Abilities.Add(maxHealthAbility);
    }

    public static float GetMaxHealth()
    {
        return _playerData.Abilities.Find(ab => ab.AbilityType == AbilityType.MaxHealth).Data.Value;

    }
    
    private static void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == "Gameplay")
        {
            StartGame();
        }
    }

    private static void StartGame()
    {
        _player = GameObject.FindWithTag("Player");

        if (_player != null)
        {
            var health = _player.GetComponent<Health>();
            health.HealthEnded += EndGame;

            health.Init(GetMaxHealth());
        }
        else
        {
            Debug.LogError("Player is not found");
        }
    }

    private static void EndGame()
    {
        var health = _player.GetComponent<Health>();
        health.HealthEnded -= EndGame;

        SceneManager.LoadScene("Menu");
    }
}
