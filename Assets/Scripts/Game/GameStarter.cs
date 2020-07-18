using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public void Awake()
    {
        Init();
    }

    private void Init()
    {
        PlayerManager.Init();
        
        LoadMenu();
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
