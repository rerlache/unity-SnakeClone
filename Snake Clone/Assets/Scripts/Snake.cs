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
    public Vector3 headTurningPos;
    public bool dirHasChanged;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        snakeHead = snakePartList[0];
        for (int i = 0; i < snakeLength; i++)
        {
            CreateNewBodyPartAndAdd(GetPositionFromPrevious(snakePartList[i].transform.position));
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(UpdateBodyPartPositionDelayed());
        //UpdateBodyPartPosition();
    }
    void CreateNewBodyPartAndAdd(Vector3 newPos)
    {
        GameObject previousPart = snakePartList[snakePartList.Count - 1].gameObject;
        GameObject newBodyPart = CreateNewTile();
        newBodyPart.transform.parent = transform;
        newBodyPart.transform.position = newPos;
        snakePartList.Add(newBodyPart);
    }
    public void StretchSnakeAfterEating()
    {
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
    void UpdateBodyPartPosition()
    {
        for (int i = 1; i < snakePartList.Count; i++)
        {
            Vector3 newPos = GetPositionFromPrevious(snakePartList[i - 1].transform.position);
            if (dirHasChanged)
            {
                newPos.x--;
                newPos.y--;
                dirHasChanged = false;
            }
            snakePartList[i].transform.position = newPos;
        }
    }
    IEnumerator UpdateBodyPartPositionDelayed()
    {
        for (int i = 1; i < snakePartList.Count; i++)
        {
            Vector3 newPos = GetPositionFromPrevious(snakePartList[i - 1].transform.position);
            yield return new WaitForSeconds(Player.DELAYTIME);
            if (dirHasChanged)
            {
                newPos.x--;
                newPos.y--;
                dirHasChanged = false;
            }
            snakePartList[i].transform.position = newPos;
        }
    }
    Vector3 GetPositionFromPrevious(Vector3 prevPosition)
    {
        int x = ((int)Mathf.Round(prevPosition.x));
        int y = ((int)Mathf.Round(prevPosition.y));
        x = moveDir == 'r' ? x-- : moveDir == 'l' ? x++ : x;
        y = moveDir == 'u' ? y-- : moveDir == 'd' ? y++ : y;
        return new(x, y, 0);
    }
}
