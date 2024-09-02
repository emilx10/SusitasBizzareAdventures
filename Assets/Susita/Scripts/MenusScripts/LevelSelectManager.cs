using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelSelectManager : MonoBehaviour
{


    [SerializeField] private GameObject lvl1Panel;
    [SerializeField] private GameObject lvl2Panel;
    [SerializeField] private GameObject lvl3Panel;
    private int _currentSelectedLevel = 1;
    

    // Start is called before the first frame update
    void Awake()
    {
        ShowLvl1Panel();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_currentSelectedLevel == 2)
            {
                _currentSelectedLevel++;
                ShowLvl3Panel();
            }

            if (_currentSelectedLevel == 1)
            {
                _currentSelectedLevel++;
                ShowLvl2Panel();
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (_currentSelectedLevel == 2)
            {
                _currentSelectedLevel--;
                ShowLvl1Panel();
            }

            if (_currentSelectedLevel == 3)
            {
                _currentSelectedLevel--;
                ShowLvl2Panel();
            }
        }
    }

    private void ShowLvl1Panel() 
    {
        lvl1Panel.SetActive(true);
        lvl2Panel.SetActive(false);
        lvl3Panel.SetActive(false);
    }

    private void ShowLvl2Panel()
    {
        lvl1Panel.SetActive(false);
        lvl2Panel.SetActive(true);
        lvl3Panel.SetActive(false);
    }
    private void ShowLvl3Panel()
    {
        lvl1Panel.SetActive(false);
        lvl2Panel.SetActive(false);
        lvl3Panel.SetActive(true);
    }
}
