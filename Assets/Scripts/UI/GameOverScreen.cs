using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Button btnRevive;
    [SerializeField] Button btnRestart;
    [SerializeField] Button btnMainMenu;

    [SerializeField] Canvas controlCanvas;
    [SerializeField] Canvas pauseMenuCanvas;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] PlayerInput playerInput;

    [SerializeField] GameObject player;

    private void OnEnable()
    {
        Time.timeScale = 1f;
        StartGameScript.OnRewardVideoRewarded += OnRewardVideoRewarded;
        btnRevive.onClick.AddListener(OnReviveButtonClicked);
        btnRestart.onClick.AddListener(OnRestartButtonClicked);
        btnMainMenu.onClick.AddListener(OnMainMenuButtonClicked);
        GameManager.onGameOver += OnGameOver;
    }


    private void OnDisable()
    {
        StartGameScript.OnRewardVideoRewarded -= OnRewardVideoRewarded;

        btnRevive.onClick.RemoveListener(OnReviveButtonClicked);
        btnRestart.onClick.RemoveListener(OnRestartButtonClicked);
        btnMainMenu.onClick.RemoveListener(OnMainMenuButtonClicked);
        GameManager.onGameOver -= OnGameOver;
    }

    private void OnRewardVideoRewarded()
    {
        OnRevive();
    }

    private void OnGameOver()
    {
        playerInput.EnableGameOverScreenInput();
        StartCoroutine(nameof(GameOverCoroutine));
    }

    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        controlCanvas.enabled = false;
        pauseMenuCanvas.enabled = false;
        gameOverCanvas.enabled = true;
        playerInput.DisableAllActions();
    }



    // Animation Event
    void EnableGameOverScreenInput()
    {
        // playerInput.EnableGameOverScreenInput();
    }

    private void OnReviveButtonClicked()
    {

        if (StartGameScript.playAdsVideo)
        {
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo();
            }
        }
        else
        {
            OnRevive();
        }
    }

    private void OnRevive()
    {
        player.SetActive(true);
        player.GetComponent<PlayerControl>().Revive();
        gameOverCanvas.enabled = false;
        controlCanvas.enabled = true;
        Time.timeScale = 1f;
    }

    private void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    private void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    private void onResume()
    {
        Time.timeScale = 1f;

        playerInput.EnableGameActionInput();
        playerInput.SwitchToFixedUpdateMode();
        GameManager.GameState = GameState.Playing;
    }

}
