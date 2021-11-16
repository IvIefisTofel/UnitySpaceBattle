using Leopotam.Ecs;
using SpaceBattle.Systems;
using UnityEngine;
using Voody.UniLeo;

namespace SpaceBattle.MonoBehaviours {
    internal sealed class EcsGameStartup : MonoBehaviour {
        private EcsWorld _world;
        private EcsSystems _systems;

        public void Start () {
            // void can be switched to IEnumerator for support coroutines.
            
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif

            _systems.ConvertScene();

            AddSystems();
            AddOneFrames();
            AddInjections();

            _systems.Init();
        }

        public void Update () {
            _systems.Run();
        }

        public void OnDestroy ()
        {
            if (_systems == null) return;

            _systems.Destroy ();
            _systems = null;
            _world.Destroy ();
            _world = null;
        }

        private void AddSystems()
        {
            _systems
                .Add(new InputSystem())
            ;
        }

        private void AddOneFrames()
        {
            // register one-frame components (order is important), for example:
            // .OneFrame<TestComponent1> ()
            // .OneFrame<TestComponent2> ()
        }

        private void AddInjections()
        {
            // inject service instances here (order doesn't important), for example:
            // .Inject (new CameraService ())
            // .Inject (new NavMeshSupport ())
        }

    }
}