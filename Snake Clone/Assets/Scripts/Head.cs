using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public float xTurnPosition;
    public float yTurnPosition;
    void Update()
    {
        if(Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0){
            yTurnPosition = transform.position.y;
        }
        if(Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0){
            xTurnPosition = transform.position.x;
        }
    }
}
