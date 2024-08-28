using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD_Enviornment.Observer
{
    internal class Subject<T> : ISubject<T>
    {
        private List<IObserver<T>> _observerList = new List<IObserver<T>>();
        private T? _object = default (T);
        public void AddSubscriber(IObserver<T> observer)
        {
            _observerList.Add(observer);
        }

        public void DeleteSubscriber(IObserver<T> observer)
        {
            _observerList.Remove(observer);
        }

        public T GetObject()
        {
           return _object;
        }

        public void SetObject(T obj)
        {
            _object = obj;
        }

        public void Notify()
        {
            foreach (IObserver<T> observer in _observerList)
                observer.DoAction();
        }
    }
}
