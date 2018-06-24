using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* An implementation of ICollection<T> that's basically an ArrayList.
* Just for fun. Might add some cool/useful list functions.
* The list will just be a collection of whatever objects, and 
* the order will be invisible to the user.
* Author: Brandt Hill
*/
namespace BrandtList
{
    public class BrandtList<T> : ICollection<T>
    {
        private const int DEFAULTSIZE = 1024;
        private int size, count, firstAvailable, firstInUse, lastInUse;
        private T[] array;
        private bool[] inUse;

        public int Count
        {
            get
            {
                return count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public BrandtList(int size)
        {
            this.size = size;
            array = new T[size];
            inUse = new bool[size];
            count = 0;
            firstAvailable = 0;
            firstInUse = 0;
            lastInUse = 0;
        }

        public BrandtList()
        {
            this.size = DEFAULTSIZE;
            array = new T[size];
            inUse = new bool[size];
            count = 0;
            firstAvailable = 0;
            firstInUse = 0;
            lastInUse = 0;
        }

        public void Add(T item)
        {
            if (count == size)
                ResizeArray();
            array[firstAvailable] = item;
            inUse[firstAvailable] = true;
            count++;
            firstInUse = (firstAvailable < firstInUse) ? firstAvailable : firstInUse;
            lastInUse = (firstAvailable > lastInUse) ? firstAvailable : lastInUse;
            UpdateFirstAvailable();
        }

        private void ResizeArray()
        {
            size *= 2;
            Array.Resize(ref array, size);
            Array.Resize(ref inUse, size);
        }

        public void Clear()
        {
            for(int i = 0; i < size; i++)
            {
                array[i] = default(T);
                inUse[i] = false;
            }
            count = 0;
            firstAvailable = 0;
            firstInUse = 0;
            lastInUse = 0;
        }

        public bool Contains(T item)
        {
            for(int i = firstInUse; i <= lastInUse; i++)
            {
                if (inUse[i] && item.Equals(array[i]))
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
                if (inUse[i] && item.Equals(array[i]))
                    num++;
            }
            return num;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int index = firstInUse;
            for (int i = arrayIndex; i < array.Length; i++)
            {
                while (!inUse[index])
                {
                    index++;
                    if (index > lastInUse)
                        return;
                }
                array[i] = this.array[i];
            }
        }

        private void UpdateFirstAvailable()
        {
            for (int i = firstAvailable + 1; i < size; i++)
            {
                if (!inUse[i])
                {
                    firstAvailable = i;
                    return;
                }
            }
        }

        private void UpdateFirstInUse()
        {
            if (count == 0)
            {
                lastInUse = -1;
                return;
            }
            for (int i = firstInUse + 1; i <= lastInUse; i++)
            {
                if (inUse[i])
                {
                    firstInUse = i;
                    return;
                }
            }
        }

        private void UpdateLastInUse()
        {
            if(count == 0)
            {
                lastInUse = -1;
                return;
            }
            for (int i = lastInUse - 1; i >= firstInUse ; i--)
            {
                if (inUse[i])
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
                if (inUse[i] && item.Equals(array[i]))
                {
                    array[i] = default(T);
                    inUse[i] = false;
                    count--;
                    if (i == firstInUse)
                        UpdateFirstInUse();
                    if (i == lastInUse)
                        UpdateLastInUse();
                    return true;
                }
            }
            return false;
        }

        public int RemoveAll(T item)
        {
            int num = 0;
            for (int i = firstInUse; i < lastInUse; i++)
            {
                if (inUse[i] && item.Equals(array[i]))
                {
                    array[i] = default(T);
                    inUse[i] = false;
                    count--;
                    if (i == firstInUse)
                        UpdateFirstInUse();
                    if (i == lastInUse)
                        UpdateLastInUse();
                    num++;
                }
            }
            return num;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = firstInUse; i <= lastInUse; i++)
            {
                if (inUse[i])
                    yield return array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
