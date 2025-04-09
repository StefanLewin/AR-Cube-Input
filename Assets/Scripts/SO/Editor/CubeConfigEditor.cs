using UnityEngine;
using UnityEditor;

namespace SO
{
    [CustomEditor(typeof(CubeConfig))]
    public class CubeConfigEditor : Editor
    {
        private readonly string[][] _buttonNameSets = new string[][]
        {
            new string[] { "Play", "Pause", "Back", "Forward" },
            new string[] { "Yes", "No", "Maybe", "Cancel" },
            new string[] { "+1", "-1", "+10", "-10" },
            new string[] { "Save", "Load", "Quit", "Retry" },
            new string[] { "Run", "Walk", "Jump", "Crouch" },
            new string[] { "Up", "Down", "Left", "Right" }
        };

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);
            
            CubeConfig cubeConfig = (CubeConfig)target;

            if (GUILayout.Button("Fill button names with default names"))
            {
                FillButtonNamesWithRandomSets(cubeConfig);
            }
            
            GUILayout.Space(10);
            
            if (GUILayout.Button("Fill cube sides with random pastel colors"))
            {
                ApplyRandomPastelColors(cubeConfig);
            }
        }

        private void FillButtonNamesWithRandomSets(CubeConfig cubeConfig)
        {
            for (int i = 0; i < cubeConfig.m_cubeSides.Length; i++)
            {
                cubeConfig.m_cubeSides[i].m_buttonNames = i < _buttonNameSets.Length ? _buttonNameSets[i] : _buttonNameSets[i % _buttonNameSets.Length];
            }

            EditorUtility.SetDirty(cubeConfig);
            Debug.Log("Button names filled with default names.");
        }
        
        private void ApplyRandomPastelColors(CubeConfig cubeConfig)
        {
            for (int i = 0; i < cubeConfig.m_cubeSides.Length; i++)
            {
                cubeConfig.m_cubeSides[i].m_buttonColor = GenerateRandomPastelColor();
            }

            EditorUtility.SetDirty(cubeConfig);
            Debug.Log("Random pastel colors applied to sides.");
        }

        private Color GenerateRandomPastelColor()
        {
            float hue = Random.Range(0f, 1f);
            float saturation = Random.Range(0.4f, 0.6f); // Pastel saturation range
            float value = Random.Range(0.8f, 1f);       // Brightness for pastel colors

            var returnColor = Color.HSVToRGB(hue, saturation, value);
            returnColor.a = 0.5f;
            return returnColor;
        }
    }
}