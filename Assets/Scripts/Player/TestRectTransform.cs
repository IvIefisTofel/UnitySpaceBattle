using UnityEngine;

namespace SpaceBattle.Player
{
    public class TestRectTransform : MonoBehaviour
    {
        public RectTransform rectTransform;

        void Start()
        {
            rectTransform.anchorMin = new Vector2(1, 0);
            rectTransform.anchorMax = new Vector2(1, 0);
            rectTransform.pivot = new Vector2(1, 0);
            
            rectTransform.anchoredPosition3D = new Vector3(-30, 30);
        }
    }
}
