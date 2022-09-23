using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager 
{

    public static Action<float> planeHorizontalControll;
    public static Action<float> planeVerticalControll;
    public static Action planeHorizontalBaseRotation;
    public static Action planeVerticalBaseRotation;
    public static Action<float> speedControll;
}
