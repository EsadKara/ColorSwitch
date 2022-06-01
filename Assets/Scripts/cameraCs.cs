using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCs : MonoBehaviour
{
    Transform ball;
    void Start()
    {
        ball = GameObject.Find("Ball").transform;
    }
    void LateUpdate()
    {
        if (ball.position.y > transform.position.y - 3)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(ball.position.x, ball.position.y + 3, -10), 3f*Time.deltaTime);
        }

    }
}
