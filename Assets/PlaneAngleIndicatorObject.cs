using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAngleIndicatorObject : MonoBehaviour
{
    [SerializeField] List<GameObject> targets;
    [SerializeField] GameData gameData;
    private void OnEnable()
    {
        EventManager.planeRotationIndicator += PlaneplaneRotationIndicator;
        EventManager.planeRotationIndicatorBaseAngle += PlaneRotationIndicatorBaseAngle;
    }
    private void OnDisable()
    {
        EventManager.planeRotationIndicator -= PlaneplaneRotationIndicator;
        EventManager.planeRotationIndicatorBaseAngle -= PlaneRotationIndicatorBaseAngle;
    }

    void PlaneplaneRotationIndicator(float joystickHorizontalValue)
    {
        transform.Rotate(0,0, -joystickHorizontalValue * Time.deltaTime * 30, Space.Self);


    }
    void PlaneRotationIndicatorBaseAngle()
    {
        transform.rotation =
             Quaternion.Slerp(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0), 1f * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "plane")
        {
            gameData.targetCounter++;
            transform.position = targets[gameData.targetCounter].transform.position;
            if (gameData.planeAngleCheck)
            {
                gameData.score += 10;
                EventManager.scoreChange.Invoke();
            }
            else
            {
                gameData.score -= 10;
                EventManager.scoreChange.Invoke();
            }

        }
    }
}
