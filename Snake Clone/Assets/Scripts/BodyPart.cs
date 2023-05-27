using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public static BodyPart Instance { get; private set; }
    public float listPosition;
    public float xPos;
    public float yPos;
    public Vector3 headPosition;
    public bool moveUp;
    public bool moveDown;
    public bool moveLeft;
    public bool moveRight;
    public Vector3 prevItemPosition;
    char prevDir;
    private void Awake()
    {
        Instance = this;
    }
    public void SetPreviousDirection(char dir)
    {
        prevDir = dir;
    }
    private void LateUpdate()
    {
        if (GameManager.Instance.GameState == GameManager.State.PLAY)
        {
            transform.position = new Vector3(xPos, yPos, 0);
            MovePart();
            moveRight = Snake.Instance.moveDir == 'r';
            moveLeft = Snake.Instance.moveDir == 'l';
            moveUp = Snake.Instance.moveDir == 'u';
            moveDown = Snake.Instance.moveDir == 'd';
        }
    }
    void MovePart()
    {
        if (moveRight)
        {
            xPos = headPosition.x - listPosition;
        }
        else if (moveLeft)
        {
            xPos = headPosition.x + listPosition;
        }
        if (moveUp)
        {
            yPos = headPosition.y - listPosition;
        }
        else if (moveDown)
        {
            yPos = headPosition.y + listPosition;
        }
    }
}
