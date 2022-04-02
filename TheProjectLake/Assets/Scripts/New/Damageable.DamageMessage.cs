using System.Collections;
using UnityEngine;

namespace SoulsLike
{
    public partial class Damageable : MonoBehaviour
    {

        public struct DamageMessage
        {
           
            public MonoBehaviour damager;
            public int amount;
            public GameObject damageSource;
        }
    }
}