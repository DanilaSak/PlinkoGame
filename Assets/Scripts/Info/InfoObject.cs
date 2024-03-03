using UnityEngine;

namespace Info
{
    
    [CreateAssetMenu(menuName = "Create InfoObject", fileName = "InfoObject", order = 0)]
    public class InfoObject : ScriptableObject
    {
        public Sprite image;
        [TextArea(4,8)]
        public string text;
    }
}