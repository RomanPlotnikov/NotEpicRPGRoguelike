using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Player _player;
    private void OnEnable()
    {
        _player.Died.AddListener(OnPlayerDied);
        _player.EnteredGate.AddListener(OnPlayerEnteredGate);
    }

    private void OnDisable()
    {
        _player.Died.RemoveListener(OnPlayerDied);
        _player.EnteredGate.RemoveListener(OnPlayerEnteredGate);
    }

    private void OnPlayerDied()
    {
        SceneManager.LoadScene(1);
    }

    private void OnPlayerEnteredGate()
    {
        SceneManager.LoadScene(2);
    }
}
