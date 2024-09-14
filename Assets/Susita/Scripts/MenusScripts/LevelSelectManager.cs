using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelSelectManager : MonoBehaviour
{


    [SerializeField] private GameObject lvl1Panel;
    [SerializeField] private GameObject lvl2Panel;
    [SerializeField] private GameObject lvl3Panel;
    [SerializeField] private GameObject ReturnPanel;
    private int _currentSelectedLevel = 1;
    

    // Start is called before the first frame update
    void Awake()
    {
        ShowLvl1Panel();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (_currentSelectedLevel == 1) _currentSelectedLevel = 4;
            else _currentSelectedLevel--;
            ShowPanel(_currentSelectedLevel);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (_currentSelectedLevel == 4) _currentSelectedLevel = 1;
            else _currentSelectedLevel++;
            ShowPanel(_currentSelectedLevel);
        }
    }

    private void ShowPanel(int level)
    {
        switch (level)
        {
            case 1:
                ShowLvl1Panel();
                break;
            case 2:
                ShowLvl2Panel();
                break;
            case 3:
                ShowLvl3Panel();
                break;
            case 4:
                ShowReturnPanel();
                break;
            default:
                break;
        }
    }

    private void ShowLvl1Panel() 
    {
        lvl1Panel.SetActive(true);
        lvl2Panel.SetActive(false);
        lvl3Panel.SetActive(false);
        ReturnPanel.SetActive(false);
    }

    private void ShowLvl2Panel()
    {
        lvl1Panel.SetActive(false);
        lvl2Panel.SetActive(true);
        lvl3Panel.SetActive(false);
        ReturnPanel.SetActive(false);
    }
    private void ShowLvl3Panel()
    {
        lvl1Panel.SetActive(false);
        lvl2Panel.SetActive(false);
        lvl3Panel.SetActive(true);
        ReturnPanel.SetActive(false);
    }
    private void ShowReturnPanel()
    {
        lvl1Panel.SetActive(false);
        lvl2Panel.SetActive(false);
        lvl3Panel.SetActive(false);
        ReturnPanel.SetActive(true);
    }
}
