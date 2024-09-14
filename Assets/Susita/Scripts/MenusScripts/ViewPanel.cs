using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    private int _index = 0;

    public bool IsOver
    {
        get { return _index >= items.Length; }
    }

    public void HideAllItems()
    {
        foreach (var item in items)
        {
            item.SetActive(false);
        }
    }

    public bool ViewNextItem()
    {
        if(IsOver) 
        {
            return false;
        }

        items[_index].SetActive(true);

        _index++;

        return true;  
    }
    
}
