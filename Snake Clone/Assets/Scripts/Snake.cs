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
        for (int i = 1; i < snakePartList.Count; i++)
        {
            GameObject bodyPart = snakePartList[i].gameObject;
            GameObject prevObj = snakePartList[i - 1].gameObject;
            bodyPart.GetComponent<BodyPart>().headPosition = headTurningPos;
        }
    }
}
