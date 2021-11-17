using Leopotam.Ecs;
using SpaceBattle.Components;
using SpaceBattle.Tags;
using UnityEngine;

namespace SpaceBattle.Systems
{
    internal sealed class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DirectionComponent, PlayerTag> _directionFilter = null;

        private float _moveX;
        private float _moveY;

        public void Run()
        {
            SetDirection();

            foreach (var i in _directionFilter)
            {
                ref var direction = ref _directionFilter.Get1(i).direction;

                direction.x = _moveX;
                direction.y = _moveY;
            }
        }

        private void SetDirection()
        {
            _moveX = Input.GetAxis("Horizontal");
            _moveY = Input.GetAxis("Vertical");
        }
    }
}
