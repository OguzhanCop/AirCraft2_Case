using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManeger : MonoBehaviour
{
    public FixedJoystick joystick;
    public Slider accerelator;
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        JoystickControll();
        AccerelationSlider();
    }

    void JoystickControll()
    {
        if (joystick.Horizontal != 0)
        {
            EventManager.planeHorizontalControll.Invoke(joystick.Horizontal);

        }
        else
        {
            EventManager.planeHorizontalBaseRotation.Invoke();
        }
        if (joystick.Vertical != 0)
        {
            EventManager.planeVerticalControll.Invoke(joystick.Vertical);

        }
        else
        {
            EventManager.planeVerticalBaseRotation.Invoke();
        }

    }
    void AccerelationSlider()
    {
        EventManager.speedControll.Invoke(accerelator.value);

    }
}
