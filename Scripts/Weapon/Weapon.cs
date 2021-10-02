using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _sharpness;

    private bool _isSetted;

    private void Start()
    {
        _animator ??= GetComponent<Animator>();
        if (_player == null)
        {
            Debug.Log($"There isn't player for Weapon.cs");
            _player = FindObjectOfType<Player>();
        }
    }

    private void OnEnable()
    {
        _isSetted = (transform.parent == _player) ? true : false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy) && _isSetted)
        {
            enemy.TakeDamage(_sharpness * _player.Power);
            Debug.Log(enemy.Health);
        }
    }

    public void Hit()
    {
        _animator.SetTrigger("onAttack");
    }

    public void SetTo(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;

        _animator.enabled = true;
        _isSetted = true;
    }
}
