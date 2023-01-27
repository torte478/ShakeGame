using System;
using UnityEditorInternal;

namespace Utils
{
    internal static class FuncExtensions
    {
        public static TOut _<TIn, TOut>(this TIn x, Func<TIn, TOut> f)
            => f(x);
    }
}