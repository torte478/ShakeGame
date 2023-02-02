﻿using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace Shake.Utils
{
    internal static class DOTweenExtensions
    {
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOTimingMove(
            this Transform transform,
            Vector3 to,
            float speed)
        {
            var duration = Vector3.Distance(transform.position, to) * 100f / speed;

            return transform.DOMove(to, duration);
        }
        
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOTimingMove(
            this Transform transform,
            Vector3 from,
            Vector3 to,
            float speed)
        {
            var duration = Vector3.Distance(from, to) * 100f / speed;

            return transform.DOMove(to, duration);
        }
    }
}