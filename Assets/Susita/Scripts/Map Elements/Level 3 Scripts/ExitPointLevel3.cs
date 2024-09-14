using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPointLevel3 : MonoBehaviour
{
    [SerializeField] GameObject _exitPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy Car"))
        {
            SceneManager.LoadScene("EndGameScene");
        }

        if (other.CompareTag("Player"))
        {
            //SceneManager.LoadScene();
        }
    }
}
