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
            //moveRight = xPos < prevItemPosition.x;
            //moveLeft = xPos > prevItemPosition.x;
            //moveUp = yPos < prevItemPosition.y;
            //moveDown = yPos > prevItemPosition.y;
            //MovePart();
        }
    }
    void MovePart()
    {
        if (moveRight)
        {
            xPos = prevItemPosition.x - 1;
        }
        else if (moveLeft)
        {
            xPos = prevItemPosition.x + 1;
        }
        if (moveUp)
        {
            yPos = prevItemPosition.y - 1;
        }
        else if (moveDown)
        {
            yPos = prevItemPosition.y + 1;
        }
    }
}
