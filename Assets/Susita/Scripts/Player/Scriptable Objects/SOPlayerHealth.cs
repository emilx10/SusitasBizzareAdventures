using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealthSettings", menuName = "ScriptableObjects/PlayerHealthSettings")]
public class SOPlayerHealth : ScriptableObject
{
    [Header("Health")]
    public float maxHealth;
    public float speedMultWhenDamaged;
    public float deathDelay;

    [Header("Heat Management")]
    public float chillTime;
    public float maxHeat = 100;
    public float restChill;
    public float maxSpeedHeat;
    public float overheatTime;
    public float postOverheat;
    public float smokeMult;
}
