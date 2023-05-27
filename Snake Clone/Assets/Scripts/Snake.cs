using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public static Snake Instance { get; private set; }
    [SerializeField] GameObject bodyPartPrefab;
    public List<GameObject> snakePartList;
    public GameObject snakeHead;
    public int snakeLength = 3;
    public char moveDir;
    public Vector3 headTurningPos;
    BodyPart bodyPartScript;
    private void Awake()
    {
        Instance = this;
        bodyPartScript = gameObject.GetComponent<BodyPart>();
    }
    void Start()
    {
        snakeHead = snakePartList[0];
        InitSnakePartList();
    }
    void FixedUpdate()
    {
        if (GameManager.Instance.GameState == GameManager.State.GAMEOVER)
        {
            DestroyBodyPartSphere();
            CleanUpBodyPartList();
            snakeLength = 3;
            snakeHead.transform.position = new Vector3(0, 0, 0);
            InitSnakePartList();
        }
        if (GameManager.Instance.GameState == GameManager.State.PLAY)
        {
            UpdateBodyPartPosition();
        }
    }
    void InitSnakePartList()
    {
        for (int i = 0; i < snakeLength; i++)
        {
            GameObject bodyPart = Instantiate(bodyPartPrefab, transform);
            bodyPart.name = "Part" + i;
            bodyPart.GetComponent<BodyPart>().listPosition = i + 1;
            bodyPart.GetComponent<BodyPart>().xPos = -bodyPart.GetComponent<BodyPart>().listPosition;
            bodyPart.GetComponent<BodyPart>().yPos = 0;
            bodyPart.GetComponent<BodyPart>().prevItemPosition = snakePartList[snakePartList.Count - 1].transform.position;
            snakePartList.Add(bodyPart);
        }
    }
    void DestroyBodyPartSphere()
    {
        for (int i = 1; i <= snakeLength; i++)
        {
            Destroy(snakePartList[i]);
        }
    }
    void CleanUpBodyPartList()
    {
        snakePartList.RemoveRange(1, snakePartList.Count - 1);
    }
    public void StretchSnakeAfterEating()
    {
        snakeLength++;
        snakePartList.Add(CreateBodyPartFromPrefab());
    }
    GameObject CreateBodyPartFromPrefab()
    {
        float lastIndex = snakePartList.Count;
        GameObject newBodyPart = Instantiate(bodyPartPrefab, transform);
        newBodyPart.transform.name = "Part" + (lastIndex).ToString();
        newBodyPart.GetComponent<BodyPart>().listPosition = lastIndex;
        newBodyPart.GetComponent<BodyPart>().xPos = -lastIndex;
        newBodyPart.GetComponent<BodyPart>().prevItemPosition = snakePartList[snakePartList.Count - 1].transform.position;
        return newBodyPart;
    }
    void UpdateBodyPartPosition()
    {
        Head headScript = GetComponentInChildren<Head>();
        for (int i = 1; i < snakePartList.Count; i++)
        {
            GameObject bodyPart = snakePartList[i].gameObject;
            Transform prevObj = snakePartList[i - 1].gameObject.transform;
            bodyPart.GetComponent<BodyPart>().headPosition = headTurningPos;
            bodyPart.GetComponent<BodyPart>().prevItemPosition = prevObj.position;
            if (headScript.xTurnPosition > bodyPart.GetComponent<BodyPart>().xPos)
            {
                bodyPart.GetComponent<BodyPart>().moveRight = true;
                bodyPart.GetComponent<BodyPart>().moveLeft = false;
                bodyPart.GetComponent<BodyPart>().xPos = bodyPart.GetComponent<BodyPart>().prevItemPosition.x - 1f;
            }
            else if (headScript.xTurnPosition < bodyPart.GetComponent<BodyPart>().xPos)
            {
                bodyPart.GetComponent<BodyPart>().moveLeft = true;
                bodyPart.GetComponent<BodyPart>().moveRight = false;
                bodyPart.GetComponent<BodyPart>().xPos = bodyPart.GetComponent<BodyPart>().prevItemPosition.x + 1f;
            }
            else if (headScript.xTurnPosition == bodyPart.GetComponent<BodyPart>().xPos)
            {
                if (headTurningPos.y > bodyPart.GetComponent<BodyPart>().yPos)
                {
                    bodyPart.GetComponent<BodyPart>().moveUp = true;
                    bodyPart.GetComponent<BodyPart>().moveDown = false;
                }
                else if (headTurningPos.y < bodyPart.GetComponent<BodyPart>().yPos)
                {
                    bodyPart.GetComponent<BodyPart>().moveDown = true;
                    bodyPart.GetComponent<BodyPart>().moveUp = false;
                }
            }
            else if (headScript.yTurnPosition > bodyPart.GetComponent<BodyPart>().yPos)
            {
                bodyPart.GetComponent<BodyPart>().moveUp = true;
                bodyPart.GetComponent<BodyPart>().moveDown = false;
                bodyPart.GetComponent<BodyPart>().yPos = bodyPart.GetComponent<BodyPart>().prevItemPosition.y - 1f;
            }
            else if (headScript.yTurnPosition < bodyPart.GetComponent<BodyPart>().yPos)
            {
                bodyPart.GetComponent<BodyPart>().moveDown = true;
                bodyPart.GetComponent<BodyPart>().moveUp = false;
                bodyPart.GetComponent<BodyPart>().yPos = bodyPart.GetComponent<BodyPart>().prevItemPosition.y + 1f;
            }
            else if (headScript.yTurnPosition == bodyPart.GetComponent<BodyPart>().yPos)
            {
                if (headTurningPos.x > bodyPart.GetComponent<BodyPart>().xPos)
                {
                    bodyPart.GetComponent<BodyPart>().moveRight = true;
                    bodyPart.GetComponent<BodyPart>().moveLeft = false;
                }
                else if (headTurningPos.x < bodyPart.GetComponent<BodyPart>().xPos)
                {
                    bodyPart.GetComponent<BodyPart>().moveLeft = true;
                    bodyPart.GetComponent<BodyPart>().moveRight = false;
                }
            }
        }
    }
}
