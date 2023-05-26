using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player PlayerInstance { get; private set; }
    public int Score { get; set; }
    int highScore;
    
    static float DELAYTIME = .17f;
    public bool movingLeft;
    public bool movingRight = true;
    public bool movingUp;
    public bool movingDown;
    float timer;
    int inputCounter = 0;
    bool gameOver;
    Vector3 headTurningPos;
    void Start()
    {
        PlayerInstance = this;
        gameOver = false;
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 && !movingLeft)
        {
            movingRight = true;
            Snake.Instance.moveDir = 'r';
            movingDown = movingLeft = movingUp = false;
        }
        else if (Input.GetAxis("Horizontal") < 0 && !movingRight)
        {
            movingLeft = true;
            Snake.Instance.moveDir = 'l';
            movingDown = movingRight = movingUp = false;
        }
        else if (Input.GetAxis("Vertical") > 0 && !movingDown)
        {
            movingUp = true;
            Snake.Instance.moveDir = 'u';
            movingDown = movingRight = movingLeft = false;
        }
        else if (Input.GetAxis("Vertical") < 0 && !movingUp)
        {
            movingDown = true;
            Snake.Instance.moveDir = 'd';
            movingUp = movingRight = movingLeft = false;
        }
        if (inputCounter == 0 && !gameOver)
        {
            inputCounter = 1;
            StartCoroutine(TurnHeadDelayed());
            Snake.Instance.UpdateBodyPartPosition();
        }
    }
    void FixedUpdate()
    {
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(transform.position);
        GameOver();
    }

    void GameOver()
    {
        movingDown = false;
        movingUp = false;
        movingLeft = false;
        movingUp = false;
        gameOver = true;
        highScore = Score;
        Score = 0;
    }

    IEnumerator TurnHeadDelayed()
    {
        headTurningPos = Snake.Instance.snakeHead.transform.position;
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
