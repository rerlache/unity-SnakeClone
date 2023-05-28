using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartLogic : MonoBehaviour
{
    public bool isHead = false;
    public SnakeLogic snake;
    public void Move(Vector2 direction)
    {
        gameObject.transform.position = new Vector3(
            Mathf.RoundToInt(gameObject.transform.position.x + direction.x),
            Mathf.RoundToInt(gameObject.transform.position.y + direction.y),
            0);
        if (isHead)
        {
            if (snake.snakeSize > 0 && snake.partPositions.Count < snake.snakeSize)
            {
                snake.partPositions.Add(gameObject.transform.position);
            }
            else if (snake.snakeSize > 0 && snake.partPositions.Count == snake.snakeSize)
            {
                snake.partPositions.Add(gameObject.transform.position);
                snake.partPositions.RemoveAt(0);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (isHead)
        {
            if (other.tag == "Wall")
            {
                GameManager.Instance.SwitchState(GameManager.State.GAMEOVER);
            }
            else if (other.tag == "BodyPart")
            {
                int partIndex = Int32.Parse(other.name);
                if (partIndex > 2)
                {
                    GameManager.Instance.SwitchState(GameManager.State.GAMEOVER);
                }
            }
        }
    }
}
