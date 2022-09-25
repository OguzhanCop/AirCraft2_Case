using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointDirection : MonoBehaviour
{
    public GameObject plane;
    public GameObject checkPoint;
    float distance;

    void Update()
    {
        DirectionTarget();

    }
    void DirectionTarget()
    {
        distance = Vector3.Distance(plane.transform.position, checkPoint.transform.position);
        if (distance > 550)
        {
            transform.GetChild(0).gameObject.SetActive(true);
           transform.LookAt(checkPoint.transform.position);
        }
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }
}
