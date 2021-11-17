using Leopotam.Ecs;
using SpaceBattle.Components;
using SpaceBattle.Tags;
using UnityEngine;

namespace SpaceBattle.Systems
{
    internal sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, MovableComponent, DirectionComponent> _movableFilter = default;

        public void Run()
        {
            var deltaTime = Time.deltaTime;

            foreach (var i in _movableFilter)
            {
                ref var transform = ref _movableFilter.Get1(i).modelTransform;
                ref var speed = ref _movableFilter.Get2(i).speed;
                ref var direction = ref _movableFilter.Get3(i).direction;

                var rawDirection = (transform.right * direction.x) + (transform.up * direction.y);

                transform.Translate(rawDirection * speed * deltaTime);
            }
        }
    }
}
