using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenesManager : MonoBehaviour
{
    public void AnimalCollectionScene()
    {
        SceneManager.LoadScene("AnimalCollection");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenuScene");
    }
    public void EndScene()
    {
        SceneManager.LoadScene("EndGameScene");
    }

    public void ViewPanels(int type)
    {
        ViewPanelsManager.CURRENT_VIEW_PANEL = (ViewPanelType)type;
        SceneManager.LoadScene("Panels");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void LastLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}
