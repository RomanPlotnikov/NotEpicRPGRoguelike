using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class PlayerPursuer : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private Rigidbody2D _rigidboody;
    [SerializeField] private float _triggerRadius;
    [SerializeField] private Enemy _pursuer;

    private void OnEnable()
    {
        _target.Died.AddListener(OnPlayerDied);
    }
    private void OnDisable()
    {
        _target.Died.RemoveListener(OnPlayerDied);
    }

    private void Start()
    {
        _pursuer = GetComponent<Enemy>();

        if (_target == null)
        {
            Debug.Log("There isn't target for PlayerPursuer.cs");
            _target ??= FindObjectOfType<Player>();
        }
    }

    private void Update()
    {
        Vector3 targetDistance = _target.transform.position - transform.position;
        if ((targetDistance).magnitude < _triggerRadius)
            StartCoroutine(FollowTheTrigger(targetDistance));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(_target.gameObject))
        {
            _pursuer.Attack(_target);
        }
    }

    private IEnumerator FollowTheTrigger(Vector3 offset = new Vector3())
    {
        Vector3 rightView = new Vector3(1, 1, 1);
        Vector3 leftView = new Vector3(-1, 1, 1);
        _pursuer.gameObject.transform.localScale = (offset.x > 0) ? rightView : leftView;

        _rigidboody.MovePosition(transform.position + (offset.normalized * _pursuer.Speed * Time.deltaTime));

        yield return null;
    }

    private void OnPlayerDied()
    {
        StopCoroutine(FollowTheTrigger());
    }

}
