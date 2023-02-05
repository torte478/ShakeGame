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
        private readonly bool _isSome;

        public T Value
        {
            get
            {
                if (!_isSome)
                    throw new Exception("Maybe is None");

                return _value;
            }
        }
        
        public Maybe()
        {
            _isSome = false;
        }

        public Maybe(T value)
        {
            _isSome = true;
            _value = value;
        }

        public override string ToString()
            => _isSome
                   ? $"Some({Value.ToString()})"
                   : "None";

        public Maybe<TOut> To<TOut>(Func<T, TOut> onSome)
            => _isSome
                   ? new Maybe<TOut>(onSome(_value))
                   : new Maybe<TOut>();

        public TOut To<TOut>(Func<T, TOut> onSome, Func<TOut> onNone)
            => _isSome
                   ? onSome(_value)
                   : onNone();

        public Maybe<T> To(Action<T> onSome)
        {
            if (_isSome)
                onSome(_value);

            return this;
        }
    }
}