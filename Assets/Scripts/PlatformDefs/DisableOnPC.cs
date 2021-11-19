using UnityEngine;

namespace SpaceBattle.PlatformDefs
{
    public class DisableOnPC : MonoBehaviour
    {
        private void Start()
        {
#if UNITY_STANDALONE
            gameObject.SetActive(false);
#endif
        }
    }
}
