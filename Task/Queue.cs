using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class Queue<T> : IEnumerable, IEnumerable<T>
    {
        private T[] arrQueue;

        private int head = 0;
        private int tail = 0;
        private int size = 0;

        private const int defaultCapacity = 4;

        public int Count => this.size;

        public Queue()
        {
            arrQueue = new T[defaultCapacity];
        }


        public Queue(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException("capacity is lower than a zero");
            arrQueue = new T[capacity];
        }


        public bool Contains(T item)
        {
            int index = this.head;
            int size = this.size;
            EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
            while (size --> 0)
            {
                if ((object)item == null)
                {
                    if ((object)this.arrQueue[index] == null)
                        return true;
                }
                else if ((object)this.arrQueue[index] != null && equalityComparer.Equals(this.arrQueue[index], item))
                    return true;
                index = (index + 1) % this.arrQueue.Length;
            }
            return false;
        }

        public void Enqueue(T item)
        {
            if (this.size == this.arrQueue.Length)
            {
                int capacity = (int)((long)this.arrQueue.Length * 200L / 100L);
                if (capacity < this.arrQueue.Length + 4)
                    capacity = this.arrQueue.Length + 4;
                this.SetCapacity(capacity);
            }
            this.arrQueue[this.tail] = item;
            this.tail = (this.tail + 1) % this.arrQueue.Length;
            this.size = this.size + 1;
        }

        public T Dequeue()
        {
            if (this.size == 0)
                throw new InvalidOperationException("size is zero");
            T obj = this.arrQueue[this.head];
            this.arrQueue[this.head] = default(T);
            this.head++;
            this.size--;
            return obj;
        }

        public T Peek()
        {
            if (this.size == 0)
                throw new InvalidOperationException("size is zero");
            return this.arrQueue[this.head];
        }

        internal T GetElement(int i)
        {
            return this.arrQueue[(this.head + i) % this.arrQueue.Length];
        }

        private void SetCapacity(int capacity)
        {
            T[] objArray = new T[capacity];
            if (this.size > 0)
            {
                if (this.head < this.tail)
                {
                    Array.Copy((Array)this.arrQueue, this.head, (Array)objArray, 0, this.size);
                }
                else
                {
                    Array.Copy((Array)this.arrQueue, this.head, (Array)objArray, 0, this.arrQueue.Length - this.head);
                    Array.Copy((Array)this.arrQueue, 0, (Array)objArray, this.arrQueue.Length - this.head, this.tail);
                }
            }
            this.arrQueue = objArray;
            this.head = 0;
            this.tail = this.size == capacity ? 0 : this.size;
        }

        public void TrimExcess()
        {
            if (this.size >= (int)((double)this.arrQueue.Length * 0.9))
                return;
            this.SetCapacity(this.size);
        }

        public T[] ToArray()
        {
            T[] objArray = new T[this.size];
            if (this.size == 0)
                return objArray;
            if (this.head < this.tail)
            {
                Array.Copy((Array)this.arrQueue, this.head, (Array)objArray, 0, this.size);
            }
            else
            {
                Array.Copy((Array)this.arrQueue, this.head, (Array)objArray, 0, this.arrQueue.Length - this.head);
                Array.Copy((Array)this.arrQueue, 0, (Array)objArray, this.arrQueue.Length - this.head, this.tail);
            }
            return objArray;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        private struct Enumerator : IEnumerator<T>
        {
            private Queue<T> q;
            private int index;
            private T currentElement;


            internal Enumerator(Queue<T> q)
            {
                this.q = q;
                index = -1;
                currentElement = default(T);
            }


            public void Dispose()
            {
                index = -2;
                currentElement = default(T);
            }

            public bool MoveNext()
            {
                if (index == -2)
                    return false;

                index++;

                if (index == q.size)
                {
                    index = -2;
                    currentElement = default(T);
                    return false;
                }

                currentElement = q.GetElement(index);
                return true;
            }

            public T Current
            {
                get
                {
                    if (index < 0)
                    {
                        if (index == -1)
                            throw new InvalidOperationException(nameof(index));
                        else
                            throw new InvalidOperationException(nameof(index));
                    }
                    return currentElement;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }
            public void Reset()
            {
                index = -1;
                currentElement = default(T);
            }
        }
    }
}