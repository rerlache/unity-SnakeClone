using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    int x = 10;
    int y = 0;
    int z = 0;
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Head")
        {
            x = Random.Range(-19, 19);
            y = Random.Range(-10, 10);
            transform.position = new Vector3(x, y, z);
            Player.PlayerInstance.StretchSnakeAfterEating();
            Player.PlayerInstance.Score += 5;
            Debug.Log("Score: " + Player.PlayerInstance.Score);
        }
    }

}
