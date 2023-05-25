using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Player Instance { get; private set; }
    [SerializeField] List<GameObject> snakeArray;
    public bool movingLeft;
    public bool movingRight = true;
    public bool movingUp;
    public bool movingDown;
    float timer;
    int inputCounter = 0;
    bool gameOver;
    GameObject snakeHead;
    void Start()
    {
        Instance = this;
        gameOver = false;
        snakeHead = snakeArray[0];
        Debug.Log(snakeArray.Count);
        CreateNewBodyPart(3);
        UpdateBodyPartPosition();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            movingRight = true;
            movingDown = movingLeft = movingUp = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            movingLeft = true;
            movingDown = movingRight = movingUp = false;
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            movingUp = true;
            movingDown = movingRight = movingLeft = false;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            movingDown = true;
            movingUp = movingRight = movingLeft = false;
        }
        if (inputCounter == 0 && !gameOver)
        {
            inputCounter = 1;
            StartCoroutine(MovePlayerDelayed());
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
        //gameOver = true;
    }

    IEnumerator MovePlayerDelayed()
    {
        int x = ((int)Mathf.Round(snakeHead.transform.position.x));
        int y = ((int)Mathf.Round(snakeHead.transform.position.y));
        if (movingRight)
        {
            if (x < 19)
            {
                x++;
            }
            else { GameOver(); }
        }
        else if (movingLeft)
        {
            if (x > -19)
            {
                x--;
            }
            else { GameOver(); }
        }
        else if (movingUp)
        {
            if (y < 10)
            {
                y++;
            }
            else { GameOver(); }
        }
        else if (movingDown)
        {
            if (y > -10)
            {
                y--;
            }
            else { GameOver(); }
        }
        yield return new WaitForSeconds(.17f);
        SetHeadPosition(new Vector3(x, y, 0));
        UpdateBodyPartPosition();
        inputCounter = 0;
    }
    void SetHeadPosition(Vector3 newPos)
    {
        snakeHead.transform.position = newPos;
    }
    void UpdateBodyPartPosition()
    {
        for (int i = 1; i < snakeArray.Count; i++)
        {
            int x = ((int)Mathf.Round(snakeArray[i - 1].transform.position.x));
            int y = ((int)Mathf.Round(snakeArray[i - 1].transform.position.y));
            x = movingRight ? x++ : movingLeft ? x-- : x;
            y = movingUp ? y++ : movingDown ? y-- : y;
            GameObject newBodyPart = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newBodyPart.transform.position = new Vector3(x, y, 0);
            newBodyPart.transform.parent = transform;
            //GameObject.Destroy(snakeArray[i]);
            snakeArray.Add(gameObject);
        }
    }
    void CreateNewBodyPart(int howMany)
    {
        for (int i = 0; i < howMany; i++)
        {
            GameObject newBodyPart = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newBodyPart.transform.parent = transform;
            snakeArray.Add(newBodyPart);
        }
    }
}
