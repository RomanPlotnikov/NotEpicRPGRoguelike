using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;

    private float _lastHitTime = 0f;

    [HideInInspector] public UnityEvent Died;
    public float Health => _health;
    
    private void Start()
    {
        _sprite ??= GetComponent<SpriteRenderer>();
        _rigidbody ??= GetComponent<Rigidbody2D>();
        _animator ??= GetComponent<Animator>();
    }

    private void Update()
    {
        _lastHitTime += Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        _sprite.flipX = (direction.x > 0) ? true : false;
        _animator.SetTrigger("onMove");
        _rigidbody.MovePosition(transform.position + (direction * _speed * Time.deltaTime));
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
