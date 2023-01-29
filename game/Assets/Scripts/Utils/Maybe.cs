using System;

namespace Shake.Utils
{
    internal static class Maybe
    {
        public static Maybe<T> Some<T>(T value) => new(value);
        public static Maybe<T> None<T>() => new();
    }
    
    internal sealed class Maybe<T>
    {
        private readonly T _value;

        public bool IsSome { get; }

        public bool IsNone => !IsSome;

        public T Value
        {
            get
            {
                if (!IsSome)
                    throw new Exception("Maybe is None");

                return _value;
            }
        }
        
        public Maybe()
        {
            IsSome = false;
        }

        public Maybe(T value)
        {
            IsSome = true;
            _value = value;
        }

        public override string ToString()
            => IsSome
                   ? $"Some({Value.ToString()})"
                   : "None";
    }
}