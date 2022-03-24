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

        private bool mHasActiveDialog;

        private void Awake() {
            
            dialogUI.SetActive(false);
        }
        private void Update() {
            
            if(!mHasActiveDialog && PlayerInput.Instance != null && PlayerInput.Instance.OptionClickTarget != null)
            {
                if(PlayerInput.Instance.OptionClickTarget.CompareTag("QuestGiver"))
                {
                    var distanceToTarget = Vector3.Distance(
                    PlayerInput.Instance.transform.position,
                    PlayerInput.Instance.OptionClickTarget.transform.position
                    );

                    if(distanceToTarget < 2.4f)
                    {
                        StartDialog();
                    }
                
                }
            }

            closePanel();
        }

        private void StartDialog()
        {
            mHasActiveDialog = true;
            dialogUI.SetActive(true);
            dialogHeaderText.text = "Hello there, protagonist! Are you new here?";
        }

        public void closePanel()
        {
           if(Input.GetButtonDown("Submit"))
           {
                dialogUI.SetActive(false);
           }
           
        }
    }
}
