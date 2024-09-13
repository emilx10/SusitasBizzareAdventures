using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewPanelType
{
    Pre1,Post1,Pre2,Post2,Pre3,Post3
}

public class ViewPanelsManager : MonoBehaviour
{ 
    [SerializeField] private ChangeScenesManager changeScenesManager;

    public static ViewPanelType CURRENT_VIEW_PANEL;

    [SerializeField] private ViewPanel[] viewPanels;
    [SerializeField] private float viewDuration;
    private bool _canPass = false;

    private void Awake()
    {
        CloseAllPanels();
        ViewNextItem();
    }

    private IEnumerator Countdown()
    {
        _canPass = false;
        yield return new WaitForSeconds(viewDuration);
        _canPass = true;
    }

    private void Update()
    {
        if (Input.anyKeyDown && _canPass)
        {
            ViewNextItem();
        }
    }

    private void ViewNextItem()
    {
        ViewPanel current = viewPanels[(int)CURRENT_VIEW_PANEL];
        if (current.ViewNextItem())
        {
            StartCoroutine(Countdown());
            return;
        }

        PanelFinalAction();
    }
    private void PanelFinalAction()
    {
        switch (CURRENT_VIEW_PANEL)
        {
            case ViewPanelType.Post1:
            case ViewPanelType.Post2:
                viewPanels[(int)CURRENT_VIEW_PANEL].HideAllItems();
                CURRENT_VIEW_PANEL++;
                ViewNextItem();
                break;

            case ViewPanelType.Pre1:
                changeScenesManager.Level1();
                break;

            case ViewPanelType.Pre2:
                changeScenesManager.Level2();
                break;

            case ViewPanelType.Pre3:
                changeScenesManager.Level3();
                break;

            case ViewPanelType.Post3:
                changeScenesManager.MainMenu();
                break;
        }
    }
    private void CloseAllPanels()
    {
        foreach (var viewPanel in viewPanels) 
        {
            viewPanel.HideAllItems();
        }
    }
}
