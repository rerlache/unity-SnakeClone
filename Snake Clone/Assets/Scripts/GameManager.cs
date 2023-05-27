using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject panelMainMenu;
    [SerializeField] GameObject panelGame;
    [SerializeField] GameObject panelInstructions;
    [SerializeField] GameObject panelHighscore;
    [SerializeField] GameObject panelGameOver;
    public static GameManager Instance { get; set; }
    State _gameState;
    public State GameState
    {
        get { return _gameState; }
        set { _gameState = value; }
    }

    public enum State { MENU, PLAY, HIGHSCORE, INSTRUCTIONS, GAMEOVER };
    void Awake()
    {
        Instance = this;
        SwitchState(State.MENU);
    }
    public void SwitchState(State newState, float delay = 0)
    {
        State prevState = GameState;
        StartCoroutine(SwitchDelay(newState, delay));
        if (prevState == State.MENU && newState == State.PLAY)
        {
            Player.Instance.movingRight = true;
            Snake.Instance.moveDir = 'r';
        }
    }
    IEnumerator SwitchDelay(State newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        EndState();
        GameState = newState;
        BeginState(newState);
    }
    void BeginState(State newState)
    {
        Debug.Log("Entering State: " + newState);
        switch (newState)
        {
            case State.MENU:
                panelMainMenu.SetActive(true);
                break;
            case State.PLAY:
                panelGame.SetActive(true);
                break;
            case State.HIGHSCORE:
                panelHighscore.SetActive(true);
                break;
            case State.INSTRUCTIONS:
                panelInstructions.SetActive(true);
                break;
            case State.GAMEOVER:
                panelGameOver.SetActive(true);
                break;
            default:
                break;
        }
    }
    void EndState()
    {
        Debug.Log("Ending State: " + _gameState);
        switch (_gameState)
        {
            case State.MENU:
                panelMainMenu.SetActive(false);
                break;
            case State.PLAY:
                panelGame.SetActive(false);
                break;
            case State.HIGHSCORE:
                panelHighscore.SetActive(false);
                break;
            case State.INSTRUCTIONS:
                panelInstructions.SetActive(false);
                break;
            case State.GAMEOVER:
                panelGameOver.SetActive(false);
                break;
            default:
                break;
        }
    }
}
