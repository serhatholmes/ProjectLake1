using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike {

    public enum QuestTip
    {
        HUNT,
        GATHER,
        TALK,
        EXPLORE
    }
    [System.Serializable]
    public class Quest1 : MonoBehaviour
    {
        public string uid;
        public string title;
        public string descript;
        public int exp;
        public int gold;

        public int amount;
        public string[] targets;
        public string talkTo;
        public Vector3 explore;
        public string questOwnr;
        public QuestTip type;

    }

}

