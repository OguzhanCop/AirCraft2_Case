using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlaneExplosion : MonoBehaviour
{
    public ParticleSystem explosionPartickle;
    public GameObject mainCamera;
    public GameData gameData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "environment" )
        {

            gameObject.transform.parent.gameObject.SetActive(false);
            explosionPartickle.Play();
            EventManager.losePanel.Invoke();
            mainCamera.GetComponent<CinemachineVirtualCamera>().Follow = null;
            mainCamera.GetComponent<CinemachineVirtualCamera>().LookAt = null;
           


        }
        
    }
  
}
