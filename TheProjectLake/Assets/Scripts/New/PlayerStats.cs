using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SoulsLike
{
    public class PlayerStats : MonoBehaviour, iMessageReceiver
    {
        public int maxLevel;
        public int currentLevel;
        public int currentExp;
        public int[] availableLevels;

        private void Awake() {

            availableLevels = new int[maxLevel];
            ComputeLevels(maxLevel);
        }
  
        private void ComputeLevels(int levelCount)
        {
            for(int i=0; i<levelCount; i++)
            {
                var level = i +1;
                var levelPow = Mathf.Pow(level,2);
                var expToLevel = Convert.ToInt32(levelPow * levelCount);

                availableLevels[i] = expToLevel;
            }
        }

         public void OnReceiveMessage(MessageType type, object sender,object msg)
        {
            if(type == MessageType.DEAD)
            {
                var exp = (sender as Damageable).experince;
                GainExperience(exp);
            }
                   
            
        }

        public void GainExperience(int exp)
        {
            Debug.Log("gaining"+exp);
        }

       
    }
}
