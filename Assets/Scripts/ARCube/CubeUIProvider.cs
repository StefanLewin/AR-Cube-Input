using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARCube
{
    //This script provides the UI elements of a cubes' side.
    public class CubeUIProvider : MonoBehaviour
    {
        public List<Button> Buttons { get; private set; }
        public Image BackgroundImage { get; private set; }

        private void Awake()
        {
            Buttons = new List<Button>();
            var buttonContainer = transform.GetChild(0);

            foreach (Transform button in buttonContainer)
            {
                Buttons.Add(button.GetComponent<Button>());
            }
            
            BackgroundImage = transform.GetChild(0).GetComponent<Image>();
        }
    
    

    }
}
