using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _sharpness;

    private void Awake()
    {
        _animator.enabled = false;
        this.enabled = false;
    }

    private void OnEnable()
    {
        _animator.enabled = true;
    }

    private void Start()
    {
        _player = GetComponentInParent<Player>();
        _animator ??= this.gameObject.GetComponent<Animator>();
    }

    public void Hit()
    {
        _animator.SetTrigger("onAttack"); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_sharpness * _player.Stats.Power);
            Debug.Log(enemy.Health);
        }
    }

}
