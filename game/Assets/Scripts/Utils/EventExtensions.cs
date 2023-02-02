using System;

namespace Shake.Utils
{
    internal static class EventExtensions
    {
        public static void Call<T>(this Action<T> @event, T x)
            => @event?.Invoke(x);
    }
}