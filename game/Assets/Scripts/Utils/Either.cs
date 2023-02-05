using System;

namespace Shake.Utils
{
    internal sealed class Either<TLeft, TRight>
    {
        private readonly TLeft _left;
        private readonly TRight _right;
        private readonly bool _isLeft;

        public TLeft Left
        {
            get
            {
                if (!_isLeft)
                    throw new Exception("Either is Right");

                return _left;
            }
        }
        
        public TRight Right
        {
            get
            {
                if (_isLeft)
                    throw new Exception("Either is Left");

                return _right;
            }
        }

        public Either(TLeft left)
        {
            _left = left;
            _isLeft = true;
        }

        public Either(TRight right)
        {
            _right = right;
            _isLeft = false;
        }

        public override string ToString()
            => _isLeft
                   ? $"Right({Right.ToString()}"
                   : $"Left({Left.ToString()}";

        public Either<TOutLeft, TOutRight> To<TOutLeft, TOutRight>(
            Func<TLeft, TOutLeft> onLeft,
            Func<TRight, TOutRight> onRight)
            => _isLeft
                   ? new Either<TOutLeft, TOutRight>(onLeft(_left))
                   : new Either<TOutLeft, TOutRight>(onRight(_right));
    }
}