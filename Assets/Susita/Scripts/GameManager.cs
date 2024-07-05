using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PlayerHealth _player;

    private void Awake()
    {
        Instance = this;
    }

    public PlayerHealth GetPlayerHealth()
    {
        return _player;
    }
}
