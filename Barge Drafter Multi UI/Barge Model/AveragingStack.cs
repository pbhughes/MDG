using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MDG.Model
{
    public class AveragingStack<T>:IEnumerable<T>,ICollection<T>
    {
        private Queue<T> _items;

        private int _count = 0;
        public AveragingStack()
        {
            _items = new Queue<T>();
            _count = 5;

        } 

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public void Add(T item)
        {
            TypeCheck(item);
            if (_items.Count == _count)
            {
                _items.Dequeue();
              
            }
            _items.Enqueue ( item );
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            TypeCheck(item);
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array,arrayIndex);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            TypeCheck(item);
            _items.Dequeue();
            return true;
        }

        private void TypeCheck(T item)
        {
            if (item is short || item is int || item is double || item is float || item is decimal || item is ushort)
                ; //Allow numbers
            else
            {
                throw new InvalidOperationException(
                    "Place only numeric types on the stack for averaging.  Currently supports short, integer, double, decimal, and float types only");
            }
        }

        public double Average
        {
            get { 
                double sum = 0F;
                foreach (var item in _items)
                {
                    double val = Convert.ToDouble(item);
                    sum += val;
                }
                return sum/_items.Count;


            }
        }

      
        
    }
}
