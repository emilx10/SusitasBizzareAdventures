using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCollapse : MonoBehaviour
{
    [SerializeField] private float _warningTime,_killingDelay, _destroyTime;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _killer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _destroyTime);
        Invoke(nameof(Collapse), _warningTime);
    }

    private void Collapse()
    {
        _particleSystem.Play();
        Invoke(nameof(KILLER), _killingDelay);
    }

    private void KILLER()
    {
        _killer.SetActive(true);
    }
}
