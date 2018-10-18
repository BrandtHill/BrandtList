using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// An implementation of ICollection<T> just for fun. Its underlying data structure
/// is an array of structs that contain a generic object and a boolean. The order is
/// invisible to the user, and this implementation aims to be super space efficient,
/// reusing empty array cells before growing. I might add some cool functionality later.
/// Author: Brandt Hill
/// </summary>
namespace BrandtList
{
    public class BrandtList<T> : ICollection<T>
    {
        private const int DEFAULTSIZE = 1024;
        private int size, firstAvailable, firstInUse, lastInUse;
        private Thing[] array;

        private struct Thing{
            public T Item { get; set; }
            public bool InUse { get; set; }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public BrandtList(int size)
        {
            this.size = size;
            array = new Thing[size];
            Count = 0;
            firstAvailable = 0;
            firstInUse = 0;
            lastInUse = 0;
        }

        public BrandtList() : this(DEFAULTSIZE)
        {
        }

        public void Add(T item)
        {
            if (Count == size) ResizeArray();

            array[firstAvailable].Item = item;
            array[firstAvailable].InUse = true;
            Count++;
            firstInUse = (firstAvailable < firstInUse) ? firstAvailable : firstInUse;
            lastInUse = (firstAvailable > lastInUse) ? firstAvailable : lastInUse;
            UpdateFirstAvailable();
        }

        private void ResizeArray()
        {
            size *= 2;
            Array.Resize(ref array, size);
            UpdateFirstAvailable();
        }

        public void Clear()
        {
            for(int i = 0; i < size; i++)
            {
                array[i].Item = default(T);
                array[i].InUse = false;
            }
            Count = 0;
            firstAvailable = 0;
            firstInUse = 0;
            lastInUse = 0;
        }

        public bool Contains(T item)
        {
            for(int i = firstInUse; i <= lastInUse; i++)
            {
                if (array[i].InUse && item.Equals(array[i].Item))
                {
                    return true;
                }
            }
            return false;
        }

        public int ContainsHowMany(T item)
        {
            int num = 0;
            for (int i = firstInUse; i <= lastInUse; i++)
            {
                if (array[i].InUse && item.Equals(array[i].Item))
                {
                    num++;
                }
            }
            return num;
        }

        public void CopyTo(T[] destArray, int arrayIndex)
        {
            int index = firstInUse;
            for (int i = arrayIndex; i < destArray.Length; i++)
            {
                while (!array[index].InUse)
                {
                    index++;
                    if (index > lastInUse) return;
                }
                destArray[i] = array[i].Item;
            }
        }

        public T[] ToArray()
        {
            return array.Where(i => i.InUse).Select(i => i.Item).ToArray();
        }

        private void UpdateFirstAvailable()
        {
            for (int i = firstAvailable + 1; i < size; i++)
            {
                if (!array[i].InUse)
                {
                    firstAvailable = i;
                    return;
                }
            }
        }

        private void UpdateFirstInUse()
        {
            if (Count == 0)
            {
                lastInUse = -1;
                return;
            }
            for (int i = firstInUse + 1; i <= lastInUse; i++)
            {
                if (array[i].InUse)
                {
                    firstInUse = i;
                    return;
                }
            }
        }

        private void UpdateLastInUse()
        {
            if(Count == 0)
            {
                lastInUse = -1;
                return;
            }
            for (int i = lastInUse - 1; i >= firstInUse ; i--)
            {
                if (array[i].InUse)
                {
                    lastInUse = i;
                    return;
                }
            }
        }

        public bool Remove(T item)
        {
            for (int i = firstInUse; i < size; i++)
            {
                if (array[i].InUse && item.Equals(array[i].Item))
                {
                    array[i].Item = default(T);
                    array[i].InUse = false;
                    Count--;
                    if (i == firstInUse)    UpdateFirstInUse();
                    if (i == lastInUse)     UpdateLastInUse();
                    return true;
                }
            }
            return false;
        }

        public int RemoveAll(T item)
        {
            int num = 0;
            for (int i = firstInUse; i <= lastInUse; i++)
            {
                if (array[i].InUse && item.Equals(array[i].Item))
                {
                    array[i].Item = default(T);
                    array[i].InUse = false;
                    Count--;
                    if (i == firstInUse)    UpdateFirstInUse();
                    if (i == lastInUse)     UpdateLastInUse();
                    num++;
                }
            }
            return num;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = firstInUse; i <= lastInUse; i++)
            {
                if (array[i].InUse) yield return array[i].Item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
