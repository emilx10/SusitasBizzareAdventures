using UnityEngine;

public class LavaFlowHandler : MonoBehaviour
{
    [SerializeField] private Transform _lava;
    private bool _isFlowing;
    [SerializeField] private Transform _lavaEndPos;
    [SerializeField] private float _lavaSpeed;
    private void Update()
    {
        if (_isFlowing)
        {
            _lava.transform.position = Vector2.MoveTowards(_lava.transform.position, _lavaEndPos.position, _lavaSpeed*Time.deltaTime);
        }
    }

    public void StartFlow()
    {
        _isFlowing = true;
    }

    public void StopFlow()
    {
        _isFlowing &= false;
    }
}
