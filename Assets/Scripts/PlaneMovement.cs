using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaneMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float speedMultiplierFactor;
    private float speed;
    private bool planeTakeOff=false;
    private bool planeFalling=false;
    public ParticleSystem jetEffect1;
    public ParticleSystem jetEffect2;
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
    void SpeedControll(float accerelatorValue )
    {
        var jetEffect1Setting = jetEffect1.main;
        var jetEffect2Setting = jetEffect2.main;
        if (rb.velocity.magnitude <= accerelatorValue * 50 )
        {

            speed += accerelatorValue * speedMultiplierFactor * Time.deltaTime;
            jetEffect1Setting.startLifetime= accerelatorValue * 2; 
            jetEffect2Setting.startLifetime = accerelatorValue * 2;
            gameData.speed = (int)speed * 5;
            rb.velocity = transform.forward * speed;

            if (gameData.speed > 130 && !planeTakeOff)                
                StartCoroutine(PlaneFlying());
            if (gameData.speed > 80 && planeFalling)
            {
                planeFalling = false;
                gameData.planeFlying = true;
            }

        }
        else
        {
            if (rb.velocity.magnitude >= 1 && !planeFalling)
            {
                speed -= (1 - accerelatorValue) * speedMultiplierFactor / 2 * Time.deltaTime;
                gameData.speed = (int)speed * 4;
                jetEffect1Setting.startLifetime = accerelatorValue * 2;
                jetEffect2Setting.startLifetime = accerelatorValue * 2;
                rb.velocity = transform.forward * speed;
                if (gameData.speed < 40 && planeTakeOff)
                {
                    planeFalling = true;
                    gameData.planeFlying = false;
                    transform.DORotateQuaternion(Quaternion.Euler(70, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), 8f);
                    
                }
                    
            }
            else
            {
                if(!planeFalling)
                rb.velocity = transform.forward * 0;
                else
                {
                    rb.velocity = transform.forward * 30;
                   
                }
            }
        }

    }
   
    
   
    IEnumerator PlaneFlying()
    {
        transform.Rotate(-10, 0, 0, Space.Self);
        planeTakeOff = true;
        yield return new WaitForSecondsRealtime(2f);
        gameData.planeFlying = true;
    }
  

}
