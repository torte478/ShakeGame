namespace Shake.Utils
{
    internal sealed class Memorable<T>
    {
        private T _current;
        
        public T Previous { get; private set; }

        public T Current
        {
            get => _current;
            set
            {
                Previous = _current;
                _current = value;
            }
        }

        public Memorable(T current)
        {
            _current = current;
        }
    }
}