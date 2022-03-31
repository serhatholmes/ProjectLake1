using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoulsLike
{
    public class DialogManager : MonoBehaviour
    {
        public GameObject dialogUI;
        public Text dialogHeaderText;
        public Text dialogWelcomeText;

        public float maxDialogDistance=2.4f;

        private PlayerInput mPlayer;
        private QuestGiver mNpc;
        private Dialog mActiveDialog;

        public bool HasActiveDialog {get { return mActiveDialog != null;}}

        public float DialogDistance 
            { 
                get {
                     return  Vector3.Distance(
                    mPlayer.transform.position,
                    mNpc.transform.position);
            }
        }

        private void Start() 
        {
            mPlayer = PlayerInput.Instance;
            //Debug.Log("calis");
        }

       
        private void Update() {
            
            if(!HasActiveDialog && mPlayer != null && mPlayer.OptionClickTarget != null)
            {
                Debug.Log(mPlayer.OptionClickTarget.tag);
                if(mPlayer.OptionClickTarget.CompareTag("QuestGiver"))
                {
                    
                    mNpc = mPlayer.OptionClickTarget.GetComponent<QuestGiver>();

                   

                    if(DialogDistance < maxDialogDistance)
                    {
                        
                        StartDialog();
                    }
                
                }
            }

            if(HasActiveDialog && DialogDistance > maxDialogDistance + 1.0f)
            {
                StopDialog();
            }
        }

        private void StartDialog()
        {
            mActiveDialog = mNpc.dialog;
            dialogUI.SetActive(true);
            dialogHeaderText.text = mNpc.name;
        }

        private void StopDialog()
        {
            mNpc = null;
            mActiveDialog = null;
            dialogUI.SetActive(false);
        }

        
    }
}
