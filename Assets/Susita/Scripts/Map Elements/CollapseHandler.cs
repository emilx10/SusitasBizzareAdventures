using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseHandler : MonoBehaviour
{
    [SerializeField] private float _warningTime,_killingDelay, _destroyTime;
    [SerializeField] private ParticleSystem _warningParticleSystem,_killingParticleSystem;
    [SerializeField] private GameObject _killer;

    public void TriggerCollapse()
    {
        Destroy(gameObject, _destroyTime);
        Invoke(nameof(Collapse), _warningTime);
        _warningParticleSystem.Play();
    }

    private void Collapse()
    {
        _killingParticleSystem.Play();
        Invoke(nameof(KILLER), _killingDelay);
    }

    private void KILLER()
    {
        _killer.SetActive(true);
    }
}
