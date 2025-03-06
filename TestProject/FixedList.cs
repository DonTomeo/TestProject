using System.Collections;

namespace TestProject
{
    public class FixedList<T> : IEnumerable
    {
        public FixedList(int lenght)
        {
            values = new T[lenght];
        }
        T[] values;
        int _freeSpaceFromEnd = 0;
        int? _freeSpace;
        bool _skipNullOrDefault = false;
        public T this[int index]
        {
            get
            {
                return values[index];
            }
            set
            {
                values[index] = value;
            }
        }

        public FixedList<T> SkipNullOrDefault(bool val)
        {
            this._skipNullOrDefault = val;

            return this;
        }

        public T Add(T value)
        {
            int index = _freeSpace ?? _freeSpaceFromEnd;
            bool incrise = false;
            if (_freeSpace == null)
            {
                incrise = true;
            }
            values[index] = value;
            if (incrise)
            {
                _freeSpaceFromEnd++;
            }
            _freeSpace = null;
            return value; ;
        }

        public bool Remove(T value)
        {
            int index = FindIndex(value);
            if (index >= 0)
            {
                values[index] = default(T);
                _freeSpace = index;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int FindIndex(T obj)
        {
            if (obj == null)
                throw new NullReferenceException();

            int pos = 0;
            do
            {
                if (pos < values.Length)
                {
                    EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
                    if (equalityComparer.Equals(obj, values[pos]))
                    {
                        return pos;
                    }
                    pos++;
                }
                else
                {
                    return -1;
                }
            } while (true);
        }

        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this, _skipNullOrDefault);
        }

        public struct Enumerator : IEnumerator<T>
        {
            int _index = -1;
            FixedList<T> _list;
            bool _skipNullOrDefault;
            public Enumerator(FixedList<T> list)
            {
                _list = list;
            }

            public Enumerator(FixedList<T> list, bool skipNullOrDefault) : this(list)
            {
                _skipNullOrDefault = skipNullOrDefault;
            }

            public T Current => _list[_index];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                _index++;
                T? defaultValue = default(T);
                EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;

                if (_list.values == null || _index >= _list.values.Length)
                {
                    return false;
                }
                else if (defaultValue == null || equalityComparer.Equals(_list.values[_index]))
                {
                    if (_skipNullOrDefault)
                        return MoveNext();

                    return true;
                }

                return true;
            }


            public void Reset()
            {
                _index = -1;
            }
        }
    }
}
