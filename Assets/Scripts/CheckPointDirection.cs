using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPointDirection : MonoBehaviour
{
    public GameObject plane;
    public GameObject checkPoint;
    float distance;
    public TextMeshProUGUI warninMasage;
    int countDownSecond;
    bool warninMasageActive;
    Coroutine countDown;


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
            
            if (!warninMasageActive)
            {
                countDownSecond = 10;
                countDown = StartCoroutine(CountDown());
                warninMasageActive = true;
            }
            
        }
        else
        {
            warninMasageActive = false;
            transform.GetChild(0).gameObject.SetActive(false);
            if(countDown!=null)
            StopCoroutine(countDown);
        }
    }
            
    
    IEnumerator CountDown()
    {
       while(countDownSecond >= 0)
        {
            
            warninMasage.text = "WARNING, RETURN TO MISSION IN " + countDownSecond + " " + " SECONDS";
            countDownSecond--;
            yield return new WaitForSecondsRealtime(1f);
        }
        EventManager.losePanel.Invoke();
       
    }
}
