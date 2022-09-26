using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointAngleCheck : MonoBehaviour
{
    Renderer checkPointParentColor;
    Renderer checkPointChildColor1;
    Renderer checkPointChildColor2;
    bool angleControllFirstObject = false;
    public GameData gameDate;
    void Start()
    {
        checkPointParentColor = this.GetComponent<Renderer>();
        checkPointChildColor1 = this.transform.GetChild(0).GetComponent<Renderer>();
        checkPointChildColor2 = this.transform.GetChild(1).GetComponent<Renderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "angle")
        {
            angleControllFirstObject = true;
        }

        if (other.gameObject.tag == "angle2" && angleControllFirstObject)
        {
            checkPointParentColor.material.color = Color.green;
            checkPointChildColor1.material.color = Color.green;
            checkPointChildColor2.material.color = Color.green;
            gameDate.planeAngleCheck = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "angle" || other.gameObject.tag == "angle2")
        {
            checkPointParentColor.material.color = Color.red;
            checkPointChildColor1.material.color = Color.red;
            checkPointChildColor2.material.color = Color.red;
            angleControllFirstObject = false;
            gameDate.planeAngleCheck = false;
        }
    }
}
