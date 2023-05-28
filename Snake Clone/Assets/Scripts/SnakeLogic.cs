using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeLogic : MonoBehaviour
{
    public Vector3 direction = new Vector3(1, 0, 0);
    public List<BodyPartLogic> body = new();
    public List<Vector3> partPositions = new();
    public int snakeSize;
    public BodyPartLogic head;
    public BodyPartLogic partPrefab;
    bool movingRight;
    bool movingLeft;
    bool movingDown;
    bool movingUp;
    private void Start()
    {
        body.Clear();
        partPositions.Clear();
        partPositions.Add(Vector3.zero);
    }
    private void Update()
    {
        if (GameManager.Instance.GameState == GameManager.State.PLAY)
        {
            if (Input.GetAxis("Horizontal") > 0 && !movingLeft)
            {
                movingRight = true;
                direction = new Vector3(1, 0, 0);
                movingDown = movingLeft = movingUp = false;
            }
            else if (Input.GetAxis("Horizontal") < 0 && !movingRight)
            {
                movingLeft = true;
                direction = new Vector3(-1, 0, 0);
                movingDown = movingRight = movingUp = false;
            }
            else if (Input.GetAxis("Vertical") > 0 && !movingDown)
            {
                movingUp = true;
                direction = new Vector3(0, 1, 0);
                movingDown = movingRight = movingLeft = false;
            }
            else if (Input.GetAxis("Vertical") < 0 && !movingUp)
            {
                movingDown = true;
                direction = new Vector3(0, -1, 0);
                movingUp = movingRight = movingLeft = false;
            }
        }
    }
    public void MoveSnake()
    {
        head.Move(direction);
        foreach (BodyPartLogic part in body)
        {
            part.transform.position = partPositions[body.IndexOf(part)];
        }
    }
    public void AddBodyPart()
    {
        snakeSize++;
        BodyPartLogic newPart = Instantiate(partPrefab, partPositions[0], new Quaternion(0, 0, 0, 0), transform);
        newPart.name = body.Count.ToString();
        body.Add(newPart);
    }
}
