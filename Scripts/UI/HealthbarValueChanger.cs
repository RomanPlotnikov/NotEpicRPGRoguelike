using UnityEngine;
using UnityEngine.UI;

public class HealthbarValueChanger : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _healthbar;

    private void OnEnable()
    {
        _player.Damaged.AddListener(DisplayHealth);
    }

    private void OnDisable()
    {
        _player.Damaged.RemoveListener(DisplayHealth);
    }

    private void Start()
    {
        _player ??= GetComponentInParent<Player>();
    }

    private void DisplayHealth()
    {
        _healthbar.value = (_player.CurrentHealth) / (_player.MaxHealth);
    }
}
