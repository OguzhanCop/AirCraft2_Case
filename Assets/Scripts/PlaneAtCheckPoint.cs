using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlaneAtCheckPoint : MonoBehaviour
{
    [SerializeField] List<GameObject> checkPoints;
    [SerializeField] GameData gameData;
    public GameObject fnishCamera;
    public ParticleSystem coinParticleSystem;
    public ParticleSystem confettiParticleSystem;
    public ParticleSystem confetti2ParticleSystem;


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
            gameData.checkPointCounter++;
            if (gameData.checkPointCounter < checkPoints.Count)
            {
                transform.position = checkPoints[gameData.checkPointCounter].transform.position;
                transform.rotation = Quaternion.Euler(transform.rotation.x, checkPoints[gameData.checkPointCounter].transform.rotation.eulerAngles.y-90, transform.rotation.z);
               
                if (transform.rotation.eulerAngles.y <= 270 && transform.rotation.eulerAngles.y >0)
                {
                   transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.eulerAngles.y - 180, transform.rotation.z);
                }
                if (gameData.planeAngleCheck)
                {
                    gameData.score += 10;
                    coinParticleSystem.Play();
                    EventManager.scoreChange.Invoke();
                }
                else
                {
                    gameData.score -= 10;
                    EventManager.scoreChange.Invoke();
                }
            }
            else
            {
                fnishCamera.GetComponent<CinemachineVirtualCamera>().Follow = null;
                fnishCamera.GetComponent<CinemachineVirtualCamera>().Priority = 20;
                confettiParticleSystem.Play();
                confetti2ParticleSystem.Play();
                EventManager.winPanel.Invoke();
            }
           

        }
    }
   
}
