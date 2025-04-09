using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Vuforia;

namespace ARCube
{
    //This app triggers an action, when a certain minimum distance between the camera and the cubes's side has been reached.
    public class ProximityInputProvider : MonoBehaviour
    {
        [HideInInspector] public GameObject m_currentCubeSide;
        private Camera _camera;
        private bool _cooldownActive = false;
        
        
        public static ProximityInputProvider Instance { get; private set; }

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
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_cooldownActive || !m_currentCubeSide || !CheckIfTracked()) return;
            
            var distanceToCamera = Vector3.Distance(m_currentCubeSide.transform.position, _camera.transform.position);
            if (!(distanceToCamera < 0.8f)) return;
            
            FeedbackHandler.Instance.SetFeedback($"Input detected on {m_currentCubeSide.GetComponent<ButtonHighlighter>().m_nearestButton.name}!");
            StartCoroutine(Cooldown());
        }

        IEnumerator Cooldown()
        {
            _cooldownActive = true;
            yield return new WaitForSeconds(2f); 
            FeedbackHandler.Instance.SetFeedback("Ready for Input");
            _cooldownActive = false;
        }
        
        private bool CheckIfTracked()
        {
            foreach (var trackable  in CubeManagement.Instance.m_targets)
            {
                if (trackable.TargetStatus.Status ==  Status.TRACKED)
                {
                    return true;
                }
            }
            return false;
        }
    }
}