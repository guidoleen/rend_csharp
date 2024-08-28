using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD_Enviornment.Observer
{
    internal interface ISubject<T>
    {
        void DeleteSubscriber(IObserver<T> observer);
        void AddSubscriber(IObserver<T> observer);
        T GetObject();
        void SetObject(T obj);
        void Notify();
    }
}
