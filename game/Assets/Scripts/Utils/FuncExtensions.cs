using System;

namespace Shake.Utils
{
    internal static class FuncExtensions
    {
        public static TOut _<TIn, TOut>(this TIn x, Func<TIn, TOut> f)
            => f(x);
    }
}