using Leopotam.Ecs;
using UnityEngine;

namespace SpaceBattle.Systems
{
    public class InputSystem : IEcsRunSystem
    {
        // private readonly EcsFilter<ModelComponent, PlayerTag> _directionFilter = null;
        
        private float _moveX;
        private float _moveZ;
        
        public void Run()
        {
            SetDirection();
            
            // if (Input.mouseScrollDelta.y != 0f)
            //     Debug.Log(Input.mouseScrollDelta.y);
            //     
            // if(Input.GetMouseButtonDown(0))
            //     Debug.Log("Left mouse button Click");
            //
            // if(Input.GetMouseButtonDown(1))
            //     Debug.Log("Right mouse button Click");
            //
            // if(Input.GetMouseButtonDown(2))
            //     Debug.Log("Scroll mouse button Click");

            // foreach (var i in _directionFilter)
            // {
            //     ref var direction = ref _directionFilter.Get1(i).Direction;
            //
            //     direction.x = _moveX;
            //     direction.z = _moveZ;
            // }
        }

        private void SetDirection()
        {
            _moveX = Input.GetAxis("Horizontal");
            _moveZ = Input.GetAxis("Vertical");
        }
    }
}
