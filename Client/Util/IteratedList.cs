using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Client
{
    public class IteratedList<T> : IList<T>
    {
        List<T> internalList;
        bool iterating;
        List<T> toBeAdded;
        List<T> toBeRemoved;

        public IteratedList()
        {
            internalList = new List<T>();
            toBeAdded = new List<T>();
            toBeRemoved = new List<T>();
        }

        T IList<T>.this[int index]
        {
            get => internalList[index];

            set
            {
                internalList[index] = value;
            }
        }

        int ICollection<T>.Count => internalList.Count;

        bool ICollection<T>.IsReadOnly => false;

        public void Add(T item)
        {
            if (iterating)
                toBeAdded.Add(item);
            else
                internalList.Add(item);
        }

        public void Clear()
        {
            if (iterating)
                throw new NotSupportedException();
            internalList.Clear();
            toBeAdded.Clear();
            toBeRemoved.Clear();
        }

        bool ICollection<T>.Contains(T item)
        {
            return internalList.Contains(item);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)internalList).CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotSupportedException("Use " + nameof(ForEach) + " method");
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotSupportedException("Use " + nameof(ForEach) + " method");
        }

        int IList<T>.IndexOf(T item)
        {
            return internalList.IndexOf(item);
        }

        void IList<T>.Insert(int index, T item)
        {
            if (iterating)
                throw new NotSupportedException("Use" + nameof(Add) + " method");
            internalList.Insert(index, item);
        }

        bool ICollection<T>.Remove(T item)
        {
            if (iterating)
            {
                toBeRemoved.Add(item);
                return internalList.Contains(item);
            }

            return internalList.Remove(item);
        }

        void IList<T>.RemoveAt(int index)
        {
            if (iterating)
                throw new NotSupportedException("Use" + nameof(ICollection<T>.Remove) + " method");
            internalList.RemoveAt(index);
        }

        public void ForEach(Action<T> action)
        {
            iterating = true;

            internalList.ForEach(action);

            if (toBeAdded.Count > 0)
            {
                internalList.AddRange(toBeAdded);
                toBeAdded.Clear();
            }

            if (toBeRemoved.Count > 0)
            {
                internalList.RemoveAll(element => toBeRemoved.Contains(element));
                toBeRemoved.Clear();
            }

            iterating = false;
        }

        public int RemoveAll(Predicate<T> match)
        {
            if (iterating)
            {
                var matched = internalList.Where(element => match(element)).ToList();
                toBeRemoved.AddRange(matched);
                return matched.Count;
            }

            return internalList.RemoveAll(match);
        }
    }
}
