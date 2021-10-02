using UnityEngine;
using UnityEngine.Events;

public class Player : PlayerInfo
{
    [HideInInspector] public UnityEvent EnteredGate;
    [HideInInspector] public UnityEvent Damaged;
    [HideInInspector] public UnityEvent Died;

    private void Update()
    {
        _lastHitTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (InteractionButton.IsCliked && (_lastHitTime > _attackCooldown))
            Attack();
        if (Joystick.Direction.magnitude > 0.05f)
            MoveTo(Joystick.Direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (InteractionButton.IsCliked && collision.TryGetComponent(out Weapon weapon))
            SetWeapon(weapon);
        if (collision.TryGetComponent(out Gate gate))
            EnteredGate.Invoke();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Sprite.sortingOrder = (collision.transform.position.y > transform.position.y) ? 1 : -1;
    }

    private void MoveTo(Vector2 direction)
    {
        Rigidbody.MovePosition((Vector2)transform.position + (direction * _agility * Time.deltaTime));

        transform.localScale = (direction.x > 0) ? Vector3.one : new Vector3(-1, 1, 1);
    }

    public void Attack()
    {
        if (CurrentWeapon == null)
            Hand.Hit();
        else
            CurrentWeapon.Hit();

        _lastHitTime = 0f;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Damaged.Invoke();

        if (CurrentHealth <= 0)
            Died.Invoke();
    }

    public void SetWeapon(Weapon weapon)
    {
        CurrentWeapon?.gameObject.SetActive(false);

        CurrentWeapon = weapon;
        weapon.SetTo(Hand.transform);
    }
}
