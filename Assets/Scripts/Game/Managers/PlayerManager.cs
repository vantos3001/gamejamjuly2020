using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerManager
{
    private static PlayerData _playerData;
    public static PlayerData PlayerData => _playerData;

    private static GameObject _player;
    public static Health Health => _player.GetComponent<Health>();
    
    public static void Init()
    {
        CreatePlayer();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void CreatePlayer()
    {
        _playerData = new PlayerData();
        
        var clickTimeAbility = new Ability(AbilityType.RestClickTime);
        var maxHealthAbility = new Ability(AbilityType.MaxHealth);
        
        _playerData.Abilities.Add(clickTimeAbility);
        _playerData.Abilities.Add(maxHealthAbility);
    }

    public static float GetMaxHealth()
    {
        return _playerData.Abilities.Find(ab => ab.AbilityType == AbilityType.MaxHealth).Data.Value;

    }

    public static float GetRestClickTime()
    {
        return _playerData.Abilities.Find(ab => ab.AbilityType == AbilityType.RestClickTime).Data.Value;
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
            var health = Health;
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
        _playerData.CurrentCoins++;

        var isoRend = _player.GetComponentInChildren<IsometricCharacterRenderer>();
        isoRend.SetDeath();
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
