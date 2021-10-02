using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Player _player;
    [SerializeField] private float _boundX = 0.15f;
    [SerializeField] private float _boundY = 0.05f;

    private void Start()
    {
        if (_target == null || _player == null)
        {
            Debug.Log("There isn't target for CameraController.cs");
            _player = FindObjectOfType<Player>();
            _target = _player.transform;
        }
    }

    private void OnEnable()
    {
        _player.Died.AddListener(OnPlayerDied); 
    }

    private void OnDisable()
    {
        _player.Died.RemoveListener(OnPlayerDied);
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = _target.position.x - transform.position.x;
        if (deltaX > _boundX || deltaX < -_boundX)
            delta.x = (transform.position.x < _target.position.x) ? (deltaX - _boundX) : (deltaX + _boundX);
        
        float deltaY = _target.position.y - transform.position.y;
        if (deltaY > _boundY || deltaY < -_boundY)
            delta.y = (transform.position.y < _target.position.y) ? (deltaY - _boundY) : (deltaY + _boundY);

        transform.position += new Vector3(delta.x, delta.y, 0);
    }

    private void OnPlayerDied()
    {
        this.enabled = false;
    }
}