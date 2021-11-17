using Leopotam.Ecs;
using SpaceBattle.Components;
using SpaceBattle.Tags;
using UnityEngine;

// using Object = UnityEngine.Object;

namespace SpaceBattle.Systems
{
    internal sealed class BackgroundSystem : IEcsRunSystem
    {
        private const float Speed = 0.2f;

        private readonly EcsFilter<ModelComponent, BackgroundSpriteTag> _backgroundFilter = default;

        public void Run()
        {
            var deltaTime = Time.deltaTime;
            foreach (var i in _backgroundFilter) {
                ref var transform = ref _backgroundFilter.Get1(i).modelTransform;

                transform.Translate(Vector3.down * Speed * deltaTime);

                if (transform.position.y < -10) {
                    transform.position += new Vector3(0, 30);
                }
            }
        }
    }
}
