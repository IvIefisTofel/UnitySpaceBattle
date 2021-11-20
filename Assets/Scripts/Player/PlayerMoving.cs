using System.Collections.Generic;
using SpaceBattle.Classes;
using UnityEngine;

namespace SpaceBattle.Player
{
    public class PlayerMoving : MonoBehaviour
    {
        public Transform player;
        public BoxCollider2D playerCollider;
        public ChangeLevel changeLevel;

        private Restrictions _restrictions;
        private PlayerInputAction _controls;

        private Animator _activeSpaceShipAnimator;
        private SpaceShipOptions _activeSpaceShipOptions;

        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int MoveUpDown = Animator.StringToHash("MoveUpDown");
        private static readonly int MoveLeft = Animator.StringToHash("MoveLeft");
        private static readonly int MoveRight = Animator.StringToHash("MoveRight");

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
            ChangeLevel.OnLevelChanged += ChangeActiveSpaceShip;
        }

        private void OnDisable()
        {
            _controls.Disable();
            ChangeLevel.OnLevelChanged -= ChangeActiveSpaceShip;
        }

        private void ChangeActiveSpaceShip()
        {
            _activeSpaceShipAnimator = changeLevel.GetActiveSpaceShipAnimator();
            _activeSpaceShipOptions = changeLevel.GetActiveSpaceShipOptions();
        }

        private void Update()
        {
            PlayerMove(_controls.PlayerActionMap.Move.ReadValue<Vector2>());
        }

        private void PlayerMove(Vector2 direction)
        {
            var rawDirection = new Vector3(direction.x, direction.y)
                               * _activeSpaceShipOptions.speedMultiplier
                               * Time.deltaTime;

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
                _activeSpaceShipAnimator.SetTrigger(Idle);
            } else if (rawDirection.x != 0) {
                _activeSpaceShipAnimator.SetTrigger(
                    rawDirection.x < 0
                        ? MoveLeft
                        : MoveRight
                    );
            }
            else {
                _activeSpaceShipAnimator.SetTrigger(MoveUpDown);
            }
            
            player.Translate(rawDirection);
        }
    }
}
