using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ARCube
{
    //This script handles button highlighting, based on the relative position and rotation to the camera.
    public class ButtonHighlighter : MonoBehaviour
    {
        public Button m_nearestButton;
        
        private List<Button> Buttons { get; set; }
        private List<Vector3> _distances;
        private Camera _camera;
        private int _nearestButton = 0;
        private bool _buttonsInitialized = false;

        private void Awake()
        {
            _camera = Camera.main;
            _distances = new List<Vector3>();
        }

        private void OnEnable()
        {
            CubeManagement.Instance.OnSidesCreated += SetButtons;
        }

        private void Update()
        {
            if (!_buttonsInitialized) return;
            
            UpdateDistances();
            HighlightButton();
        }

        private void SetButtons(object sender, EventArgs e)
        {
            Buttons = GetComponent<CubeUIProvider>().Buttons;
            ResetDistances();
            _buttonsInitialized = true;
        }
        
        private void ResetDistances()
        {
            _distances.Clear();

            for (int i = 0; i < Buttons.Count(); i++)
            {
                _distances.Add(new Vector3(0,0,0));
            }
        }

        private void UpdateDistances()
        {
            Vector3 cameraPosition = _camera.transform.position;
            
            for (var i = 0; i < Buttons.Count; i++)
            {
                _distances[i] = Buttons[i].transform.position - cameraPosition;
            }
        }
        
        private void HighlightButton()
        {
            var lowestDistance = float.MaxValue;

            for (var i = 0; i < Buttons.Count; i++)
            {
                if (!(_distances[i].magnitude + 0.01f < lowestDistance)) continue;
                _nearestButton = i;
                lowestDistance = _distances[i].magnitude;
                m_nearestButton = Buttons[i];
            }
            
            if(_nearestButton <= Buttons.Count() && Buttons.Count != 0)
                EventSystem.current.SetSelectedGameObject(Buttons[_nearestButton].gameObject);
        }
    }
}