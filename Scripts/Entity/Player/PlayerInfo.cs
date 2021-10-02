using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] protected InteractionButton InteractionButton;
    [SerializeField] protected SpriteRenderer Sprite;
    [SerializeField] protected Rigidbody2D Rigidbody;
    [SerializeField] protected Joystick Joystick;
    [SerializeField] protected Hand Hand;

    [SerializeField] protected float _attackCooldown = 0.5f;
    
    [SerializeField] protected float _maxHealth = 5f;
    [SerializeField] protected float _agility = 2f;
    [SerializeField] protected float _power = 2f;

    protected float _lastHitTime = 0f;
    protected float _currentHealth;
    protected Weapon CurrentWeapon;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;
    public float Power => _power;

    private void Start()
    {
        _currentHealth = _maxHealth;

        Sprite ??= GetComponent<SpriteRenderer>();
        Rigidbody ??= GetComponent<Rigidbody2D>();
        Joystick ??= GetComponent<Joystick>();
        Hand ??= GetComponentInChildren<Hand>();

        if (Joystick == null)
        {
            Debug.Log($"There isn't joystick for PlayerInfo.cs");
            Joystick = FindObjectOfType<Joystick>();
        }  
        else if (InteractionButton == null)
        {
            Debug.Log($"There isn't interaction button for PlayerInfo.cs");
            InteractionButton = FindObjectOfType<InteractionButton>();
        }
    }
} 