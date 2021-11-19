using System.Collections.Generic;
using SpaceBattle.Classes;
using UnityEngine;

namespace SpaceBattle.Player
{
    public class PlayerMoving : MonoBehaviour
    {
        public float movingSpeed = 1.2f;
        public Transform player;
        public BoxCollider2D playerCollider;
        public Animator animator;

        private Restrictions _restrictions;
        private PlayerInputAction _controls;

        private void Start()
        {
            float halfHeight = Camera.main.orthographicSize;
            float halfWidth = halfHeight * Screen.width / Screen.height;
            float yLimit = halfHeight - (playerCollider.size.y / 2);

            _restrictions = new Restrictions
            {
                x = new List<float>() {halfWidth, halfWidth * -1},
                y = new List<float>() {yLimit - 0.35f, yLimit * -1},
            };
        }

        private void Awake()
        {
            _controls = new PlayerInputAction();
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Update()
        {
            PlayerMove(_controls.PlayerActionMap.Move.ReadValue<Vector2>());
        }

        private void PlayerMove(Vector2 direction)
        {
            var rawDirection = new Vector3(direction.x, direction.y) * movingSpeed * Time.deltaTime;

            if (rawDirection.x != 0)
                foreach (var x in _restrictions.x)
                    if (
                        (x > 0 && rawDirection.x > 0 && player.position.x >= x)
                        || (x < 0 && rawDirection.x < 0 && player.position.x <= x)
                    ) {
                        rawDirection.x = 0;
                        player.position = new Vector3(x, player.position.y, player.position.z);
                    }

            if (rawDirection.y != 0)
                foreach (var y in _restrictions.y)
                    if (
                        (y > 0 && rawDirection.y > 0 && player.position.y >= y)
                        || (y < 0 && rawDirection.y < 0 && player.position.y <= y)
                    ) {
                        rawDirection.y = 0;
                        player.position = new Vector3(player.position.x, y, player.position.z);
                    }

            if (rawDirection.x == 0 && rawDirection.y == 0) {
                animator.Play("Player_idle");
            } else if (rawDirection.x != 0) {
                animator.Play(
                    rawDirection.x < 0
                        ? "Player_move_left"
                        : "Player_move_right"
                    );
            }
            else {
                animator.Play("Player_move_up_down");
            }
            
            player.Translate(rawDirection);
        }
    }
}
