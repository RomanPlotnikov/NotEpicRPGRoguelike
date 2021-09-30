using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator ??= GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(0.01f);
        }
    }

    public void Hit()
    {
        _animator.SetTrigger("onAttack");
    }
}
