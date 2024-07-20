using UnityEngine;
using UnityEngine.Events;

public class Tripwire : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    private GameObject _player;
    private void Start()
    {
        _player = GameManager.Instance.GetPlayerHealth().gameObject;
        PlayerPrefs.SetString("Level", "Level2");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            _event.Invoke();
            Destroy(gameObject);
        }
    }
}
