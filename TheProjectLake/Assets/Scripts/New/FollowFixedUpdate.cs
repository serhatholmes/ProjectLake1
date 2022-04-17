using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SoulsLike
{


public class FollowFixedUpdate : MonoBehaviour
{
        public Transform toFollow;

   void FixedUpdate()
    {
            if(toFollow==null)
            {
                    return;
            }
            transform.position = toFollow.position;
            transform.rotation = toFollow.rotation;
    }

    public void SetFollowe(Transform followe)
    {
            toFollow= followe;
    }
}

}
