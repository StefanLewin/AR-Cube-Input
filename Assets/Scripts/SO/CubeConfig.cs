using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "CubeConfig", menuName = "ARCube/Config", order = 1)]
    public class CubeConfig : ScriptableObject
    {
        public CubeSide[] m_cubeSides = new CubeSide[6];
    }
    
    [System.Serializable]
    public struct CubeSide
    {
        [Tooltip("The Target image. Hints: Image has to be configured as a Sprite(2D and UI) and 'Read/Write' has to be enabled")] 
        public Texture2D m_targetImage;
        [Tooltip("Name of the cube side")]
        public string m_targetName;
        [Tooltip("Background color of the cube side")]
        public Color m_buttonColor;
        [Tooltip("Currently only 4 buttons are accepted. Everything beyond that is ignored")]
        public string[] m_buttonNames;
    }
}