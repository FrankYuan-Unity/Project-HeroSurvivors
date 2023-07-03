using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    [SerializeField] Canvas menuCanvas;

    [Header(" ----- Button Input ------")]
    [SerializeField] Button btnResume;
    [SerializeField] Button btnRestart;
    [SerializeField] Button btnMainMenu;

    void OnEnable()
    {
        playerInput.OnGamePause += GamePause;
        playerInput.OnGameUnpause += GameUnpause;
        btnResume.onClick.AddListener(onResumeButtonClicked);
        btnRestart.onClick.AddListener(onRestartButtonClicked);
        btnMainMenu.onClick.AddListener(onMainMenuButtonClicked);
    }


    void OnDisable()
    {
        playerInput.OnGamePause -= GamePause;
        playerInput.OnGameUnpause -= GameUnpause;
        btnResume.onClick.RemoveListener(onResumeButtonClicked);
        btnRestart.onClick.RemoveListener(onRestartButtonClicked);
        btnMainMenu.onClick.RemoveListener(onMainMenuButtonClicked);
    }



    public void GameUnpause()
    {
        onResumeButtonClicked();
    }

    private void GamePause()
    {
        Time.timeScale = 0f;
        menuCanvas.enabled = true;
        playerInput.EnablePauseMenuInput();
        playerInput.SwithToDynamicUpdateMode();
        GameManager.GameState = GameState.Paused;

    }

    private void onResumeButtonClicked()
    {
        Time.timeScale = 1f;
        menuCanvas.enabled = false;
        playerInput.EnableGameActionInput();
        playerInput.SwitchToFixedUpdateMode();
        GameManager.GameState = GameState.Playing;
    }


    private void onRestartButtonClicked()
    {
        print(" -- Clicked Resume --");

        //TODO
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    private void onMainMenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
