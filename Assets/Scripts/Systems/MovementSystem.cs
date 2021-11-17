using System.Collections.Generic;
using Leopotam.Ecs;
using SpaceBattle.Classes;
using SpaceBattle.Components;
using SpaceBattle.Tags;
using UnityEngine;

namespace SpaceBattle.Systems
{
    internal sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, MovableComponent, DirectionComponent> _movableFilter = default;
        
        private readonly Restrictions _restrictions;

        private static readonly int MovingLeftRight = Animator.StringToHash("MovingLeftRight");
        private static readonly int MovingUpDown = Animator.StringToHash("MovingUpDown");

        public MovementSystem()
        {
            _restrictions = new Restrictions
            {
                x = new List<float>() {-3.5f, 3.5f},
                y = new List<float>() {-4.5f, 4f}
            };
        }

        public void Run()
        {
            var deltaTime = Time.deltaTime;

            foreach (var i in _movableFilter)
            {
                ref var transform = ref _movableFilter.Get1(i).modelTransform;
                ref var movableComponent = ref _movableFilter.Get2(i);
                ref var direction = ref _movableFilter.Get3(i).direction;

                var rawDirection = (transform.right * direction.x) + (transform.up * direction.y);

                if (rawDirection.x != 0)
                    foreach (var x in _restrictions.x)
                        if (
                            (x > 0 && rawDirection.x > 0 && transform.position.x >= x)
                            || (x < 0 && rawDirection.x < 0 && transform.position.x <= x)
                        ) {
                            rawDirection.x = 0;
                            transform.position = new Vector3(x, transform.position.y, transform.position.z);
                        }

                if (rawDirection.y != 0)
                    foreach (var y in _restrictions.y)
                        if (
                            (y > 0 && rawDirection.y > 0 && transform.position.y >= y)
                            || (y < 0 && rawDirection.y < 0 && transform.position.y <= y)
                        ) {
                            rawDirection.y = 0;
                            transform.position = new Vector3(transform.position.x, y, transform.position.z);
                        }

                if (movableComponent.animator != null) {
                    movableComponent.animator.SetBool(MovingLeftRight, rawDirection.x != 0);
                    movableComponent.animator.SetBool(MovingUpDown, rawDirection.y != 0);
                }

                transform.Translate(rawDirection * movableComponent.speed * deltaTime);
            }
        }
    }
}
