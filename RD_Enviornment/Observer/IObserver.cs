using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD_Enviornment.Observer
{
    internal interface IObserver<T>
    {
        IObserver<T> AddSubject(ISubject<T> subject);
        IObserver<T> SetAction(Action<T> action);
        void DoAction();
    }
}
