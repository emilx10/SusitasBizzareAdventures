using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void EndScene()
    {
        SceneManager.LoadScene("EndGameScene");
    }

    public void StartScene()
    {
        SceneManager.LoadScene("Emil");
    }
}
