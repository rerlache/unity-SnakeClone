using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player PlayerInstance { get; private set; }
    public int Score { get; set; }
    int highScore;
    [SerializeField] List<GameObject> snakePartList;
    static float DELAYTIME = .17f;
    public bool movingLeft;
    public bool movingRight = true;
    public bool movingUp;
    public bool movingDown;
    float timer;
    int inputCounter = 0;
    bool gameOver;
    GameObject snakeHead;
    int snakeLength = 3;
    Vector3 headTurningPos;
    void Start()
    {
        PlayerInstance = this;
        gameOver = false;
        snakeHead = snakePartList[0];
        Debug.Log(snakePartList.Count);
        for (int i = 0; i < snakeLength; i++)
        {
            CreateNewBodyPartAndAdd(GetPositionFromPrevious(snakePartList[i].transform.position));
            UpdateBodyPartPosition();
        }
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
            StartCoroutine(TurnHeadDelayed());
            UpdateBodyPartPosition();
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
        headTurningPos = snakeHead.transform.position;
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
        yield return new WaitForSeconds(DELAYTIME);
        SetHeadPosition(new Vector3(x, y, 0));
        inputCounter = 0;
    }
    void SetHeadPosition(Vector3 newPos)
    {
        snakeHead.transform.position = newPos;
    }
    void UpdateBodyPartPosition()
    {
        for (int i = 1; i < snakePartList.Count; i++)
        {
            Vector3 newPos = GetPositionFromPrevious(snakePartList[i - 1].transform.position);
            snakePartList[i].transform.position = newPos;
        }
    }
    public void CreateNewBodyPartAndAdd(Vector3 newPos)
    {
        GameObject previousPart = snakePartList[snakePartList.Count - 1].gameObject;
        GameObject newBodyPart = CreateNewTile();
        newBodyPart.transform.parent = transform;
        newBodyPart.transform.position = newPos;
        snakePartList.Add(newBodyPart);
    }
    public void StretchSnakeAfterEating(){
        snakeLength++;
        GameObject previousPart = snakePartList[snakePartList.Count - 1].gameObject;
        GameObject newBodyPart = CreateNewTile();
        newBodyPart.transform.parent = transform;
        newBodyPart.transform.position = GetPositionFromPrevious(snakePartList[snakeLength - 1].transform.position);
        snakePartList.Add(newBodyPart);
    }
    Vector3 GetPositionFromPrevious(Vector3 prevPosition)
    {
        int x = ((int)Mathf.Round(prevPosition.x));
        int y = ((int)Mathf.Round(prevPosition.y));
        x = movingRight ? x - 1 : movingLeft ? x + 1 : x;
        y = movingUp ? y - 1 : movingDown ? y + 1 : y;
        return new(x, y, 0);
    }
    GameObject CreateNewTile()
    {
        GameObject newTile = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newTile.transform.localScale += new Vector3(0, 0, -0.5f);
        newTile.AddComponent<BoxCollider>();
        return newTile;
    }
}
