using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowMovement : MonoBehaviour
{
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();

    private Animator _animator => GetComponent<Animator>(); 

    private GameManager _gameManager;

    private PlayerHealth _playerHealth;

    [SerializeField] private float _speed = 5f;

    private bool _isMoving = false;

    private bool _chase = false;

    [SerializeField] private float _range = 3f;

    [SerializeField] float _pauseDuration;


    void Start()
    {
        _gameManager = GameManager.Instance;
        _playerHealth = _gameManager.GetPlayerHealth();
    }


    private void Update()
    {
        if (!_chase)
        {
            if (Vector2.Distance(transform.position,_playerHealth.transform.position) <= _range)
            {
                _chase = true;
                _isMoving = true;
            }
        }
    }

    void FixedUpdate()
    {
        if(_isMoving)
          FollowPlayer();

        _animator.SetBool("Moving",_isMoving);
    }

    private void FollowPlayer()
    {
        Vector2 direction = CalculateDirection();
        _rb.velocity = _speed * Time.fixedDeltaTime * direction;
        transform.rotation = Quaternion.FromToRotation(Vector2.up , direction);
    }

    private Vector2 CalculateDirection()
    {
        Vector2 direction = (_playerHealth.transform.position - transform.position).normalized;
        return direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator MovementPause()
    {
        yield return new WaitForSeconds(_pauseDuration);
        _isMoving = true;
    }

    public void OnImpact()
    {
        _isMoving = false;
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0f;
        StartCoroutine(MovementPause());
    }
}
