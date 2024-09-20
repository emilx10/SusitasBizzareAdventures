using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExitHandler : MonoBehaviour
{
    [SerializeField] private KeyCode holdKey;
    [SerializeField] private float holdDuration;
    [SerializeField] private KeyCode exitKey;

    private bool _canExit = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(holdKey))
        {
            StartCoroutine(StartCountdown());
        }
        if (Input.GetKeyUp(holdKey))
        {
            StopAllCoroutines();
            _canExit = false;
        }
        if (_canExit && Input.GetKeyDown(exitKey))
        {
            Debug.Log("closing game");
            Application.Quit();
        }
    }

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(holdDuration);
        _canExit = true;
    }
}
