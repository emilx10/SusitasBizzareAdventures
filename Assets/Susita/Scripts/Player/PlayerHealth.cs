using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : EntityHealth
{
    private PlayerMovement _playerMovement=> GetComponent<PlayerMovement>();

    [SerializeField] private float _chillTime;

    [SerializeField][ReadOnly] private float _heat, _heatImmunity;
    [SerializeField] private float _restChill, _maxSpeedHeat, _overheatTime, _postOverheat, _smokeMult;
    private float _maxHeat = 100;

    [SerializeField] private Image _heatUI;

    [SerializeField] private Image _carryUI;

    [SerializeField] private Image _healthUI;

    [SerializeField] private ParticleSystem _smoke;

    [SerializeField] private float _speedMultWhenDamaged;

    private bool _dead;

    [SerializeField] private float _deathDelay;

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

    public void HealPlayerAmount(float amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth) 
        {
            _currentHealth= _maxHealth;
        }
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
        else if (engineHeat <= 0)
        {
            _heat -= _restChill * Time.deltaTime;
        }

        if (_heat> _maxHeat)
        {
            StartCoroutine(nameof(OverHeat));
        }

        ParticleSystem.EmissionModule e = _smoke.emission;
        e.rateOverTime = (_heat-_currentHealth)*_smokeMult;

        _heat = Mathf.Clamp(_heat, 0, _maxHeat);
        _heatUI.fillAmount = _heat/ _maxHeat;
        _healthUI.fillAmount = _currentHealth / _maxHealth;
    }

    private IEnumerator OverHeat()
    {
        float _timeLeft = _overheatTime;
        _playerMovement.AddSpeedModifier("Heat", 0);
        while (_timeLeft > 0) 
        {
            _timeLeft-=Time.deltaTime;
            _heat = Mathf.Lerp(_postOverheat, _maxHeat, _timeLeft/ _overheatTime);
            yield return null;
        }
        _playerMovement.RemoveSpeedModifier("Heat");
    } 


    [ContextMenu("Chill Bruh")]
    private void Chill()
    {
        _heat = 0;
    }

    public void ChillAmount(float chillAmount, float immunityTime) 
    {
        _heat -= chillAmount;
        _heatImmunity = immunityTime;
    }

    public void SetCarry(Sprite sprite)
    {
        _carryUI.sprite = sprite;
        _carryUI.gameObject.SetActive(true);
    }

    public void ClearCarry()
    {
        _carryUI.sprite = null;
        _carryUI.gameObject.SetActive(false);
    }

    public override void Die()
    {
        if (_dead) return;
        _dead = true;
        Invoke(nameof(DeathScene), _deathDelay);
    }

    private void DeathScene()
    {
        SceneManager.LoadScene("EndGameScene");
    }

    public override void OnHit()
    {
        _playerMovement.DivideSpeedInstantly(_speedMultWhenDamaged);
    }
}
