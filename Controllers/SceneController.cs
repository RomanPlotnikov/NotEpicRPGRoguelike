using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    private void OnEnable()
    {
        _player.Died.AddListener(OnPlayerDied);
    }

    private void OnDisable()
    {
        _player.Died.RemoveListener(OnPlayerDied);
    }

    private void OnPlayerDied()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel"));
    }
}
