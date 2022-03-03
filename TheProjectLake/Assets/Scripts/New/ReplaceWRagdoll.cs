using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{

    public class ReplaceWRagdoll : MonoBehaviour
    {
        public GameObject ragdollPrefab;


        public void Replace()
        {
            // Debug.Log("replace");

            GameObject ragdollInstance = Instantiate(ragdollPrefab, transform.position, transform.rotation);

            Destroy(gameObject);
        }

    }

}