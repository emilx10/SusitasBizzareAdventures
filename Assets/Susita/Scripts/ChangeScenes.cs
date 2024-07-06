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
        SceneManager.LoadScene("Level1");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void LastLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
    }
}
