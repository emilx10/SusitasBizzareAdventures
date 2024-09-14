using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPointLevel3 : MonoBehaviour
{
    [SerializeField] GameObject _exitPoint;
    [SerializeField] ChangeScenesManager _changeScenesManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Outside Player");

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Inside Player");
            _changeScenesManager.ViewPanels(5);

        }


        if (collision.CompareTag("Enemy"))
        {
            _changeScenesManager.EndScene();
        }
    }
}
