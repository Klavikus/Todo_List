using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Source.Common.Components.Implementations
{
    [Serializable]
    public struct Vector2TweenData
    {
        public Vector2 Value;
        public float Duration;
        public Ease Ease;
    }
}