using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _health;
    [SerializeField] private float _attackCooldown;

    [HideInInspector] public UnityEvent Died;
    private float _lastHitTime = 0f;
    public float Health => _health;
    public float Speed => _speed;

    private void Start()
    {
        _animator ??= GetComponent<Animator>();
    }

    private void Update()
    {
        _lastHitTime += Time.deltaTime;
    }

    public void Attack(Player target)
    {
        if (_lastHitTime > _attackCooldown)
        {
            target.TakeDamage(_damage);
            _lastHitTime = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger("onDamaged");
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        _animator.SetTrigger("onDied");
        Died.Invoke();
        Destroy(this.gameObject);
    }
}
