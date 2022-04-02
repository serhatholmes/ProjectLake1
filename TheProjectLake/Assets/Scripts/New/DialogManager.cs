using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace SoulsLike
{
    public class DialogManager : MonoBehaviour
    {
        public float timeToShowOptions = 2.0f;
        public GameObject dialogUI;
        public Text dialogHeaderText;
        public Text dialogAnswerText;

        public float maxDialogDistance=2.4f;

        public GameObject dialogOptionList;
        public Button dialogOptionPrefab;

        private PlayerInput mPlayer;
        private QuestGiver mNpc;
        private Dialog mActiveDialog;
        private float mOptionTopPosition;

        private float mTimerToShowOptions;
        private bool mForceDialogQu

        const float cDistanceBetweenOption = 34.0f;

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
            
            if(!HasActiveDialog && mPlayer.OptionClickTarget != null)
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

            if(mTimerToShowOptions > 0)
            {
                mTimerToShowOptions += Time.deltaTime;
                if(mTimerToShowOptions >= timeToShowOptions)
                {
                    mTimerToShowOptions = 0;
                    DisplayDialogOptions();
                }
            }
        }

        private void StartDialog()
        {
            mActiveDialog = mNpc.dialog;
            
            dialogHeaderText.text = mNpc.name;
            dialogUI.SetActive(true);
            ClearDialogOptions();
            DisplayAnswerText(mActiveDialog.welcomeText);
            TriggerDialogOptions();
     
          
        }

        private void DisplayDialogOptions()
        {
            HideAnswerText();
            CreateDialogMenu();
        }

        private void TriggerDialogOptions()
        {
            mTimerToShowOptions = 0.001f;
        }

        private void DisplayAnswerText(string answerText)
        {
            dialogAnswerText.gameObject.SetActive(true);
            dialogAnswerText.text = answerText;
        }

         private void HideAnswerText()
        {
            dialogAnswerText.gameObject.SetActive(false);
        }

        private void CreateDialogMenu()
        {
            mOptionTopPosition = 0;
            var queries = Array.FindAll(mActiveDialog.queries, query=> !query.isAsked);
        
            foreach (var query in queries)
            {
                mOptionTopPosition += cDistanceBetweenOption;
               var dialogOption = CreateDialogOption(query.text);
               RegisterOptionClickHandler(dialogOption, query);
            }
        }

        private Button CreateDialogOption(string optionText)
        {
            Button buttonInstance = Instantiate(dialogOptionPrefab,dialogOptionList.transform);
            buttonInstance.GetComponentInChildren<Text>().text = optionText;

            RectTransform rt = buttonInstance.GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, mOptionTopPosition,rt.rect.height);

            return buttonInstance;
        }

        private void RegisterOptionClickHandler(Button dialogOption, DialogQuery query)
        {
            EventTrigger trigger = dialogOption.gameObject.AddComponent<EventTrigger>();
            var pointerDown = new EventTrigger.Entry{

                eventID = EventTriggerType.PointerDown
            };
            
            pointerDown.callback.AddListener((e) => {

                if(!query.isAlwaysAsked)
                {
                    query.isAsked = true;
                }

                ClearDialogOptions();
                DisplayAnswerText(query.answer.text);
                TriggerDialogOptions();
            }
            );

            trigger.triggers.Add(pointerDown);
        }

        private void StopDialog()
        {
            mNpc = null;
            mActiveDialog = null;
            mTimerToShowOptions = 0;
            dialogUI.SetActive(false);
        }

        private void ClearDialogOptions()
        {
            foreach(Transform child in dialogOptionList.transform)
            {
                Destroy(child.gameObject);
            }
        }

        
    }
}
