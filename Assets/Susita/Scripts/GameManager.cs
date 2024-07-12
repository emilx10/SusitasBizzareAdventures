using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PlayerHealth _player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerPrefs.SetString("Level", SceneManager.GetActiveScene().name);
    }

    public PlayerHealth GetPlayerHealth()
    {
        return _player;
    }
}
