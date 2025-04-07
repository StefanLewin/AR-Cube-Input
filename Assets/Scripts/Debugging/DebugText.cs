using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vuforia;

namespace Debugging
{
    public class DebugText : MonoBehaviour
    {
        [SerializeField] private CubeManagement CubeManagement;
        [SerializeField] private TextMeshProUGUI InputFeedback;
        
        private TextMeshProUGUI _debugText;
        private Camera _camera;
        private List<Vector3> _distances;
        private bool anyTargetTracked = false;
        private bool isOnCooldown = false;
        private int nearestButton = 0;



        private void Awake()
        {
            _debugText = GetComponent<TextMeshProUGUI>();
            _camera = Camera.main;
            _distances = new List<Vector3>();
        }
        
        private void Start()
        {
            ResetDistances();
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfTracked();

            if (_distances.Count > 0 && _distances[nearestButton].magnitude < 0.8f && anyTargetTracked && !isOnCooldown)
            {
                InputFeedback.text = $"Input detected on {CubeManagement.buttons[nearestButton].GetComponentInChildren<TextMeshProUGUI>().text}!";
                StartCoroutine(Cooldown());
            }
            
            Vector3 cameraPosition = _camera.transform.position;
            
            for (int i = 0; i < CubeManagement.buttons.Count; i++)
            {
                _distances[i] = CubeManagement.buttons[i].transform.position - cameraPosition;
            }

            UpdateText();
            HighlightButton();
        }

        public void ResetDistances()
        {
            _distances.Clear();

            for (int i = 0; i < CubeManagement.buttons.Count(); i++)
            {
                _distances.Add(new Vector3(0,0,0));
            }
        }
        
        
        IEnumerator Cooldown()
        {
            isOnCooldown = true;
            yield return new WaitForSeconds(2f); // wait for 2 seconds
            InputFeedback.text = "Ready for Input";
            isOnCooldown = false;
        }

        void CheckIfTracked()
        {
            foreach (var trackable  in CubeManagement.targets)
            {
                if (trackable.TargetStatus.Status ==  Status.TRACKED)
                {
                    anyTargetTracked = true;
                    break; // No need to keep checking if we found one
                }

                anyTargetTracked = false;
            }
        }

        void UpdateText()
        {
            string output = "";

            /*
            for (int i = 0; i < _distances.Count; i++)
            {
                output += $"Button {i + 1}: {_distances[i].magnitude}\n";
            }*/

            output += $"Distances Count: {_distances.Count}\n";
            output += $"Buttons Count: {CubeManagement.buttons.Count}\n";

            _debugText.text = output;
        }

        void HighlightButton()
        {
            float lowestDistance = 100;

            for (int i = 0; i < CubeManagement.buttons.Count; i++)
            {
                if (_distances[i].magnitude+0.01f < lowestDistance)
                {
                    nearestButton = i;
                    lowestDistance = _distances[i].magnitude;
                }
            }
            
            if(nearestButton <= CubeManagement.buttons.Count() && CubeManagement.buttons.Count != 0)
                EventSystem.current.SetSelectedGameObject(CubeManagement.buttons[nearestButton].gameObject);
        }
    }
}
