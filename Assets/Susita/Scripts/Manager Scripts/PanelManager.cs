using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class PanelManager : MonoBehaviour
{
    public static int PANEL_VALUE;

    [SerializeField] GameObject[] panels;

    private bool _canPass = false;

    private void Awake()
    {
        CloseAllPanels();
        TurnCurrentPanelOn();
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(5f);
        _canPass = true;
    }

    private void Update()
    {
        if (Input.anyKeyDown && _canPass)
        {
            _canPass = false;
            if(PANEL_VALUE == 0)
            {
                //level 1
            }
            if (PANEL_VALUE == 1)
            {
                //level 2 pre
            }
            if (PANEL_VALUE == 2)
            {
                //level 2 
            }
            if (PANEL_VALUE == 3)
            {
                //level 3 pre
            }
            if (PANEL_VALUE == 4)
            {
                //level 3 
            }

            if (PANEL_VALUE == 5)
            {
                //Main Menu
            }
        }
    }

    void TurnCurrentPanelOn()
    {
        panels[PANEL_VALUE].SetActive(true);
    }
    void CloseAllPanels()
    {
        foreach (var panel in panels) 
        { 
            panel.SetActive(false);
        }
    }
}
