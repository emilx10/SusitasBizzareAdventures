using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenesManager : MonoBehaviour
{
    public void EndScene()
    {
        SceneManager.LoadScene("EndGameScene");
    }

    public void StartScene()
    {
        PlayerPrefs.SetInt("Panel", 0);
        SceneManager.LoadScene("Panels");
    }

    public void Level1()
    {
        PlayerPrefs.SetInt("Panel", 0);
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        PlayerPrefs.SetInt("Panel", 3);
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
