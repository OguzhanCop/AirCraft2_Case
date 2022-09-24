using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float speedMultiplierFactor;
    private float speed;
    public GameData gameData;
    private void OnEnable()
    {
        EventManager.planeHorizontalControll += PlaneHorizontalControll;
        EventManager.planeVerticalControll += PlaneVerticalControll;
        EventManager.planeHorizontalBaseRotation += PlaneHorizontalBaseRotation;
        EventManager.planeVerticalBaseRotation += PlaneVerticalBaseRotation;
       EventManager.speedControll += SpeedControll;
    }
   
    private void OnDisable()
    {
        EventManager.planeHorizontalControll -= PlaneHorizontalControll;
        EventManager.planeVerticalControll -= PlaneVerticalControll;
        EventManager.planeHorizontalBaseRotation -= PlaneHorizontalBaseRotation;
        EventManager.planeVerticalBaseRotation -= PlaneVerticalBaseRotation;
       EventManager.speedControll -= SpeedControll;
    }
    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

  
    void PlaneHorizontalControll(float joystickHorizontalValue)
    {

        transform.Rotate(0, 0, -joystickHorizontalValue * Time.deltaTime * 30, Space.Self);
        transform.Rotate(0, joystickHorizontalValue * Time.deltaTime * 50, 0, Space.World);

    }
    void PlaneVerticalControll(float joystickVerticalValue)
    {
        transform.Rotate(-joystickVerticalValue * Time.deltaTime * 30, 0, 0, Space.Self);


    }
    void PlaneHorizontalBaseRotation()
    {
        transform.rotation =
             Quaternion.Slerp(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0), 1f * Time.deltaTime);
    }
    void PlaneVerticalBaseRotation()
    {
        transform.rotation =
               Quaternion.Slerp(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), 1f * Time.deltaTime);
    }
    void SpeedControll(float accerelatorValue)
    {
        if (rb.velocity.magnitude <= accerelatorValue * 50)
        {

            speed += accerelatorValue * speedMultiplierFactor * Time.deltaTime;
            gameData.speed = (int)speed * 5;
            rb.velocity = transform.forward * speed;
            if (gameData.speed > 70)
            {
                gameData.planeFlying = true;
            }
            
        }
        else
        {
            if (rb.velocity.magnitude >= 1)
            {
                speed -= (1 - accerelatorValue) * speedMultiplierFactor / 2 * Time.deltaTime;
                gameData.speed = (int)speed * 4;
                rb.velocity = transform.forward * speed;
            }
            else
            {
                rb.velocity = transform.forward * 0;
            }
        }
    }
  

}
