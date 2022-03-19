using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SoulsLike{
    public class UniqueID : MonoBehaviour
    {

        [SerializeField]
        private string uid = Guid.NewGuid().ToString();
        
        public string Uid { get { return uid; }}
    }

}
