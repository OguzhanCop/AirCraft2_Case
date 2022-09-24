using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAngleCheck : MonoBehaviour
{
    Renderer targetParentColor;
    Renderer targetChildColor1;
    Renderer targetChildColor2;
    bool angleControllFirstObject = false;
    public GameData gameDate;
    void Start()
    {
        targetParentColor = this.GetComponent<Renderer>();
        targetChildColor1 = this.transform.GetChild(0).GetComponent<Renderer>();
        targetChildColor2 = this.transform.GetChild(1).GetComponent<Renderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "angle")
        {
            angleControllFirstObject = true;
        }

        if (other.gameObject.tag == "angle2" && angleControllFirstObject)
        {
            targetParentColor.material.color = Color.green;
            targetChildColor1.material.color = Color.green;
            targetChildColor2.material.color = Color.green;
            gameDate.planeAngleCheck = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "angle" || other.gameObject.tag == "angle2")
        {
            targetParentColor.material.color = Color.red;
            targetChildColor1.material.color = Color.red;
            targetChildColor2.material.color = Color.red;
            angleControllFirstObject = false;
            gameDate.planeAngleCheck = false;
        }
    }
}
