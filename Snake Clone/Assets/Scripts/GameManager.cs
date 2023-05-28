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
    public int fieldHeight = 10, fieldWidth = 20;
    public float timerValue = .5f;
    public SnakeLogic snake;
    float timer;
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
    private void Update()
    {
        if (GameState == State.PLAY)
        {
            if (timer > timerValue)
            {
                timer = 0f;
                Tick();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
    void Tick()
    {
        snake.MoveSnake();
    }
    public void SwitchState(State newState, float delay = 0)
    {
        State prevState = GameState;
        StartCoroutine(SwitchDelay(newState, delay));
        if (prevState == State.MENU && newState == State.PLAY)
        {
            snake.head.transform.position = new Vector3(0, 0, 0);
            snake.direction = new Vector3(1, 0, 0);
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
                for (int i = 0; i < snake.body.Count; i++)
                {
                    Destroy(snake.body[i].gameObject);
                }
                snake.body.Clear();
                snake.partPositions.Clear();
                snake.partPositions.Add(Vector3.zero);
                snake.snakeSize = 1;
                break;
            default:
                break;
        }
    }
}
