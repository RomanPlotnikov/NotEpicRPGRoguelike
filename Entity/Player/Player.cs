using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerStatsHandler))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStatsHandler _statsHandler;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Hand _hand;
    [SerializeField] private float _attackCooldown;

    private float _lastHitTime = 0f;
    private Weapon _currentWeapon;
    public PlayerStatsHandler Stats => _statsHandler;
    public bool InteractionButtonIsClicked { get; set; }

    [HideInInspector] public UnityEvent Damaged;
    [HideInInspector] public UnityEvent Died;

    private void Start()
    {
        _sprite ??= GetComponent<SpriteRenderer>();
        _statsHandler ??= GetComponent<PlayerStatsHandler>();
        _hand ??= GetComponentInChildren<Hand>();
    }

    private void Update()
    {
        if (InteractionButtonIsClicked)
            StartCoroutine(DisableClick());

        _lastHitTime += Time.deltaTime;

        if (InteractionButtonIsClicked && _lastHitTime > _attackCooldown)
            Attack();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _sprite.sortingOrder = (collision.transform.position.y > transform.position.y) ? 1 : -1;

        if (InteractionButtonIsClicked && collision.gameObject.TryGetComponent(out Weapon weapon))
            SetWeapon(weapon);
    }

    private IEnumerator DisableClick()
    {
        yield return new WaitForSeconds(0.2f);
        InteractionButtonIsClicked = false;
        StopCoroutine(DisableClick());
    }

    public void Attack()
    {
        _lastHitTime = 0f;

        if (_currentWeapon == null)
            _hand.Hit();
        else
            _currentWeapon.Hit();
    }

    public void TakeDamage(float damage)
    {
        _statsHandler.SubtractHealth(damage);
        Damaged.Invoke();

        if (_statsHandler.CurrentHealth <= 0)
            Died.Invoke();
    }

    public void SetWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        weapon.Set(_hand);
    }
}
