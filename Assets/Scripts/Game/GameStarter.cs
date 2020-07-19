using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{

    [SerializeField] private GameObject Logo;
    [SerializeField] private GameObject Gopnik;
    public void Awake()
    {
        Init();
    }

    private void Init()
    {
        PlayerManager.Init();
        UIManager.Init();
        
        Invoke("ShowGopnik", 1.5f);
    }

    private void ShowGopnik()
    {
        Logo.SetActive(false);
        Gopnik.SetActive(true);
        
        Invoke("LoadMenu", 0.05f);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
