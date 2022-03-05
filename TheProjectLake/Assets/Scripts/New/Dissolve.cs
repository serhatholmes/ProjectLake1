using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public class Dissolve : MonoBehaviour
    {

        public float dissolveTime = 5.0f;

        private void Awake()
        {
            dissolveTime += Time.time;
        }

        // Update is called once per frame
        void Update()
        {

            if(Time.time>=dissolveTime)
            {
                Destroy(gameObject);
            }
        }
    }


}