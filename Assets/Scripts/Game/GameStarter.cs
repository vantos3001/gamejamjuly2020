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
        UIManager.Init();
        
        Invoke("LoadMenu", 1.5f);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
