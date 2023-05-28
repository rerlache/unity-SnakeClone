using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] Material fivePointColor;
    [SerializeField] Material tenPointColor;
    [SerializeField] Mesh fivePointMesh;
    [SerializeField] Mesh tenPointMesh;
    public SnakeLogic snake;
    int x = 10;
    int y = 0;
    int z = 0;
    int score = 5;
    int foodCount;
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    private void Start() {
        meshRenderer = transform.GetComponent<MeshRenderer>();
        meshFilter = transform.GetComponent<MeshFilter>();
        SpawnPoint();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Head")
        {
            foodCount++;
            score = GetPoints();
            SpawnPoint();
            snake.AddBodyPart();
            Score.Instance.currentScore += score;
        }
    }
    int GetPoints(){
        int points = 5;
        int random = Random.Range(5,11);
        if(foodCount >= random){
            points = 10;
            foodCount = 0;
            GameManager.Instance.timerValue = GameManager.Instance.timerValue - 0.015f;
        }
        Debug.Log(points);
        return points;
    }
    void SpawnPoint()
    {
        x = Random.Range(-GameManager.Instance.fieldWidth, GameManager.Instance.fieldWidth);
        y = Random.Range(-GameManager.Instance.fieldHeight, GameManager.Instance.fieldHeight);
        meshRenderer.material = score == 5 ? fivePointColor : tenPointColor;
        meshFilter.mesh = score == 5 ? fivePointMesh : tenPointMesh;
        transform.position = new Vector3(x, y, z);
    }
}
