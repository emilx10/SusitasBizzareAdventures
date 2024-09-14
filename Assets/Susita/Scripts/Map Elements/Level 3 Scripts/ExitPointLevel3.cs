using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointLevel3 : MonoBehaviour
{
    [SerializeField] GameObject _exitBoulders;
    [SerializeField] GameObject _exitPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyCar"))
        {
            SpawnExit();
        }
    }

    private void SpawnExit()
    {
        Destroy(_exitBoulders);
        _exitPoint.SetActive(true);
    }
}
