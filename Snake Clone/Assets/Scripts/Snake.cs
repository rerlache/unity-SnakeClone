using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public static Snake Instance { get; private set; }
    public List<GameObject> snakePartList;
    public GameObject snakeHead;
    public int snakeLength = 3;
    public char moveDir;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        snakeHead = snakePartList[0];
        for (int i = 0; i < snakeLength; i++)
        {
            CreateNewBodyPartAndAdd(GetPositionFromPrevious(snakePartList[i].transform.position));
            UpdateBodyPartPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void CreateNewBodyPartAndAdd(Vector3 newPos)
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
    GameObject CreateNewTile()
    {
        GameObject newTile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        newTile.transform.localScale += new Vector3(0, 0, -0.5f);
        newTile.AddComponent<SphereCollider>();
        return newTile;
    }
    public void UpdateBodyPartPosition()
    {
        for (int i = 1; i < snakePartList.Count; i++)
        {
            Vector3 newPos = GetPositionFromPrevious(snakePartList[i - 1].transform.position);
            snakePartList[i].transform.position = newPos;
        }
    }
    
    Vector3 GetPositionFromPrevious(Vector3 prevPosition)
    {
        int x = ((int)Mathf.Round(prevPosition.x));
        int y = ((int)Mathf.Round(prevPosition.y));
        x = moveDir == 'r' ? x - 1 : moveDir =='l' ? x + 1 : x;
        y = moveDir == 'u' ? y - 1 : moveDir == 'd' ? y + 1 : y;
        return new(x, y, 0);
    }
}
