using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : EntityHealth
{
    private PlayerMovement _playerMovement=> GetComponent<PlayerMovement>();

    [SerializeField] private float _chillTime;

    [SerializeField][ReadOnly]private float _heat, _heatImmunity;
    [SerializeField] private float _restChill, _maxSpeedHeat, _extinguisherChill, _extinguisherImmunityTime, _overheatTime, _postOverheat;
    private float _maxHeat = 100;

    void Start()
    {
        HealPlayer();
    }

    private void Update()
    {
        HandleHeat();
    }

    [ContextMenu("Heal")]
    private void HealPlayer()
    {
        _currentHealth = _maxHealth;
    }

    private void HandleHeat()
    {
        float engineHeat = _playerMovement.GetSpeedPercentage();

        if (_heatImmunity > 0)
        {
            _heatImmunity -= Time.deltaTime;
        }

        if (engineHeat > 0 && _heatImmunity <= 0)
        {
            _heat += _maxSpeedHeat * engineHeat * Time.deltaTime;
        }
        else
        {
            _heat -= _restChill * Time.deltaTime;
        }

        if (_heat> _maxHeat)
        {
            StartCoroutine(nameof(OverHeat));
        }

        _heat = Mathf.Clamp(_heat, 0, _maxHeat);
    }

    private IEnumerator OverHeat()
    {
        float _timeLeft = _overheatTime;
        _playerMovement.AddSpeedModifier("Heat", 0);
        while (_timeLeft > 0) 
        {
            _timeLeft-=Time.deltaTime;
            _heat = Mathf.Lerp(_heat, _postOverheat, _overheatTime - _timeLeft);
            yield return null;
        }
        _playerMovement.RemoveSpeedModifier("Heat");
    } 


    [ContextMenu("Chill Bruh")]
    public void Chill()
    {
        _heat -= _extinguisherChill;
        _heatImmunity = _extinguisherImmunityTime;
    }

    
}
