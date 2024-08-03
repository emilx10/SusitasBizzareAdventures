using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCarMovement : PlayerMovementHandler
{
    [SerializeField] private float _checkpointSize;
    [SerializeField] private List<Transform> _checkPoints;
    private int _currentCheckpoint;
    [SerializeField] private int _laps;
    private bool _stop;

    [SerializeField] private float _forwardAI, _sidewaysAI;

    private Vector2 _posLastCheck;
    
    [SerializeField] private UnityEvent _onFinishRace;


    protected new void Start() 
    { 
        base.Start();
        InvokeRepeating(nameof(CheckIfStuck), 3, 2);
        _posLastCheck=transform.position;
    }

    protected override void GetInput()
    {
        if (_stop) return;

        Vector2 direction = _checkPoints[_currentCheckpoint].position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x);
        //print(AngleDifference(transform.eulerAngles.z+90,angle * Mathf.Rad2Deg)); // print angle difference
        float angleDiff = AngleDifference(transform.eulerAngles.z + 90, angle * Mathf.Rad2Deg);

        if (Mathf.Abs(angleDiff) < 1)
        {
            _vertical = 1;
            _horizontal = 0;
        }
        else
        {
            _vertical = 1 - Mathf.Abs(angleDiff) / _forwardAI;
            _horizontal = angleDiff / _sidewaysAI;
        }

        if (Vector2.Distance(transform.position, _checkPoints[_currentCheckpoint].position) <= _checkpointSize)
        {
            if (_currentCheckpoint == _checkPoints.Count - 1)
            {
                if (_laps > 0)
                {
                    _laps--;
                    _currentCheckpoint = 0;
                }
                else
                {
                    _onFinishRace?.Invoke();
                    StopTheCar();
                    CancelInvoke(nameof(CheckIfStuck));
                    Debug.Log("done");
                }
            }
            else
            {
                _currentCheckpoint++;
            }
        }
    }

    private void CheckIfStuck()
    {
        if (Vector2.Distance(_posLastCheck, transform.position) < 0.2f)
        {
            TryReverse();
        }
        else 
        { 
            _posLastCheck = transform.position;
        }
    }

    private void TryReverse()
    {
        StopTheCar();
        CancelInvoke(nameof(CheckIfStuck));

        _vertical = -1;
        int r = Random.Range(0, 3);
        switch (r) 
        {
            case 0: 
                _horizontal = 0; 
                break;
            case 1:
                _horizontal = 1;
                break;
            case 2: 
                _horizontal = -1; 
                break;
        }


        Invoke(nameof(TryDriveAgain),Random.Range(2f,7f));
    }

    private void TryDriveAgain()
    {
        _currentSpeed = 0;
        InvokeRepeating(nameof(CheckIfStuck), 3, 2);
        _stop = false;
    }

    public void StopTheCar()
    {
        _stop = true;
        _currentSpeed = 0;
        _vertical = 0;
        _horizontal = 0;
    }

    private float AngleDifference(float angle1, float angle2)
    {
        float diff = (angle2 - angle1 + 180) % 360 - 180;
        return diff < -180 ? diff + 360 : diff;
    }
}
