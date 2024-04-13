using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GameManager
{
    public static GameStateMachine GameStateMachine => m_gameStateMachine;
    private static GameStateMachine m_gameStateMachine = new GameStateMachine(EGameState.None);

    public static bool IsGamePaused => m_gameStateMachine.GameState.HasFlag(EGameState.Pause);

    public static void OpenMainMenu()
    {
        m_gameStateMachine.RemoveState(EGameState.Pause);
        SceneLoader.LoadScenes(EScenes.MainMenuBackground, EScenes.MainMenuBackground | EScenes.UI, false,
        () => { m_gameStateMachine.ChangeState(EGameState.MainMenu); });
    }

    public static void PlayGame()
    {
#if UNITY_EDITOR
        Debug.Log("Starting game");
#endif
        m_gameStateMachine.RemoveState(EGameState.Pause);
        SceneLoader.LoadScenes(EScenes.Game, EScenes.Game | EScenes.UI, false,
        () => { m_gameStateMachine.ChangeState(EGameState.Game); });
    }

    public static void PauseGame()
    {
        Time.timeScale = 0f;
        m_gameStateMachine.AddState(EGameState.Pause);
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1f;
        m_gameStateMachine.RemoveState(EGameState.Pause);
    }

    public static void OpenSettingsPause()
    {
        m_gameStateMachine.AddState(EGameState.SettingsPause);
    }

    public static void CloseSettingsPause()
    {
        m_gameStateMachine.RemoveState(EGameState.SettingsPause);
    }

    public static void OpenSettingsMainMenu()
    {
        m_gameStateMachine.AddState(EGameState.SettingsMainMenu);
    }

    public static void CloseSettingsMainMenu()
    {
        m_gameStateMachine.RemoveState(EGameState.SettingsMainMenu);
    }

    public static void TriggerVictory()
    {
#if UNITY_EDITOR
        Debug.Log("Victory !");
#endif
        m_gameStateMachine.ChangeState(EGameState.Victory);
    }

    public static void TriggerGameOver()
    {
#if UNITY_EDITOR
        Debug.Log("GameOver !");
#endif
        m_gameStateMachine.ChangeState(EGameState.GameOver);
    }

    public static void ExitGame()
    {
#if UNITY_EDITOR
        Debug.Log("Quitting game");
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}