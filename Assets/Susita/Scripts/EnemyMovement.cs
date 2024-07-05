using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowMovement : MonoBehaviour
{
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();

    private GameManager _gameManager;

    [SerializeField] private GameObject _playerObject;

    [SerializeField] private float _speed = 5f;

    private bool _isMoving = true;

    [SerializeField] float _pauseDuration;

    void Start()
    {
        _gameManager = GameManager.Instance;
        _playerObject = _gameManager.GetPlayer();
    }


    void FixedUpdate()
    {
        if(_isMoving)
          FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector2 direction = CalculateDirection();
        _rb.velocity = _speed * Time.fixedDeltaTime * direction;
        transform.rotation = Quaternion.FromToRotation(Vector2.up , direction);
    }

    private Vector2 CalculateDirection()
    {
        Vector2 direction = (_playerObject.transform.position - transform.position).normalized;
        return direction;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isMoving = false;
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            StartCoroutine(MovementPause());
        }
    }
    private IEnumerator MovementPause()
    {
        yield return new WaitForSeconds(_pauseDuration);
        _isMoving = true;
    }

}
