
namespace RD_Enviornment.Observer
{
    internal class Observer<T> : IObserver<T>
    {
        ISubject<T>? _subject;
        Action<T>? _action;
        public IObserver<T> AddSubject(ISubject<T> subject)
        {
            _subject = subject;
            _subject.AddSubscriber(this);
            return this;
        }
        public void DoAction()
        {
            var obj = _subject!.GetObject();
            _action?.Invoke(obj!);
        }

        public IObserver<T> SetAction(Action<T> action)
        {
            _action = action;
            return this;
        }
    }
}
