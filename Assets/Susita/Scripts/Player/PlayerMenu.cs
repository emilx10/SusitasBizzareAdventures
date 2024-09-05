using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    private bool _paused;
    [SerializeField] private GameObject _playerMenu;
    [SerializeField] private Button _resumeButton;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (_paused) //Unpause
        {
            _playerMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else         //pause
        {
            _playerMenu.SetActive(true);
            Time.timeScale = 0f;
            _resumeButton.Select();
        }
        _paused = !_paused;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenuScene");
    }
}
