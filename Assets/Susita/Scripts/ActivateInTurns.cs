using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInTurns : MonoBehaviour
{
    private bool _activated;
    [SerializeField] private float _delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCollapse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCollapse()
    {
        if (_activated) return;
        _activated = true;
        StartCoroutine(nameof(Collapsing));
    }

    private IEnumerator Collapsing()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(_delay);
        }   
    }


}
