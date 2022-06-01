using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleCs : MonoBehaviour
{
    public float turnSeed;

    float distance;
    int random;

    gameManager gM;
    GameObject ball;

    private void Start()
    {
        gM = GameObject.Find("GameManager").GetComponent<gameManager>();
        ball = GameObject.Find("Ball");
        random = Random.Range(0, 2);
    }
    void Update()
    {
        if (random == 0)
            transform.Rotate(0, 0, -turnSeed * Time.deltaTime);
        else
            transform.Rotate(0, 0, turnSeed * Time.deltaTime);

        distance = Vector2.Distance(transform.position, ball.transform.position);
        if (distance >= 15 && !gM.isOver)
        {
            Destroy(gameObject);
        }

    }
}
