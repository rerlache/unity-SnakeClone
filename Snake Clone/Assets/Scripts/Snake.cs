using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public static Snake Instance { get; private set; }
    [SerializeField] Material bodyMaterial;
    public List<GameObject> snakePartList;
    public GameObject snakeHead;
    public int snakeLength = 3;
    public char moveDir;
    public Vector3 headTurningPos;
    public bool dirHasChanged;
    private void Awake()
    {
        Instance = this;
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
            StartCoroutine(UpdateBodyPartPositionDelayed());
        }
    }
    void InitSnakePartList()
    {
        for (int i = 0; i < snakeLength; i++)
        {
            CreateNewBodyPartAndAdd(GetPositionFromPrevious(snakePartList[i].transform.position));
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
    void CreateNewBodyPartAndAdd(Vector3 newPos)
    {
        GameObject previousPart = snakePartList[snakePartList.Count - 1].gameObject;
        snakePartList.Add(CreateBodyPart(newPos));
    }
    public void StretchSnakeAfterEating()
    {
        snakeLength++;
        GameObject previousPart = snakePartList[snakePartList.Count - 1].gameObject;
        snakePartList.Add(CreateBodyPart(GetPositionFromPrevious(snakePartList[snakeLength - 1].transform.position)));
    }
    GameObject CreateBodyPart(Vector3 newPos)
    {
        GameObject newBodyPart = CreateNewTile();
        newBodyPart.transform.name = (transform.childCount + 1).ToString();
        newBodyPart.GetComponent<Renderer>().material = bodyMaterial;
        newBodyPart.transform.parent = transform;
        newBodyPart.transform.position = newPos;
        return newBodyPart;
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
        for (int i = 1; i < transform.childCount; i++)
        {
            Vector3 newPos = GetPositionFromPrevious(snakePartList[i - 1].transform.position);
            yield return new WaitForSeconds(Player.DELAYTIME);
            if (dirHasChanged)
            {
                newPos.x--;
                newPos.y--;
                dirHasChanged = false;
            }
            if (i > 0 && i < snakePartList.Count)
            {
                snakePartList[i].transform.position = newPos;
            }
            else{Debug.Log("failed because index: " + i);}
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
