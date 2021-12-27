using SoulsLike;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerScanner 
{
    public float detectionRadius = 8.0f;
    public float detectionAngle = 140.0f;

    public PlayerKontrol Detect(Transform detector)
    {

        if (PlayerKontrol.Instance == null)
        {
            return null;
        }

        //aralarýndaki mesafe için
        
        Vector3 toPlayer = PlayerKontrol.Instance.transform.position - detector.position;
        toPlayer.y = 0;

        if (toPlayer.magnitude <= detectionRadius)
        {
            //Debug.Log("Detecting the player!");

            if (Vector3.Dot(toPlayer.normalized, detector.forward) >
               Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
            {
                Debug.Log("Player Has been detected!!!!");
                return PlayerKontrol.Instance;
            }
        }
        /*else
        {
           // Debug.Log("Where are you?");

        }
        */
        return null;
    }

}
