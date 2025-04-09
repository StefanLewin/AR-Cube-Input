using System;
using System.Collections.Generic;
using SO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

namespace ARCube
{
    /// <summary>
    /// This script is responsible for creating the Image Target Behaviours and communicating to other components,
    /// that all cube sides and buttons are initiated.
    /// </summary>
    public class CubeManagement : MonoBehaviour
    {
        public static CubeManagement Instance { get; private set; }
        
        [SerializeField] private CubeConfig m_config;
        [SerializeField] private GameObject m_cubeCanvasPrefab;
        
        [HideInInspector] public List<ImageTargetBehaviour> m_targets;
        
        public event EventHandler OnSidesCreated; 

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        
            Instance = this;
            m_targets = new List<ImageTargetBehaviour>();
        }
        
        private void OnEnable()
        {
            VuforiaApplication.Instance.OnVuforiaStarted += CreateCubeSides;
        }

        private void CreateCubeSides()
        {
            foreach (var side in m_config.m_cubeSides)
            {
                //Create Image Target
                var mImageTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(side.m_targetImage, 0.2f, side.m_targetName);

                mImageTarget.gameObject.transform.SetParent(this.transform);
                var canvas = Instantiate(m_cubeCanvasPrefab, mImageTarget.transform);
                
                //Create Event Handler
                var eventHandler = mImageTarget.gameObject.AddComponent<DefaultObserverEventHandler>();
                eventHandler.OnTargetFound.AddListener(() =>
                {
                    ProximityInputProvider.Instance.m_currentCubeSide = canvas;
                });
                eventHandler.OnTargetLost.AddListener(() =>
                {
                    ProximityInputProvider.Instance.m_currentCubeSide = null;
                });
                eventHandler.StatusFilter = DefaultObserverEventHandler.TrackingStatusFilter.Tracked;
                eventHandler.UsePoseSmoothing = true;

                //Assign color to sube side
                canvas.GetComponent<CubeUIProvider>().BackgroundImage.color = side.m_buttonColor;

                //Assign text to buttons.
                for (int i = 0; i < 4; i++)
                {
                    var buttonText = canvas.GetComponent<CubeUIProvider>().Buttons[i]
                        .GetComponentInChildren<TextMeshProUGUI>();
                    
                    buttonText.text = side.m_buttonNames[i];
                    canvas.GetComponent<CubeUIProvider>().Buttons[i].name = side.m_buttonNames[i];
                }
                m_targets.Add(mImageTarget);
            }
            OnSidesCreated?.Invoke(this, EventArgs.Empty);
        }
    }
}
