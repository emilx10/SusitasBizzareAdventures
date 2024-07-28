using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel0;
    [SerializeField] GameObject panel1;
    [SerializeField] GameObject panel2;
    int panelNum;
    private void Awake()
    {
        panelNum = PlayerPrefs.GetInt("Panel");
        ChangePanels();
    }
    void ChangePanels()
    {
        if (panelNum == 0)
        {
            panel0.SetActive(true);
        }
        if (panelNum == 1)
        {
            panel1.SetActive(true);
        }
    }

    void CloseAllPanels()
    {

    }
}
