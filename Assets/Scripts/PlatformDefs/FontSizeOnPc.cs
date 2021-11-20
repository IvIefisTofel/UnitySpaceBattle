using UnityEngine;
using UnityEngine.UI;

namespace SpaceBattle.PlatformDefs
{
    public class FontSizeOnPc : MonoBehaviour
    {
        public int fontSize;

        void Start()
        {
#if UNITY_STANDALONE
            gameObject.GetComponent<Text>().fontSize = fontSize;
#endif
        }
    }
}
