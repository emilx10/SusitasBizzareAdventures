using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHandler : MonoBehaviour
{
    [SerializeField] private float _growTime;
    [SerializeField] private Transform _killer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(FlowLava));
    }

    private IEnumerator FlowLava()
    {
        Vector3 initialScale = Vector3.zero;
        Vector3 finalScale = Vector3.one;
        float elapsedTime = 0;

        while (elapsedTime < _growTime)
        {
            _killer.localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / _growTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it reaches the final scale
        _killer.localScale = finalScale;
    }
}
