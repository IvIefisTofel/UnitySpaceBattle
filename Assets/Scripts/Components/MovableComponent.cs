using System;
using UnityEngine;

namespace SpaceBattle.Components
{
    [Serializable]
    public struct MovableComponent
    {
        public Animator animator;
        public float speed;
    }
}