using UnityEngine;

namespace SpaceBattle.Player
{
    public class ChangeLevel : MonoBehaviour
    {
        public const int BlueSpaceShip = 0;
        public const int RedSpaceShip = 1;
        public const int YellowSpaceShip = 2;

        private int _currentSpaceship = BlueSpaceShip;
        private int _currentLevel = 1;
        private Animator _animator;
        private SpaceShipOptions _options;

        public delegate void LevelChanged();
        public static event LevelChanged OnLevelChanged;

        public Animator GetActiveSpaceShipAnimator()
        {
            return _animator;
        }

        public SpaceShipOptions GetActiveSpaceShipOptions()
        {
            return _options;
        }

        public ChangeLevel SetActiveSpaceShip(int spaceShipIndex, int level)
        {
            if (_animator != null && _currentSpaceship == spaceShipIndex && _currentLevel == level) {
                return this;
            }

            _currentSpaceship = spaceShipIndex;
            _currentLevel = level--;

            var spaceShipType = transform.GetChild(spaceShipIndex);
            for (int i = 0; i < spaceShipType.childCount; i++) {
                if (i == level) {
                    var activeGameObject = spaceShipType.GetChild(i).gameObject;
                    activeGameObject.SetActive(true);

                    _options = activeGameObject.GetComponent<SpaceShipOptions>();

                    if (_animator != null)
                        _animator.enabled = false;
                    _animator = activeGameObject.GetComponent<Animator>();
                    _animator.enabled = true;

                } else {
                    spaceShipType.GetChild(i).gameObject.SetActive(false);
                }
            }

            if (OnLevelChanged != null)
                OnLevelChanged();

            return this;
        }

        private void Start()
        {
            SetActiveSpaceShip(_currentSpaceship, _currentLevel);
        }

        public void SetLvl1()
        {
            SetActiveSpaceShip(BlueSpaceShip, 1);
        }

        public void SetLvl2()
        {
            SetActiveSpaceShip(BlueSpaceShip, 2);
        }

        public void SetLvl3()
        {
            SetActiveSpaceShip(BlueSpaceShip, 3);
        }

        public void SetLvl4()
        {
            SetActiveSpaceShip(BlueSpaceShip, 4);
        }

        public void SetLvl5()
        {
            SetActiveSpaceShip(BlueSpaceShip, 5);
        }
    }
}
