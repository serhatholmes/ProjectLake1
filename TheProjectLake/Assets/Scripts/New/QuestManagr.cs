using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace SoulsLike
{

    public class JsonHelper{

        private class Wrapper<T>
        {
            public T[] array;
        }
        public static T[] getJsonArray<T>(string json)
        {
            string newJson = "{ \"array\": " + json +"}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;


        }
    }

    public class QuestManagr : MonoBehaviour
    {
        public Quest1[] quests;

        private void Awake()
        {
            LoadQuestsFromDB();


        }

        private void LoadQuestsFromDB()
        {
            using (StreamReader reader= new StreamReader("Assets/DB/QuestDB.json"))
            {
                string json = reader.ReadToEnd();
                var loadedQuests = JsonHelper.getJsonArray<Quest1>(json);
                quests = new Quest1[loadedQuests.Length];
                quests = loadedQuests;
                
            }
        }

    }

}
