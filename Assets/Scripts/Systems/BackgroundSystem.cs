using Leopotam.Ecs;
using SpaceBattle.Components;
using SpaceBattle.Tags;
using UnityEngine;

// using Object = UnityEngine.Object;

namespace SpaceBattle.Systems
{
    public class BackgroundSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, BackgroundSpriteTag> _background = default;

        public void Run()
        {
            var deltaTime = Time.deltaTime;
            foreach (var i in _background) {
                ref var sprite = ref _background.Get1(i);
                
                sprite.modelTransform.Translate(Vector3.down * deltaTime);

                if (sprite.modelTransform.position.y < -10) {
                    sprite.modelTransform.position += new Vector3(0, 30);
                }
            }
        }
    }
}
