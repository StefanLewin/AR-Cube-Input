using System;
using TMPro;
using UnityEngine;

namespace ARCube
{
    //This script provides methods to alter the feedback text on the bottom side of the app. 
    public class FeedbackHandler : MonoBehaviour
    {
        public static FeedbackHandler Instance { get; private set; }
        private TextMeshProUGUI _feedbackText;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        
            Instance = this;
        }

        private void Start()
        {
            _feedbackText = GetComponent<TextMeshProUGUI>();
        }

        public void SetFeedback(string feedback)
        {
            _feedbackText.text = feedback;
        }
    }
}