using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("LastLevel", 1);  
    }

    public void LoadLastLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel"));
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
