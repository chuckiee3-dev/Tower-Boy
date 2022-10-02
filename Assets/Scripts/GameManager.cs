using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    NotStarted,
    Playing,
    Paused,
    Finished,
    Failed

}
public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public GameState state;
    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }

        state = GameState.NotStarted;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && state == GameState.NotStarted)
        {
            state = GameState.Playing;
            GameActions.GameStateChanged(state);
        }

        if (Input.GetKeyDown(KeyCode.R) && state is GameState.Failed or GameState.Finished)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Lose()
    {
        state = GameState.Failed;
        GameActions.GameStateChanged(state);
    }
    public void Win()
    {
        state = GameState.Finished;
        GameActions.GameStateChanged(state);
    }
}
