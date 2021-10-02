using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class PlayerPursuer : MonoBehaviour
{
    [SerializeField] private Enemy _pursuer;
    [SerializeField] private Player _target;

    [SerializeField] private float _triggerRadius;
    [SerializeField] private float _pursuitRadius;

    private void Awake()
    {
        _pursuer = GetComponent<Enemy>();

        if (_target == null)
        {
            Debug.Log("There isn't target for PlayerPursuer.cs");
            _target ??= FindObjectOfType<Player>();
        }
    }

    private void OnEnable()
    {
        _target.Died.AddListener(OnPlayerDied);
    }

    private void OnDisable()
    {
        _target.Died.RemoveListener(OnPlayerDied);
    }

    private void Update()
    {
        Vector3 targetDistance = _target.transform.position - transform.position;

        if (targetDistance.magnitude < _triggerRadius)
            StartCoroutine(FollowTheTrigger(targetDistance));
        else if (targetDistance.magnitude < _pursuitRadius)
            StopCoroutine(FollowTheTrigger());
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(_target.gameObject))
        {
            _pursuer.TryAttack(_target);
        }
    }

    private IEnumerator FollowTheTrigger(Vector3 offset = new Vector3())
    {
        _pursuer.MoveTo(offset.normalized);
        yield return null;
    }

    private void OnPlayerDied()
    {
        StopCoroutine(FollowTheTrigger());
    }

}
