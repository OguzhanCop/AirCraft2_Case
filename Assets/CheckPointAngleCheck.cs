using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointAngleCheck : MonoBehaviour
{
    Renderer CheckPointParentColor;
    Renderer CheckPointChildColor1;
    Renderer CheckPointChildColor2;
    bool angleControllFirstObject = false;
    public GameData gameDate;
    void Start()
    {
        CheckPointParentColor = this.GetComponent<Renderer>();
        CheckPointChildColor1 = this.transform.GetChild(0).GetComponent<Renderer>();
        CheckPointChildColor2 = this.transform.GetChild(1).GetComponent<Renderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "angle")
        {
            angleControllFirstObject = true;
        }

        if (other.gameObject.tag == "angle2" && angleControllFirstObject)
        {
            CheckPointParentColor.material.color = Color.green;
            CheckPointChildColor1.material.color = Color.green;
            CheckPointChildColor2.material.color = Color.green;
            gameDate.planeAngleCheck = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "angle" || other.gameObject.tag == "angle2")
        {
            CheckPointParentColor.material.color = Color.red;
            CheckPointChildColor1.material.color = Color.red;
            CheckPointChildColor2.material.color = Color.red;
            angleControllFirstObject = false;
            gameDate.planeAngleCheck = false;
        }
    }
}
