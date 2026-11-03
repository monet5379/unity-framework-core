using System;

namespace Monet5379
{
    [Serializable]
    public class ArrayBuilder<T>
    {
        private T[] _array;
        private int _capacity;
        private int _count;

        public int Count => _count;

        public int Capacity => _capacity;

        public ArrayBuilder() : this(16)
        {
        }

        public ArrayBuilder(int initialCapacity)
        {
            if (initialCapacity <= 0)
                initialCapacity = 16;

            _capacity = initialCapacity;
            _array = new T[_capacity];
            _count = 0;
        }

        public void Add(T item)
        {
            if (_count >= _capacity)
            {
                Resize();
            }
            _array[_count++] = item;
        }

        public void AddRange(T[] items)
        {
            if (items == null || items.Length == 0)
                return;

            for (int i = 0; i < items.Length; i++)
            {
                Add(items[i]);
            }
        }

        public void AddRange(System.Collections.Generic.IEnumerable<T> items)
        {
            if (items == null)
                return;

            foreach (T item in items)
            {
                Add(item);
            }
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (_count >= _capacity)
            {
                Resize();
            }

            for (int i = _count; i > index; i--)
            {
                _array[i] = _array[i - 1];
            }

            _array[index] = item;
            _count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            for (int i = index; i < _count - 1; i++)
            {
                _array[i] = _array[i + 1];
            }

            _count--;
        }

        public void Clear()
        {
            _count = 0;
        }

        public T[] ToArray()
        {
            if (_count == 0)
                return new T[0];

            if (_count == _capacity)
            {
                return _array;
            }

            T[] result = new T[_count];
            Array.Copy(_array, result, _count);
            return result;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return _array[index];
            }
            set
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                _array[index] = value;
            }
        }

        private void Resize()
        {
            _capacity *= 2;
            T[] newArray = new T[_capacity];
            Array.Copy(_array, newArray, _count);
            _array = newArray;
        }

        public void SetCapacity(int newCapacity)
        {
            if (newCapacity < _count)
                throw new ArgumentException("새로운 용량은 현재 요소 수보다 작을 수 없습니다.");

            if (newCapacity != _capacity)
            {
                _capacity = newCapacity;
                T[] newArray = new T[_capacity];
                Array.Copy(_array, newArray, _count);
                _array = newArray;
            }
        }

        public void Resize(int newSize)
        {
            if (newSize < 0)
                throw new ArgumentException("크기는 0보다 작을 수 없습니다.");

            if (newSize > _capacity)
            {
                SetCapacity(newSize);
            }

            _count = newSize;
        }
    }
}
