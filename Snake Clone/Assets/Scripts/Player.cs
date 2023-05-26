using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public static float DELAYTIME = .17f;
    bool movingLeft;
    public bool movingRight;
    bool movingUp;
    bool movingDown;
    float timer;
    int inputCounter = 0;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
    }

    void Update()
    {
        if (GameManager.Instance.GameState == GameManager.State.PLAY)
        {
            if (Input.GetAxis("Horizontal") > 0 && !movingLeft)
            {
                movingRight = true;
                Snake.Instance.moveDir = 'r';
                Snake.Instance.dirHasChanged = true;
                movingDown = movingLeft = movingUp = false;
            }
            else if (Input.GetAxis("Horizontal") < 0 && !movingRight)
            {
                movingLeft = true;
                Snake.Instance.moveDir = 'l';
                Snake.Instance.dirHasChanged = true;
                movingDown = movingRight = movingUp = false;
            }
            else if (Input.GetAxis("Vertical") > 0 && !movingDown)
            {
                movingUp = true;
                Snake.Instance.moveDir = 'u';
                Snake.Instance.dirHasChanged = true;
                movingDown = movingRight = movingLeft = false;
            }
            else if (Input.GetAxis("Vertical") < 0 && !movingUp)
            {
                movingDown = true;
                Snake.Instance.moveDir = 'd';
                Snake.Instance.dirHasChanged = true;
                movingUp = movingRight = movingLeft = false;
            }
            if (inputCounter == 0)
            {
                inputCounter = 1;
                StartCoroutine(TurnHeadDelayed());
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        GameOver();
    }

    void GameOver()
    {
        movingDown = false;
        movingUp = false;
        movingLeft = false;
        movingUp = false;
        GameManager.Instance.SwitchState(GameManager.State.GAMEOVER);
    }

    IEnumerator TurnHeadDelayed()
    {
        Snake.Instance.headTurningPos = Snake.Instance.snakeHead.transform.position;
        int x = ((int)Mathf.Round(Snake.Instance.snakeHead.transform.position.x));
        int y = ((int)Mathf.Round(Snake.Instance.snakeHead.transform.position.y));
        if (movingRight)
        {
            if (x < 20)
            {
                x++;
            }
            else { GameOver(); }
        }
        else if (movingLeft)
        {
            if (x > -20)
            {
                x--;
            }
            else { GameOver(); }
        }
        else if (movingUp)
        {
            if (y < 11)
            {
                y++;
            }
            else { GameOver(); }
        }
        else if (movingDown)
        {
            if (y > -11)
            {
                y--;
            }
            else { GameOver(); }
        }
        yield return new WaitForSeconds(DELAYTIME);
        SetHeadPosition(new Vector3(x, y, 0));
        inputCounter = 0;
    }
    void SetHeadPosition(Vector3 newPos)
    {
        Snake.Instance.snakeHead.transform.position = newPos;
    }

}
