using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CubeManagement : MonoBehaviour
{
    public static CubeManagement Instance { get; private set; }
    
    [SerializeField] public ImageTargetBehaviour[] targets;
    public List<Button> buttons;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        buttons = new List<Button>();
    }
    
}
