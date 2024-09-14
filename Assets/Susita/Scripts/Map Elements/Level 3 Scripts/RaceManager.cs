using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    private int _timer;

    [SerializeField] Text _countDownText;

    [SerializeField] PlayerMovementHandler _playerMovementHandler;

    [SerializeField] EnemyCarMovement _enemyMovementHandler;


    private void Awake()
    {
        _playerMovementHandler.enabled = false;
        _enemyMovementHandler.enabled = false;
    }

    private void Start()
    {
        StartTimer(3);
    }

    public void StartTimer(int timer)
    {
        _timer = 3;
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (_timer >= 0)
        {
            _countDownText.text = _timer.ToString();
            yield return new WaitForSeconds(1);
            _timer--;
        }
        _countDownText.enabled = false;
        _playerMovementHandler.enabled = true;
        _enemyMovementHandler.enabled = true;
    }
}
