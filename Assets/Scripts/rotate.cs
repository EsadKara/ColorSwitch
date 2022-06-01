using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
