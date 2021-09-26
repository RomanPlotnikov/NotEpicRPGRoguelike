using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerStatsHandler))]
public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private PlayerStatsHandler _statsHandler;
    private Weapon _currentWeapon;

    [HideInInspector] public UnityEvent Died;
    [HideInInspector] public UnityEvent Damaged;
    public PlayerStatsHandler Stats => _statsHandler;

    private void Start()
    {
        _sprite ??= GetComponent<SpriteRenderer>();
        _statsHandler ??= GetComponent<PlayerStatsHandler>();
        _currentWeapon = GetComponentInChildren<Weapon>();
        if (_currentWeapon == null)
            Debug.Log("There isn't first weapon for Player.cs. Instal weapon prefab as child");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Attack();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _sprite.sortingOrder = (collision.transform.position.y > transform.position.y) ? 1 : -1;

        if (collision.gameObject.TryGetComponent(out Weapon weapon) && Input.GetMouseButtonDown(0))
            SetWeapon(weapon);
    }

    public void Attack()
    {
        _currentWeapon?.Hit();
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
        _currentWeapon.gameObject.SetActive(false);

        _currentWeapon = weapon;
        _currentWeapon.transform.parent = gameObject.transform;
        _currentWeapon.transform.localPosition = new Vector2(0, 0.4f);
        _currentWeapon.enabled = true;
        _currentWeapon.gameObject.SetActive(true);
    }

}