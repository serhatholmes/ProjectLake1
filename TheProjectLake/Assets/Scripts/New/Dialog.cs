using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{   
    [System.Serializable]
    public class DialogAnswer
        {
           [TextArea(3,15)]  public string text;
            public bool isForceDialogQuit;
            public string questID;

        }
    [System.Serializable]
     public class DialogQuery
        {
           [TextArea(3,15)]  public string text;
            public DialogAnswer answer;
            public bool isAsked;
            public bool isAlwaysAsked;

        } 
    
    [System.Serializable]
    public class Dialog
    {

        [TextArea(3,15)]  public string welcomeText;
          public DialogQuery[] queries;

    }
    
}