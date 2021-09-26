using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private Joystick _joystick;

    private void OnEnable()
    {
        _player.Died.AddListener(OnPlayerDied);
        _player.Damaged.AddListener(OnPlayerDamaged);
    }
    private void OnDisable()
    {
        _player.Died.RemoveListener(OnPlayerDied);
        _player.Damaged.RemoveListener(OnPlayerDamaged);
    }

    private void Start()
    {
        _player ??= GetComponent<Player>();
        _rigidbody ??= GetComponent<Rigidbody2D>();
        _animator ??= GetComponent<Animator>();

        if (_joystick == null)
        {
            Debug.Log($"There isn't joystick for PlayerController.cs");
            _joystick = FindObjectOfType<Joystick>();
        }
    }

    private void FixedUpdate()
    {
        if (_joystick.Direction.magnitude > 0.05f)
            MoveTo(_joystick.Direction);
    }

    private void MoveTo(Vector2 direction)
    {
        Vector3 rightView = new Vector3(1, 1, 1);
        Vector3 leftView = new Vector3(-1, 1, 1);
        transform.localScale = (direction.x > 0) ? rightView : leftView;

        _animator.SetTrigger("onMove"); 
        _rigidbody.MovePosition((Vector2)transform.position + (direction * _player.Stats.Agility * Time.deltaTime));
    }

    private void OnPlayerDied()
    {
        StartCoroutine(DisablePlayer());
    }
    
    private IEnumerator DisablePlayer()
    {
        _animator.SetTrigger("onDied");

        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        Time.timeScale = 0;// убрать
        StopCoroutine(DisablePlayer());
    }

    private void OnPlayerDamaged()
    {
        _animator.SetTrigger("onDamaged");
    }
}