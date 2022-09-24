using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneExplosion : MonoBehaviour
{
    public ParticleSystem explosionPartickle;
    public GameData gameData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "environment" && gameData.planeFlying)
        {
            
            //gameObject.transform.parent.gameObject.SetActive(false);
            explosionPartickle.Play();
            Destroy(gameObject.transform.parent.gameObject);
            Debug.Log("oldu");
        }
        Debug.Log("oldu");
    }
}
