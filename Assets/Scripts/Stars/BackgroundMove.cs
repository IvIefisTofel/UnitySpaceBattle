using UnityEngine;

namespace SpaceBattle.Stars
{
    public class BackgroundMove : MonoBehaviour
    {
        public Transform[] starsBackgrounds;
        public float speed = 0.2f;

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            foreach (var background in starsBackgrounds) {
                background.Translate(Vector3.down * speed * deltaTime);

                if (background.position.y < -10) {
                    background.position += new Vector3(0, 30);
                }
            }
        }
    }
}
